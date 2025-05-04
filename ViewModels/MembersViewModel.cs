using AITUC.Data;
using AITUC.Interface;
using AITUC.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AITUC.ViewModels
{
    public partial class MembersViewModel : ObservableObject
    {
       private readonly Users loggedInUser;

        public ObservableCollection<Member> Members { get; } = new();

        [ObservableProperty] private string newMemberName;
        [ObservableProperty] private string? newMemberAddress;



        public bool CanWrite => loggedInUser?.CanWrite ?? false;

        public MembersViewModel(Users user)
        {
            this.loggedInUser = user;

            LoadMembers();
        }

        public MembersViewModel()
        {
           
        }

        private void LoadMembers()
        {
            using var db = new AppDbContext();
            var members = db.Members.ToList();
            Members.Clear();
            foreach (var member in members)
                Members.Add(member);
        }

        [RelayCommand(CanExecute = nameof(CanWrite))]
        private void AddMember()
        {
            if (string.IsNullOrWhiteSpace(NewMemberName)) return;

            var newMember = new Member
            {
                MemberName = NewMemberName,
                Address = NewMemberAddress
            };

            using var db = new AppDbContext();
            db.Members.Add(newMember);
            db.SaveChanges();

            Members.Add(newMember);

            NewMemberName = string.Empty;
            NewMemberAddress = string.Empty;
        }
    }
}
