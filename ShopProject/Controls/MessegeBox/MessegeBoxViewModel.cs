using ShopProject.Controls.MessegeBox.Enum;
using ShopProject.Core.Mvvm;

namespace ShopProject.Controls.MessegeBox
{
    public class MessegeBoxViewModel : ViewModel<MessegeBoxViewModel>
    {
        public string Title { get; }
        public string Message { get; }
        public MessageBoxType Type { get; }

        public bool IsQuestion => Type == MessageBoxType.Question;

        public string Icon
        {
            get
            {
                return Type switch
                {
                    MessageBoxType.Success => "✓",
                    MessageBoxType.Warning => "!",
                    MessageBoxType.Error => "×",
                    MessageBoxType.Question => "?",
                    _ => "i"
                };
            }
        }
        public string ColorIcon
        {
            get
            {
                return Type switch
                {
                    MessageBoxType.Success => "#4CAF50",
                    MessageBoxType.Warning => "#ffcc00",
                    MessageBoxType.Error => "#6c0e26",
                    MessageBoxType.Question => "#115697",
                    _ => "i"
                };
            }
        }
        public string ColorIconBackground
        {
            get
            {
                return Type switch
                {
                    MessageBoxType.Success => "#E8F5E9",
                    MessageBoxType.Warning => "#fff0b3",
                    MessageBoxType.Error => "#c69a9a",
                    MessageBoxType.Question => "#9ab1c6",
                    _ => "i"
                };
            }
        }

        public MessegeBoxViewModel(
            string title,
            string message,
            MessageBoxType type)
        {
            Title = title;
            Message = message;
            Type = type;
        }
    }
}
