using EagleTech_Task.Application.Features.Orders.Queries.GetOrdersByCustomerId;
using EagleTech_Task.Domain.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleTech_Task.Application.Comman.Mapping
{
    public class OrderDetailMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<OrderDetail, GetOrdersDetailsByCustomerIdQueryDto>()
                .Map(dest => dest.Price, src => src.Product.Price)
                .Map(dest => dest.Name, src => src.Product.Name)
                .Map(dest => dest.BrandName, src => src.Product.BrandName);
        }
    }
}
