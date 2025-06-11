using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Entities
{
    /// <summary>
    /// System user account linking a person to system access with role-based permissions
    /// </summary>
    public class User : IEntity
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public UserRole Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
     
        public int PersonId { get; set; }
        public Person Person { get; set; } = null!;
     }
}
