using LightningMcQueen.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace LightningMcQueen.About;

public class AboutModule : IModule
{
    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry
            .RegisterForNavigation<AboutView>();
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {

    }
}
