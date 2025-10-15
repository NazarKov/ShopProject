namespace ShopProjectWebServer.Api.DtoModels.User
{
    public class AuthorizationUserDto
    {
        public string Login { get; set; } = string.Empty; 
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string TIN { get; set; } = string.Empty;
        public bool AutomaticLogin { get; set; } 
        public int? UserRoleID { get; set; } 
        public string Token { get; set; } = string.Empty; 
    }
}
