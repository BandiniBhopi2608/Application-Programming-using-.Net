using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Assignment4_MVVM.Model;
using System.Windows;
using Assignment4_MVVM.Interface;

namespace Assignment4_MVVM.ViewModel
{
    public class MemberViewModel : ViewModelBase
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private Member _modifiedMember = new Member();

        public RelayCommand<IClosable> ExitWindowCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }

        public MemberViewModel()
        {
            DeleteCommand = new RelayCommand(DeleteMember);
            SaveCommand = new RelayCommand(SaveMember);
            UpdateCommand = new RelayCommand(UpdateMember);
            ExitWindowCommand = new RelayCommand<IClosable>(ExitWindow);
            Messenger.Default.Register<Member>(this, "MemberToEdit", LoadMember);
            Messenger.Default.Register<NotificationMessage>(this, ResetData);
        }

        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                RaisePropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                RaisePropertyChanged("LastName");
            }
        }
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                RaisePropertyChanged("Email");
            }
        }

        private void SaveMember()
        {
            Member objMember = new Member();
            objMember.FirstName = FirstName;
            objMember.LastName = LastName;
            objMember.Email = Email;
            Messenger.Default.Send(objMember, "add");
        }

        private void LoadMember(Member objMember)
        {
            _modifiedMember = objMember;
            FirstName = objMember.FirstName;
            LastName = objMember.LastName;
            Email = objMember.Email;
        }

        private void UpdateMember()
        {
            _modifiedMember.FirstName = FirstName;
            _modifiedMember.LastName = LastName;
            _modifiedMember.Email = Email;
            Messenger.Default.Send(_modifiedMember, "edit");
        }

        private void DeleteMember()
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete '" + FirstName + " " + LastName + "'"
                                     , "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Messenger.Default.Send(_modifiedMember, "delete");
            }
        }

        private void ExitWindow(IClosable window)
        {
            if (window != null)
            {
                window.Close();
                ResetFields();
            }
        }

        private void ResetData(NotificationMessage message)
        {
            if (message.Notification.Contains("added successfully.")
                || message.Notification.Contains("updated successfully.")
                || message.Notification.Contains("removed successfully."))
            {
                ResetFields();
            }
        }

        private void ResetFields()
        {
            FirstName = "";
            LastName = "";
            Email = "";
        }
    }
}
