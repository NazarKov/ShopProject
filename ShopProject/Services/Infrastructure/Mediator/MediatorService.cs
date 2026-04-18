using ShopProject.Model.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopProject.Services.Infrastructure.Mediator
{
    public static class MediatorService
    {
        internal static MediatorNavigation Navigation { get; } = new MediatorNavigation();
        private static MediatorNotifications Notifications { get; } = new MediatorNotifications();
        private static MediatorEvent Events { get; } = new MediatorEvent();

        public static void AddNavigation(NavigationButton navigationButton, Action callback)
        {
            Navigation.Add(navigationButton.ToString(), callback);
        }

        public static void ExecuteNavigation(NavigationButton navigationButton)
        {
            Navigation.Execute(navigationButton.ToString());
        }

        public static void AddHandlerNotifications<T>(Action<T> handler)
        {
            Notifications.Subscribe<T>(typeof(T),  handler);
        }

        public static void PublishNotifications<T>(T message)
        {
            Notifications.Publish(message);
        }

        public static void AddHandlerNotificationsAsync<T>(Func<T, Task> handler)
        {
            Notifications.Subscribe<T>(typeof(T), handler);
        }

        public static async Task PublishNotificationsAsync<T>(T message)
        {
             await Notifications.PublishAsync(message);
        }

        public static void AddEvent(string eventName, Action callback)
        {
            Events.Add(eventName, callback);
        }
        public static void AddEvent<T>(string eventName, Action<T> callback)
        {
            Events.Add(eventName, callback);
        }
        public static void ExecuteEvent(string eventName)
        {
            Events.Execute(eventName, null);
        }
        public static void ExecuteEvent<T>(string eventName, T payload)
        {
            Events.Execute(eventName, payload);
        }

        public static void AddEventAsync(string eventName, Func<Task> callback)
        {
            Events.AddAsync(eventName, callback);
        }
        public static void AddEventAsync<T>(string eventName, Func<T, Task> callback)
        {
            Events.AddAsync(eventName, callback);
        }
        public static async Task ExecuteEventAsync(string eventName)
        {
            await Events.ExecuteAsync(eventName);
        }
        public static async Task ExecuteEventAsync<T>(string eventName, T payload)
        {
            await Events.ExecuteAsync(eventName, payload);
        }
    }
}
