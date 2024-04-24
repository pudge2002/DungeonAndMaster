using LightningMcQueen.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace LightningMcQueen.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Window;
        public MainWindow()
        {
            InitializeComponent();
            Window=this;
        }
        private void MovingWindow(object sender, RoutedEventArgs e)
        {
            if(Mouse.LeftButton==MouseButtonState.Pressed) 
            {
                MainWindow.Window.DragMove();
            }
        }
    }
}
