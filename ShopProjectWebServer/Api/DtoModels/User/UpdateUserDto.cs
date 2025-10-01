using ShopProjectDataBase.Entities;

namespace ShopProjectWebServer.Api.DtoModels.User
{
    public class UpdateUserDto
    {
        public Guid ID { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string TIN { get; set; } = string.Empty;
        public bool AutomaticLogin { get; set; }
        public int Status { get; set; }
        public int UserRole_ID { get; set; }
        public ElectronicSignatureKey? SignatureKey { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
    }
}
