using Prism.Mvvm;

namespace LightningMcQueen.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        string Image {  get; set; }

        public MainWindowViewModel()
        {

        }
    }
}
