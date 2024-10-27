namespace EagleTech_Task.Application.Dtos
{
    public class AuthDto
    {
        public AuthDto(string userId, string userName, string email, ICollection<string> roles, bool remeberMe)
        {
            UserId = userId;
            UserName = userName;
            Email = email;
            Roles = roles;
            RemeberMe = remeberMe;
        }

        public string UserId { get; }
        public string UserName { get; }
        public string Email { get; }
        public ICollection<string> Roles { get; }
        public bool RemeberMe { get; }
    }
}
