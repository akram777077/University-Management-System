using Domain.Enums;

namespace Applications.DTOs.Users;

public record struct UpdateUserRoleRequest
{
    public UserRole? Role { get; set; }
}