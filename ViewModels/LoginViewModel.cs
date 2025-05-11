using AITUC.Interface;
using AITUC.Models;
using AITUC.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Microsoft.Data.Sqlite;
using System.Windows;
using System.Windows.Controls;

namespace AITUC.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        public event Action<Users>? LoginSucceeded;

        private readonly string connectionString = "Data Source=membership.db";

        [ObservableProperty]
        private string username = string.Empty;

        private readonly Window _window;

        public string Password { get; internal set; }

        public LoginViewModel(Window window)
        {
            _window = window;
        }
        public LoginViewModel() { }
     [RelayCommand]
        private void Login(PasswordBox passwordBox)
        {
            using var conn = new SqliteConnection(connectionString);
            conn.Open();

            using var cmd = new SqliteCommand("SELECT * FROM Users WHERE Username = @u AND Password = @p", conn);
            cmd.Parameters.AddWithValue("@u", Username);
            cmd.Parameters.AddWithValue("@p", passwordBox.Password);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var user = new Users
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Username = reader.GetString(reader.GetOrdinal("Username")),
                    Password= reader.GetString(reader.GetOrdinal("Password")),
                    Role = reader.GetString(reader.GetOrdinal("Role")),
                    CanRead = reader.GetBoolean(reader.GetOrdinal("CanRead")),
                    CanWrite = reader.GetBoolean(reader.GetOrdinal("CanWrite")),
                };

                Application.Current.Dispatcher.Invoke(() =>
                {
                    var mainWindow = new MainWindow(user);
                    mainWindow.Show();
                    _window?.Close();
                });
            }
            else
            {
                MessageBox.Show("Invalid username or password", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
