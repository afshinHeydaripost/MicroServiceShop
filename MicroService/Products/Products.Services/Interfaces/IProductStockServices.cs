using Helper;
using Helper.VieModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Services.Interfaces;

public interface IProductStockServices
{
        Task<GeneralResponse> Create(ProductStockViewModel item);
    }

