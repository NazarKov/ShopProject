using System.Text.Json;

namespace TcpJsonRPC
{
    public class CommandUser
    {
        public TypeCommand TypeCommand { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
        public string? PathKey { get; set; }
        public string? PasswordKey { get; set; }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }

        public static CommandUser FromJson(string json)
        {
            return JsonSerializer.Deserialize<CommandUser>(json);
        }
    }
}
