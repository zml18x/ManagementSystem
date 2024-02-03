namespace SpaManagementSystem.Infrastructure.Dto
{
    public class JwtDto
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public string Role { get; set; }

        public JwtDto(string token, DateTime expires)
        {
            Token = token;
            Expires = expires;
        }
    }
}