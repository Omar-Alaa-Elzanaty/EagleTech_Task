using EagleTech_Task.Domain.Constant;

namespace EagleTech_Task.Domain.Models
{
    public class Order:BaseEntity
    {
        public float TotalCost { get; set; }
        public Guid CustomerId { get; set; }
        public OrderStatus Status { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual List<OrderDetail> Details { get; set; }
    }
}
