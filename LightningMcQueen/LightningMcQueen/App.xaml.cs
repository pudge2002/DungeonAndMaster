using LightningMcQueen.About;
using LightningMcQueen.Base;
using LightningMcQueen.ViewModels;
using LightningMcQueen.Views;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;

namespace LightningMcQueen
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<MainWindowViewModel>();
        }
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog
                .AddModule<AboutModule>()
                .AddModule<BaseModule>();
        }
    }
}
