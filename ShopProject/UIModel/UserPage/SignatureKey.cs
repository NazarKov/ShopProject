using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.UIModel.UserPage
{
    public class SignatureKey
    {
        public Guid? ID { get; set; }
        public byte[]? Signature { get; set; }
        public string? SignaturePassword { get; set; }
        public DateTimeOffset CreateAt { get; set; }
        public DateTimeOffset EndAt { get; set; }
    }
}
