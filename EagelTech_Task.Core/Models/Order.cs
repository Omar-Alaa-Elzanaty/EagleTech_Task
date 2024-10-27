namespace EagleTech_Task.Domain.Models
{
    public class Order:BaseEntity
    {
        public float TotalCost { get; set; }
        public List<Product> Products { get; set; }
    }
}
