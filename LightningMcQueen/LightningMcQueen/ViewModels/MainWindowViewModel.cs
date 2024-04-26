using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using ReactiveUI;
using System.Windows;
using System.Windows.Input;


namespace LightningMcQueen.ViewModels
{
    class MainWindowViewModel : BindableBase
    {
        
        public ICommand ControlViewCommand { get; set; }
        public ICommand CamsViewCommand { get; set; }
        public ICommand AboutViewCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand DragMoveCommand { get; set; }
        public MainWindowViewModel(IRegionManager regionManager)
        {
           
            ControlViewCommand = new DelegateCommand(() =>
            {
                regionManager.RequestNavigate("MainRegion", "ControlView");
            });
            CamsViewCommand = new DelegateCommand(() =>
            {
                regionManager.RequestNavigate("MainRegion", "CamsView");
            });
            AboutViewCommand = new DelegateCommand(() =>
            {
                regionManager.RequestNavigate("MainRegion", "AboutView");
            });
            CloseCommand = ReactiveCommand.Create(Application.Current.Shutdown);
            DragMoveCommand = ReactiveCommand.Create(Application.Current.MainWindow!.DragMove);
        }   
    }
}
