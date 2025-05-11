using AITUC.Models;
using AITUC.ViewModels;
using AITUC.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
namespace AITUC.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private Users? currentUser;

        public ObservableCollection<TabItemViewModel> OpenTabs { get; set; } = new();
        public TabItemViewModel? SelectedTab { get; set; }

        public ICommand NavigateCommand { get; }
        public ICommand CloseTabCommand { get; }

        public bool CanRead => CurrentUser?.CanRead ?? true;
        public bool IsAdmin => CurrentUser?.Role == "admin";

        public MainViewModel()
            : this(new Users { Username = "Guest", Role = "guest", CanRead = true }) { }

        public MainViewModel(Users users)
        {
            CurrentUser = users;

            NavigateCommand = new RelayCommand<string>(NavigateTo);
            CloseTabCommand = new RelayCommand<TabItemViewModel>(CloseTab);

            NavigateTo("MembersView");
        }

        private void NavigateTo(string? viewName)
        {
            if (string.IsNullOrWhiteSpace(viewName))
                viewName = "MembersView";

            var existing = OpenTabs.FirstOrDefault(t => t.ViewName == viewName);
            if (existing != null)
            {
                SelectedTab = existing;
                return;
            }

            var newTab = new TabItemViewModel
            {
                Title = viewName,
                ViewName = viewName,
                Content = viewName switch
                {
                    "MembersView" => new MembersView(),
                    "UsersView" => new UserView(),
                    
                }
            };

            OpenTabs.Add(newTab);
            SelectedTab = newTab;
        }

        private void CloseTab(TabItemViewModel tab)
        {
            if (OpenTabs.Contains(tab))
                OpenTabs.Remove(tab);
        }
    }
}