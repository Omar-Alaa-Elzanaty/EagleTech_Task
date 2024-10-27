using EagleTech_Task.Application.Dtos;

namespace EagleTech_Task.Application.Interfaces.Auth
{
    public interface IAuthServices
    {
        string GenerateToken(AuthDto authDto);
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
