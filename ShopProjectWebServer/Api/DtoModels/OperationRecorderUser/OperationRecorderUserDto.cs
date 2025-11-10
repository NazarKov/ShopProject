using ShopProjectDataBase.Entities;
<<<<<<< HEAD
using ShopProjectWebServer.Api.DtoModels.OperationRecorder;
using ShopProjectWebServer.Api.DtoModels.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
=======
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4

namespace ShopProjectWebServer.Api.DtoModels.OperationRecorderUser
{
    public class OperationRecorderUserDto
    { 
<<<<<<< HEAD
        [JsonPropertyName("User")]
        public UserDto? User { get; set; }
        [JsonPropertyName("OpertionsRecorders")]
        public IEnumerable<OperationRecorderDto>? OpertionsRecorders { get; set; }
=======
        public Guid ID { get; set; } 
        public Guid? UserID { get; set; }
        public int? OpertionsRecordersID { get; set; }
>>>>>>> 306da6b87d87ea969d9567c60bf1dbf9a079baf4
    }
}
