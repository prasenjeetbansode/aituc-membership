using AITUC.Data;
using AITUC.Interface;
using AITUC.Models;
using AITUC.Services;
using AITUC.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace AITUC.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private Users? currentUser;

        [ObservableProperty]
        private UserControl? currentView;

        public bool CanRead => CurrentUser?.CanRead ?? false;
        public bool IsAdmin => CurrentUser?.Role == "admin";

        public MainViewModel()
        {
        }

        [RelayCommand]
        private void Navigate(string? viewName)
        {
            if (string.IsNullOrEmpty(viewName)) return;

            switch (viewName)
            {
                case "LoginView":
                    var loginView = new LoginView();
                    if (loginView.DataContext is LoginViewModel loginVM)
                    {
                        loginVM.LoginSucceeded += OnLoginSucceeded;
                    }
                    CurrentView = loginView;
                    break;

                case "MembersView":
                    if (CanRead)
                    {
                        var view = new MembersView();
                        view.DataContext = new MembersViewModel(currentUser); // or pass user if needed
                        CurrentView = view;
                    }
                    break;

                case "UsersView":
                    if (IsAdmin)
                    {
                        var view = new UserView();
                        view.DataContext = new UsersViewModel(currentUser); // or pass user if needed
                        CurrentView = view;
                    }
                    break;
            }

            OnPropertyChanged(nameof(CanRead));
            OnPropertyChanged(nameof(IsAdmin));
        }

        private void OnLoginSucceeded(Users loggedInUser)
        {
            CurrentUser = loggedInUser;
            Navigate("MembersView");
        }
    }
}
