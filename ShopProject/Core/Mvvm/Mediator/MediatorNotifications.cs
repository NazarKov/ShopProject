using System;
using System.Collections.Generic; 
using System.Threading.Tasks;

namespace ShopProject.Core.Mvvm.Mediator
{
    internal class MediatorNotifications
    {
        private readonly Dictionary<Type, List<Delegate>> _handlers = new();
        public void Subscribe<T>(Type type, Delegate handler)
        { 
            if (!_handlers.ContainsKey(type))
                _handlers[type] = new List<Delegate>();

            _handlers[type].Add(handler);
        }

        public void Publish<T>(T message)
        {
            var type = typeof(T);

            if (!_handlers.ContainsKey(type))
                return;

            foreach (var handler in _handlers[type])
            {
                ((Action<T>)handler)(message);
            }
        }

        public async Task PublishAsync<T>(T message)
        {
            if (!_handlers.ContainsKey(typeof(T)))
                return;

            var handlers = _handlers[typeof(T)];

            foreach (var handler in handlers)
            {
                switch (handler)
                {
                    case Func<T, Task> asyncHandler:
                        await asyncHandler(message);
                        break;
                    case Action<T> syncHandler:
                        syncHandler(message);
                        break;
                }
            }
        }

    }
}
