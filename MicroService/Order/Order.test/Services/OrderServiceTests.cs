using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Order.DataModel.Context;
using Order.Services.Services;
using Helper;
using Xunit;

public class OrderServicesTests
{
    private MicroServiceShopOrderContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<MicroServiceShopOrderContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new MicroServiceShopOrderContext(options);
    }
    [Fact]
    public async Task CreateOrderForUser_Should_Create_Draft_Order_When_No_Open_Order_Exists()
    {
        // Arrange
        var context = CreateContext();
        var service = new OrderServices(context);
        var userId = 15;

        // Act
        var result = await service.CreateOrderForUser(userId);

        // Assert
        result.isSuccess.Should().BeTrue();

        var orderInDb = await context.Orders
            .FirstOrDefaultAsync(x => x.UserId == userId);

        orderInDb.Should().NotBeNull();
        orderInDb.Status.Should().Be(OrderStatus.Draft.ToString());
        orderInDb.TotalPrice.Should().Be(0);
        orderInDb.Finalized.Should().BeFalse();
        orderInDb.Revoked.Should().BeFalse();
    }
    [Fact]
    public async Task CreateOrderForUser_Should_Not_Create_New_Order_When_Open_Order_Exists()
    {
        // Arrange
        var context = CreateContext();

        context.Orders.Add(new Order.DataModel.Models.Order
        {
            UserId = 10,
            Status = null
        });

        await context.SaveChangesAsync();

        var service = new OrderServices(context);

        // Act
        var result = await service.CreateOrderForUser(10);

        // Assert
        result.isSuccess.Should().BeTrue();

        var ordersCount = await context.Orders.CountAsync(x => x.UserId == 10);

        ordersCount.Should().Be(1);
    }
}