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

        // Personal Information
        [ObservableProperty] private string memberName;
        [ObservableProperty] private string memberAddress;
        [ObservableProperty] private string memberArea;
        [ObservableProperty] private string memberAreaCode;
        [ObservableProperty] private string memberMobile;
        [ObservableProperty] private string memberEducation;
        [ObservableProperty] private int memberSon;
        [ObservableProperty] private int memberDaughter;
        [ObservableProperty] private int memberWorkYear;
        [ObservableProperty] private string memberWorkDescription;
        [ObservableProperty] private string memberBPLCard; 
        [ObservableProperty] private string memberRationCard; 
        [ObservableProperty] private string memberWorkPlace;

        // Membership Details
        [ObservableProperty] private DateTime? newMembershipStartDate = DateTime.Today;
        [ObservableProperty] private DateTime? newMembershipEndDate = DateTime.Today.AddYears(1);
        [ObservableProperty] private bool newMembershipIsActive = true;
        [ObservableProperty] private DateTime? newMembershipRenewedOn;
        [ObservableProperty] private decimal newMembershipFees;
        [ObservableProperty] private string newMembershipPaidWith;



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
            var member = new Member
            {
                MemberName = memberName,
                Address = memberAddress,
                Area = memberArea,
                AreaCode = memberAreaCode,
                Mobile = memberMobile,
                Education = memberEducation,
                Son = memberSon,
                Daughter = memberDaughter,
                WorkYear = memberWorkYear,
                WorkDescription = memberWorkDescription,
                BPLCard = memberBPLCard,
                RationCard = memberRationCard,
                WorkPlaces = memberWorkPlace,
               
                
            };

            Members.Add(member);
            ClearForm();
        }
        private void ClearForm()
        {
            memberName = string.Empty;
            memberAddress = string.Empty;
            memberArea = string.Empty;
            memberAreaCode = string.Empty;
            memberMobile = string.Empty;
            memberEducation = string.Empty;
            memberSon = 0;
            memberDaughter = 0;
            memberWorkYear = 0;
            memberWorkDescription = string.Empty;
            memberBPLCard = string.Empty;
            memberRationCard = string.Empty;
            memberWorkPlace = string.Empty;

            newMembershipStartDate = DateTime.Today;
            newMembershipEndDate = DateTime.Today.AddYears(1);
            newMembershipIsActive = true;
            newMembershipRenewedOn = null;
            newMembershipFees = 0;
            newMembershipPaidWith = string.Empty;
        }
    }
}
