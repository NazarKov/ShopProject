using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProject.Core.Mvvm.Mediator 
{
    internal class MediatorEvent
    {
        private readonly Dictionary<Type, List<Delegate>> _events= new();
        private readonly Dictionary<string, List<Delegate>> _eventsAsync = new();
        public void Add(string key, Delegate callback)
        {
            var typeKey = typeof(string);

            if (!_events.ContainsKey(typeKey))
                _events[typeKey] = new List<Delegate>();

            if (!_events[typeKey].Contains(callback))
                _events[typeKey].Add(callback);
        }

        public void Execute(string key, object? payload)
        { 
            var delegates = _events.Where(kvp => kvp.Key == typeof(string))  
                .SelectMany(kvp => kvp.Value)
                .ToList();
             

            foreach (var subscriber in delegates)
            {
                if (subscriber is Action action)
                    action();
                else
                    subscriber.DynamicInvoke(payload);
            }
        } 
        public void AddAsync(string eventName, Func<Task> callback)
        {
            if (!_eventsAsync.ContainsKey(eventName))
                _eventsAsync[eventName] = new List<Delegate>();

            _eventsAsync[eventName].Add(callback);
        }

        public async Task ExecuteAsync(string eventName)
        {
            if (!_eventsAsync.ContainsKey(eventName))
                return;

            var subscribers = _eventsAsync[eventName].ToArray();

            foreach (var sub in subscribers)
            {
                if (sub is Func<Task> asyncCallback)
                    await asyncCallback();
            }
        }

        public void AddAsync<T>(string eventName, Func<T, Task> callback)
        {
            if (!_eventsAsync.ContainsKey(eventName))
                _eventsAsync[eventName] = new List<Delegate>();

            _eventsAsync[eventName].Add(callback);
        }

        public async Task ExecuteAsync<T>(string eventName, T payload)
        {
            if (!_eventsAsync.ContainsKey(eventName))
                return;

            var subscribers = _eventsAsync[eventName].ToArray();

            foreach (var sub in subscribers)
            {
                if (sub is Func<T, Task> asyncCallback)
                    await asyncCallback(payload);
            }
        }

    }
}
