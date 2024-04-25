using Prism.Commands;
using Prism.Mvvm;
using System.Windows;
using System.Windows.Input;


namespace LightningMcQueen.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        public ICommand MoveWindowCommand { get; }
        public DelegateCommand ControlViewCommand { get; set; }
        public DelegateCommand CamsViewCommand { get; set; }
        public DelegateCommand AboutViewCommand { get; set; }
        public ControlViewModel ControlVM { get; set; }
        public CamsViewModel CamsVM { get; set; }
        public AboutViewModel AboutVM { get; set; }
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                SetProperty(ref _currentView, value);
            }
        }
        private void MoveWindow(Window window)
        {
            window.DragMove();
        }
        public MainWindowViewModel()
        {
            MoveWindowCommand = new DelegateCommand<Window>(MoveWindow);
            ControlVM = new ControlViewModel();
            CamsVM = new CamsViewModel();
            AboutVM = new AboutViewModel();
            CurrentView = ControlVM;
            ControlViewCommand = new DelegateCommand(() =>
            {
                CurrentView = ControlVM;
            });
            CamsViewCommand = new DelegateCommand(() =>
            {
                CurrentView = CamsVM;
            });
            AboutViewCommand = new DelegateCommand(() =>
            {
                CurrentView = AboutVM;
            });
        }   
    }
}
