namespace EagleTech_Task.Domain.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public Guid? ManagerId { get; set; }
        public virtual User? Manager { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
