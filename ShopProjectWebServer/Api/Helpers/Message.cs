using System.Text.Json;

namespace ShopProjectWebServer.Api.Helpers
{
    public class Message
    {
        public string MessageBody { get; set; }
        public TypeMessage Type { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
