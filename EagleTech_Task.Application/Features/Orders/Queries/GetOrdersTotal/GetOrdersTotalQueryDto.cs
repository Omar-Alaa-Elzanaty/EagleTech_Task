using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleTech_Task.Application.Features.Orders.Queries.GetOrdersTotal
{
    public class GetOrdersTotalQueryDto
    {
        public Guid Id { get; set; }
        public float TotalAmount { get; set; }
    }
}
