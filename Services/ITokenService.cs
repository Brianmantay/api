using api.Dtos;
 
namespace api.Services
{
    public interface ITokenService
    {
        string GenerateToken(UserDto user);
    }
}