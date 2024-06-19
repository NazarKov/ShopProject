using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniServiceSigningFiles.Helpers.TcpJsonRCP
{
    public class UserCommand
    {
        public TypeCommand TypeCommand { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string PathKey { get; set; }
        public string PasswordKey { get; set; }
        public DateTime Time { get; set; }

        public string ToJson()
        {
            var options = new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            return JsonSerializer.Serialize(this,options);
        }

        public static UserCommand FromJson(string json)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            return JsonSerializer.Deserialize<UserCommand>(json,options);
        }
    }
}
