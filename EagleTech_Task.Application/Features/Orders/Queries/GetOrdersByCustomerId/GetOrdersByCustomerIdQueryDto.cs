using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleTech_Task.Application.Features.Orders.Queries.GetOrdersByCustomerId
{
    public class GetOrdersByCustomerIdQueryDto
    {
        public Guid Id { get; set; }
        public float TotalCost { get; set; }
        public List<GetOrdersDetailsByCustomerIdQueryDto> Details { get; set; }
    }

    public class GetOrdersDetailsByCustomerIdQueryDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string BrandName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
