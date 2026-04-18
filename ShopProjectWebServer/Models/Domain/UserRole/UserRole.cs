namespace ShopProjectWebServer.Models.Domain.UserRole
{
    public class UserRole
    {
        public int ID { get; set; } 
        public string NameRole { get; set; } = string.Empty; 
        public int TypeAccess { get; set; } = 0;
    }
}
