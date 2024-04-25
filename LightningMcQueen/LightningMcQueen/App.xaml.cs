﻿using LightningMcQueen.ViewModels;
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
            containerRegistry.RegisterSingleton<ControlViewModel>();
            containerRegistry.RegisterSingleton<CamsViewModel>();
            containerRegistry.RegisterSingleton<AboutViewModel>();
        }

    }
}
