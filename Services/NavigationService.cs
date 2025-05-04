using AITUC.Interface;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AITUC.Services
{
    public class NavigationService : ObservableObject, INavigationService
    {
        private object _currentViewModel;
        public object CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }

        public void NavigateTo(object viewModel)
        {
            CurrentViewModel = viewModel;
        }
    }
}
