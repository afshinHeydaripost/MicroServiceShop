using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Products.DataModel.Context;
using Helper.VieModels;
using Products.DataModel.Models;

/// <summary>
/// Summary description for BestPrice
/// </summary>
public class BestPrice
{
    private readonly List<Discount> _discounts;
    private readonly List<ProductModelViewMode> _products;

    public BestPrice(
        MicroServiceShopContext context,
        List<int> validProductIds,
        int productId = 0,
        int productModelId = 0)
    {
        _products = context.ProductModels
            .Where(x => validProductIds.Contains(x.ProductId) && x.Price > 0)
            .Where(x => productId == 0 || x.ProductId == productId)
            .Where(x => productModelId == 0 || x.Id == productModelId)
            .Select(x => new ProductModelViewMode
            {
                ProductId = x.ProductId,
                ProductModelId = x.Id,
                BrandId = x.Product.BrandId,
                Price = x.Price,
                Amount = x.Price
            })
            .ToList();

        _discounts = context.Discounts
            .Where(x => x.Active && x.ValidityDate > DateTime.Now)
            .ToList();
    }

    public resBestPrice ByProductModelID(int productModelId)
    {
        var product = _products.FirstOrDefault(x => x.ProductModelId == productModelId);
        return product == null
            ? EmptyResult()
            : Calculate(product);
    }

    public resBestPrice ByProductID(int productId)
    {
        var products = _products.Where(x => x.ProductId == productId).ToList();
        if (!products.Any())
            return EmptyResult();

        return products
            .Select(Calculate)
            .OrderBy(x => x.Price)
            .First();
    }

    private resBestPrice Calculate(ProductModelViewMode product)
    {
        var basePrice = product.Price ?? 0;
        var result = CreateBaseResult(basePrice);

        var discount =
            GetModelDiscount(product.ProductModelId) ??
            GetProductDiscount(product.ProductId) ??
            GetBrandDiscount(product.BrandId);

        return discount == null
            ? result
            : ApplyDiscount(result, discount);
    }

    #region Discount Finders

    private Discount GetModelDiscount(int? modelId) =>
        _discounts.FirstOrDefault(x => x.ProductModelId == modelId);

    private Discount GetProductDiscount(int productId) =>
        _discounts.FirstOrDefault(x =>
            x.ProductId == productId && x.ProductModelId == null);

    private Discount GetBrandDiscount(int? brandId) =>
        _discounts.FirstOrDefault(x =>
            x.BrandId == brandId && x.ProductId == null && x.ProductModelId == null);

    #endregion

    #region Helpers

    private resBestPrice ApplyDiscount(resBestPrice res, Discount discount)
    {
        decimal finalPrice;

        if (discount.DiscountRate > 0)
        {
            finalPrice = Math.Ceiling(
                (res.BasePrice * (1 - discount.DiscountRate.Value / 100)) / 100) * 100;
        }
        else
        {
            finalPrice = res.BasePrice - discount.DiscountPrice.GetValueOrDefault();
        }

        if (finalPrice <= 0)
            return res;

        res.Price = (int)finalPrice;
        res.DiscountID = discount.Id;
        res.DisPercent = CalculatePercent(res.BasePrice, res.Price);
        return res;
    }

    private static float CalculatePercent(int basePrice, int finalPrice) =>
        (float)Math.Round(((basePrice - finalPrice) / (float)basePrice) * 100, 1);

    private static resBestPrice CreateBaseResult(int price) => new()
    {
        BasePrice = price,
        Price = price,
        DisPercent = 0
    };

    private static resBestPrice EmptyResult() => new()
    {
        BasePrice = 0,
        Price = 0,
        DisPercent = 0
    };

    #endregion
}


