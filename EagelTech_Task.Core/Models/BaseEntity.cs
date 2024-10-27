using EagleTech_Task.Domain.Interfaces;

namespace EagleTech_Task.Domain.Models
{
    public class BaseEntity:IAuditable
    {
        public string Id { get; set; }
        public DateTime CreationDate { get; set ; }
    }
}
