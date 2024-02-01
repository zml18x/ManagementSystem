namespace SpaManagementSystem.Core.Models
{
    public class UserRole
    {
        public Guid Id { get; protected set; }
        public Guid UserId { get; protected set; }
        public Guid RoleId { get; protected set; }



        public UserRole(Guid id, Guid userId, Guid roleId)
        {
            Id = id;
            UserId = userId;
            RoleId = roleId;
        }
    }
}