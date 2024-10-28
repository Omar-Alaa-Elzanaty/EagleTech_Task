namespace EagleTech_Task.Application.Dtos
{
    public class AuthDto
    {
        public AuthDto(Guid userId, string userName, string email, ICollection<string> roles)
        {
            UserId = userId;
            UserName = userName;
            Email = email;
            Roles = roles;
        }

        public Guid UserId { get; }
        public string UserName { get; }
        public string Email { get; }
        public ICollection<string> Roles { get; }
    }
}
