using ShopProject.Core.Mvvm;  

namespace ShopProject.Model.UI.OperationRecorder
{
    internal class OperationRecorderSelectItemModel : Model<OperationRecorderSelectItemModel>
    {
        public OperationRecorderModel OperationRecorder { get; set; }
        private bool _isActive { get; set; }
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; OnPropertyChanged(nameof(IsActive)); }
        }
    }
}
