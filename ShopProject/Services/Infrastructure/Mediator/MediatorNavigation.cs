using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Services.Infrastructure.Mediator
{
    internal class MediatorNavigation
    {
        private IDictionary<string, List<Action>> pl_dict =
           new Dictionary<string, List<Action>>();

        public void Add(string token, Action callback)
        {
            if (!pl_dict.ContainsKey(token))
            {
                var list = new List<Action>();
                list.Add(callback);
                pl_dict.Add(token, list);
            }
            else
            {
                bool found = false;
                foreach (var item in pl_dict[token])
                    if (item.Method.ToString() == callback.Method.ToString())
                        found = true;
                if (!found)
                    pl_dict[token].Add(callback);
            }
        }

        public void Remove(string token, Action callback)
        {
            if (pl_dict.ContainsKey(token))
                pl_dict[token].Remove(callback);
        }

        public void Execute(string token)
        {
            if (pl_dict.ContainsKey(token))
                foreach (var callback in pl_dict[token])
                    callback();
        }
    }
}
