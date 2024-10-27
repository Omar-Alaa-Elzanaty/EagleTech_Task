namespace EagleTech_Task.Domain.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string HashPassword { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
