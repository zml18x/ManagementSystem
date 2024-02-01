namespace SpaManagementSystem.Core.Models
{
    public class Role
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }



        public Role(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}