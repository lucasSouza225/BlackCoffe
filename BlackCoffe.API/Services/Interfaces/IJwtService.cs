using BlackCoffe.API.DTOs;

namespace BlackCoffe.API.Services.Interfaces;

public interface IJwtService
{
    string GenerateToken(UserDto user);
}