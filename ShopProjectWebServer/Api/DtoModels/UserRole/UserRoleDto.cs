namespace ShopProjectWebServer.Api.DtoModels.UserRole
{
    public class UserRoleDto
    {
        public int ID { get; set; }
        public string NameRole { get; set; } = string.Empty; 
        public int TypeAccess { get; set; } = 0;
    }
}
