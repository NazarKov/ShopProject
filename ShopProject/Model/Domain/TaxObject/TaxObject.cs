using ShopProject.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopProject.Model.Domain.TaxObject
{
    public class TaxObject
    {
        public Guid ID { get; set; }
        public string NameOwner { get; set; } = string.Empty;
        public string TypeObjectName { get; set; } = string.Empty;
        public string NameObject { get; set; } = string.Empty;
        public string CodeObject { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public TypeStatusTaxObject TypeStatus { get; set; }
        [JsonIgnore]
        public string TypeOfRights { get; set; } = string.Empty;
        [JsonIgnore]
        public DateTimeOffset? D_ACC_START { get; set; }
        [JsonIgnore]
        public DateTimeOffset? D_ACC_END { get; set; }
        [JsonIgnore]
        public string C_DISTR { get; set; } = string.Empty;
        [JsonIgnore]
        public DateTimeOffset? D_LAST_CH { get; set; }
        [JsonIgnore]
        public string C_TERRIT { get; set; } = string.Empty;
        [JsonIgnore]
        public string? REG_NUM_OBJ { get; set; }

        [JsonIgnore]
        public string KATOTTG { get; set; } = string.Empty;
        [JsonIgnore]
        public bool LoadTaxServer { get; set; } = false;
    }
}
