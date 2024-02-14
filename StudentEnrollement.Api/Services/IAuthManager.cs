using Microsoft.AspNetCore.Identity;
using StudentEnrollement.Api.DTOs.Authentication;

namespace StudentEnrollement.Api.Services
{
    public interface IAuthManager
    {
        Task<AuthResponseDto> Login(LoginDto loginDto);
        Task<IEnumerable<IdentityError>> Register(RegisterDto loginDto);
    }
}
