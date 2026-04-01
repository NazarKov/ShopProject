using FiscalServerApi.Helpers;
using ShopProject.Core.Mvvm;
using ShopProject.Core.Mvvm.Command;
using ShopProject.Core.Mvvm.Mediator.Interface;
using ShopProject.Core.Mvvm.Mediator.Notifications; 
using ShopProject.Core.Mvvm.Service;
using ShopProject.Model.Domain.Notification;
using ShopProject.Model.Enum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShopProject.ViewModel.HomePage.HomePageComponent
{
    internal class NotificationViewModel :ViewModel<NotificationViewModel>
    { 

        private ICommand _removeNotificationCommand;

        public NotificationViewModel()
        { 

            _removeNotificationCommand = CreateCommandParameterAsync(async (object obj) => await RemoveNotification(obj as Notification));

            _notifications = new ObservableCollection<Notification>();
            _notification = new Notification();
            _background = "#F5F5F5";
            _backgroundNotification = "Transparent";

            _notificationsVisibility = Visibility.Collapsed;
            _notificationVisibility = Visibility.Collapsed;

            MediatorService.AddHandlerNotificationsAsync<ShowNotificationEvent>(ShowNotification);
            MediatorService.AddEventAsync("ShowNotifications", ShowNotifications);
        }

        private string _background;
        public string Background
        {
            get { return _background; }
            set { _background = value; OnPropertyChanged(nameof(Background)); }
        }

        private string _backgroundNotification;
        public string BackgroundNotification
        {
            get { return _backgroundNotification; }
            set { _backgroundNotification = value; OnPropertyChanged(nameof(BackgroundNotification)); }
        }

        private ObservableCollection<Notification> _notifications;
        public ObservableCollection<Notification> Notifications
        {
            get { return _notifications; }
            set { _notifications = value; }
        }

        private Notification _notification;
        public Notification Notification
        {
            get { return _notification; }
            set { _notification = value; OnPropertyChanged(nameof(Notification)); }
        }

        private Visibility _notificationsVisibility;
        public Visibility NotificationsVisibility
        {
            get { return _notificationsVisibility; }
            set { _notificationsVisibility = value; OnPropertyChanged(nameof(NotificationsVisibility)); }
        }

        private Visibility _notificationVisibility;
        public Visibility NotificationVisibility
        {
            get { return _notificationVisibility; }
            set { _notificationVisibility = value; OnPropertyChanged(nameof(NotificationVisibility)); }
        }

        public async Task ShowNotification(ShowNotificationEvent e)
        {
            if(e.Notification is Notification not)
            {
                if (not.IsVisible)
                {
                    Notification = not;
                    NotificationsVisibility = Visibility.Collapsed;
                    NotificationVisibility = Visibility.Visible;
                    Background = "Transparent";
                    BackgroundNotification= "#F5F5F5";
                    await MediatorService.ExecuteEventAsync("VisibilitiNotification");


                    if (not.NotificationPersistence != NotificationPersistence.None)
                    {
                        Notifications.Insert(0, not);
                        await MediatorService.ExecuteEventAsync("AddNotificationCount", Notifications.Count);

                    }
                }
                else
                {
                    if (not.NotificationPersistence != NotificationPersistence.None)
                    {
                        Notifications.Insert(0, not);
                        await MediatorService.ExecuteEventAsync("AddNotificationCount", Notifications.Count);

                    }
                }
            }
            else
            {
                if (e.Notification is INotification n)
                {
                    var notification = new Notification()
                    {
                        Content = n.Content,
                        Title = n.Title,
                        DateTime = DateTime.Now,
                        IsVisible = true,
                        NotificationPersistence = NotificationPersistence.Persistent,
                        Type = TypeNotification.Error
                    };
                    Notification = notification;
                    NotificationsVisibility = Visibility.Collapsed;
                    NotificationVisibility = Visibility.Visible;
                    BackgroundNotification = "#F5F5F5";
                    Background = "Transparent";
                    await MediatorService.ExecuteEventAsync("VisibilitiNotification");

                    Notifications.Insert(0, notification);
                    await MediatorService.ExecuteEventAsync("AddNotificationCount", Notifications.Count);
                }
            } 
        }

        public async Task ShowNotifications()
        {
            BackgroundNotification = "Transparent";
            Background = "#F5F5F5";
            NotificationVisibility = Visibility.Collapsed;
            NotificationsVisibility = Visibility.Visible;
        }

        public ICommand RemoveNotificationCommnad => _removeNotificationCommand;

        private async Task RemoveNotification(Notification notification)
        { 
            if (notification != null)
                    Notifications.Remove(notification);

            if (Notifications.Count() == 0)
            {
                NotificationsVisibility = Visibility.Collapsed;
                Background = "Transparent";
            }
            await MediatorService.ExecuteEventAsync("AddNotificationCount", Notifications.Count);
        }


    }
}
