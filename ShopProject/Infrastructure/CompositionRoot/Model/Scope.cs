using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Infrastructure.CompositionRoot.Model
{
    public class Scope
    {
        internal Dictionary<Type, object> Instances { get; } = new();
    }
}
