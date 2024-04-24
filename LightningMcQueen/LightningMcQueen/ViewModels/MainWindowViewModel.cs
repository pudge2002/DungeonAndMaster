using LightningMcQueen.Core;


namespace LightningMcQueen.ViewModels
{
    class MainWindowViewModel : ObservableObject
    {
        public RelayCommand ControlViewCommand { get; set; }
        public RelayCommand CamsViewCommand { get; set; }
        public RelayCommand AboutViewCommand { get; set; }
        public ControlViewModel ControlVM { get; set; }
        public CamsViewModel CamsVM { get; set; }
        public AboutViewModel AboutVM { get; set; }
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value; 
                OnPropertyChanged(); 
            }
        }

        public MainWindowViewModel()
        {
            ControlVM= new ControlViewModel();
            CamsVM= new CamsViewModel();
            AboutVM= new AboutViewModel();
            CurrentView = ControlVM;
            ControlViewCommand = new RelayCommand(o =>
            {
                CurrentView = ControlVM;
            });
            CamsViewCommand = new RelayCommand(o =>
            {
                CurrentView = CamsVM;
            });
            AboutViewCommand = new RelayCommand(o =>
            {
                CurrentView = AboutVM;
            });
        }   
    }
}
