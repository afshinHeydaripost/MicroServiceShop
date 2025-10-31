using Helper;
using Helper.VieModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Services.Interfaces;

public interface IProductInfoServices
{
    Task<GeneralResponse> Create(ProductInfoViewModel item);
}

