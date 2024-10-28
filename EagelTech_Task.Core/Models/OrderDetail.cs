using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleTech_Task.Domain.Models
{
    public class OrderDetail
    {
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        public Guid  OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int Quantity { get; set; }
    }
}
