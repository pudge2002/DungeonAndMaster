using LightningMcQueen.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace LightningMcQueen.Base
{
    public class BaseModule : IModule
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry
                .RegisterForNavigation<ControlView>();
            containerRegistry
                .RegisterForNavigation<CamsView>();
            containerRegistry
               .RegisterForNavigation<HistoryView>();
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider
                .Resolve<IRegionManager>()
                .RegisterViewWithRegion("MainRegion", nameof(ControlView));

        }
    }
}
