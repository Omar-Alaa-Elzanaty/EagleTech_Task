using EagleTech_Task.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace EagleTech_Task.Domain.Models
{
    public class BaseEntity:IAuditable
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set ; }
    }
}
