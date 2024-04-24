using Prism.Mvvm;
using Alturos.Yolo;
using System.Windows.Documents;
using Alturos.Yolo.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;


using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using LightningMcQueen.Core;


namespace LightningMcQueen.ViewModels
{
    class MainWindowViewModel : ObservableObject
    {
        public RelayCommand ControlViewCommand { get; set; }
        public RelayCommand CamsViewCommand { get; set; }
        public ControlViewModel ControlVM { get; set; }
        public CamsViewModel CamsVM { get; set; }
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
            CurrentView = ControlVM;
            ControlViewCommand = new RelayCommand(o =>
            {
                CurrentView = ControlVM;
            });
            CamsViewCommand = new RelayCommand(o =>
            {
                CurrentView = CamsVM;
            });
        }   
    }
}
