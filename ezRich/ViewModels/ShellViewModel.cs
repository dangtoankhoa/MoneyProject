using Prism.Mvvm;

namespace ezRich.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private string _title = "ezRich";
        private string _tooltip = "Your completed finance solution!";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public string Tooltip
        {
            get { return _tooltip; }
            set { SetProperty(ref _tooltip, value); }
        }


        public ShellViewModel()
        {

        }
    }
}
