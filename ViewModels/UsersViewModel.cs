using AITUC.Interface;
using AITUC.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Google.Apis.Drive.v3.Data;
using Microsoft.Data.Sqlite;
using System.Collections.ObjectModel;
using System.Windows;

namespace AITUC.ViewModels
{
    public partial class UsersViewModel : ObservableObject
    {
        
        private readonly Users _loggedInUser;

        public ObservableCollection<Users> UsersA { get; } = new();

        public UsersViewModel()
        {
            
        }
        public UsersViewModel(Users loggedInUser)
        {
              _loggedInUser = loggedInUser;

            if (_loggedInUser.Role!="admin")
            {
                MessageBox.Show("Only admins can view or manage users.", "Access Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            LoadUsers();
        }
       

        private void LoadUsers()
        {
            using var conn = new SqliteConnection("Data Source=membership.db");
            conn.Open();

            using var cmd = new SqliteCommand("SELECT Id, Username, Password, Role, CanRead, CanWrite FROM Users;", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                UsersA.Add(new Users
                {
                    Id = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    Role = reader.GetString(2),
                    CanRead = reader.GetBoolean(3),
                    CanWrite = reader.GetBoolean(4)
                });
            }
        }

        [RelayCommand]
        private void Save()
        {
            using var conn = new SqliteConnection("Data Source=membership.db");
            conn.Open();

            foreach (var user in UsersA)
            {
                using var cmd = new SqliteCommand("UPDATE Users SET CanRead = @r, CanWrite = @w WHERE UserId = @id", conn);
                cmd.Parameters.AddWithValue("@r", user.CanRead ? 1 : 0);
                cmd.Parameters.AddWithValue("@w", user.CanWrite ? 1 : 0);
                cmd.Parameters.AddWithValue("@id", user.Id);
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("User permissions updated.");
        }
    }
}
