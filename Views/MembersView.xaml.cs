using AITUC.Models;
using AITUC.ViewModels;
using System.Windows.Controls;

namespace AITUC.Views
{
    /// <summary>
    /// Interaction logic for MembersView.xaml
    /// </summary>
    public partial class MembersView : UserControl
    {
        public MembersView()
        {
            InitializeComponent();

        }
        public MembersView(Users users)
        {
            InitializeComponent();

            DataContext = new MembersViewModel(users);
        }
    }
}
