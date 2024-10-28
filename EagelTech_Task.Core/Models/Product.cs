namespace EagleTech_Task.Domain.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string BrandName { get; set; }
        public float Price { get; set; }
        public virtual ICollection<OrderDetail> Orders { get; set; }
    }
}
