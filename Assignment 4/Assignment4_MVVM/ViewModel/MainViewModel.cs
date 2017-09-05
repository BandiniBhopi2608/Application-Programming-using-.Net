using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using Assignment4_MVVM.Model;
using Assignment4_MVVM.Business;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Assignment4_MVVM.View;
using Assignment4_MVVM.Interface;
using System.Windows;

namespace Assignment4_MVVM.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private Member _selectedMember;
        public RelayCommand<IClosable> ExitWindowCommand { get; private set; }
        public ICommand ShowCommand { get; private set; }
        public ICommand ShowUpdateWinCommand { get; private set; }
        private MembershipAdd objMembershipAdd;
        private MembershipUpdateDelete objMemberUpdate;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            MemberList = MembershipData.GetMemberships();
            ShowCommand = new RelayCommand(ShowMethod);
            ShowUpdateWinCommand = new RelayCommand(ShowUpdateWindow);
            ExitWindowCommand = new RelayCommand<IClosable>(ExitWindow);

            Messenger.Default.Register<Member>(this, "add", SaveMember);
            Messenger.Default.Register<Member>(this, "edit", UpdateMember);
            Messenger.Default.Register<Member>(this, "delete", DeleteMember);
        }

        public ObservableCollection<Member> MemberList { get; set; }

        public Member SelectedMember
        {
            get
            {
                return _selectedMember;
            }
            set
            {
                _selectedMember = value;
            }
        }

        private void ShowMethod()
        {
            objMembershipAdd = new MembershipAdd();
            objMembershipAdd.Show();
        }

        private void ShowUpdateWindow()
        {
            if (SelectedMember != null)
            {
                objMemberUpdate = new MembershipUpdateDelete();
                objMemberUpdate.Show();
                // Send SelectedMember to MemberViewModel. MemberViewModel will receive the member using same Token
                Messenger.Default.Send(SelectedMember, "MemberToEdit");
            }
        }

        private void ExitWindow(IClosable window)
        {
            if (window != null)
            {
                window.Close();
            }
        }

        private void SaveMember(Member objMember)
        {
            //Do validation 
            if (!Validator.IsPresent("First Name", objMember.FirstName)
               || !Validator.IsPresent("Last Name", objMember.LastName)
               || !Validator.IsPresent("Email", objMember.Email)
               || !Validator.IsValidEmail("Email", objMember.Email))
            {
                return;
            }
            MemberList.Add(objMember);
            MembershipData.SaveMemberships(MemberList);
            Messenger.Default.Send(new NotificationMessage(objMember.DisplayText + " is added successfully."));
            CloseWindow(objMembershipAdd);
        }

        private void UpdateMember(Member objMember)
        {
            // Do validation 
            if (!Validator.IsPresent("First Name", objMember.FirstName)
               || !Validator.IsPresent("Last Name", objMember.LastName)
               || !Validator.IsPresent("Email", objMember.Email)
               || !Validator.IsValidEmail("Email", objMember.Email))
            {
                return;
            }
            SelectedMember = objMember;
            MembershipData.SaveMemberships(MemberList);
            MemberList = MembershipData.GetMemberships();
            this.RaisePropertyChanged(() => this.MemberList);
            Messenger.Default.Send(new NotificationMessage(objMember.DisplayText + " is updated successfully."));
            CloseWindow(objMemberUpdate);
        }

        private void DeleteMember(Member objMember)
        {
            MemberList.Remove(objMember);
            MembershipData.SaveMemberships(MemberList);
            Messenger.Default.Send(new NotificationMessage(objMember.DisplayText + " is removed successfully."));
            CloseWindow(objMemberUpdate);
        }

        private void CloseWindow(Window wndObject)
        {
            wndObject.Close();
        }
    }
}