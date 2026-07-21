using ShopProject.Core.Mvvm; 

namespace ShopProject.Model.UI.User
{
    internal class UserSelectItemModel : Model<UserSelectItemModel>
    {
        public UserModel User { get; set; }
        private bool _isActive { get; set; }
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; OnPropertyChanged(nameof(IsActive)); }
        }
    }
}
