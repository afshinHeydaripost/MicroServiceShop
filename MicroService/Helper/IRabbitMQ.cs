﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public interface IRabbitMQ
    {
        Task<GeneralResponse> SendProductMessage<TRequest>(TRequest item,string queueName);
    }
}
