
using System.Windows;
using AITUC.Models;
using AITUC.ViewModels;
using Fluent;

namespace AITUC.Views
{
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow(Users user)
        {
            InitializeComponent();
            DataContext = new MainViewModel(user);
        }
    }
}
