using AITUC.Models;
using AITUC.ViewModels;
using AITUC.Views;
using Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AITUC
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new LoginViewModel(this);
            if (DataContext is LoginViewModel vm)
            {
                vm.LoginSucceeded += OnLoginSucceeded;
                
            }
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel viewModel)
            {
                viewModel.Password = ((PasswordBox)sender).Password;
            }
        }
        private void OnLoginSucceeded(Users user)
        {

            var mainWindow = new MainWindow(user);
            mainWindow.DataContext = new MainViewModel(user);
            mainWindow.DataContext = new MembersViewModel(user);
            mainWindow.DataContext = new UsersViewModel(user);
            this.Close();
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;  
            mainWindow.Show();

        }
    }
}
