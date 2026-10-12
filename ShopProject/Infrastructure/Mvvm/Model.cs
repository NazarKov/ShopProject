using System.ComponentModel; 

namespace ShopProject.Infrastructure.Mvvm
{
    public abstract class Model<T> : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged; 
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
