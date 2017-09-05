using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace Assignment4_MVVM.Model
{
    public class Member : ObservableObject
    {
        // Private properties
        private string strFirstName;
        private string strLastName;
        private string strEmail;

        // Default argument constructor
        public Member()
        {
            strFirstName = "";
            strLastName = "";
            strEmail = "";
        }

        // Argument constructor
        public Member(int id, string strFirstName, string strLastName, string strEmail)
        {
            this.strFirstName = strFirstName;
            this.strLastName = strLastName;
            this.strEmail = strEmail;
        }
        
        // Property : FirstName
        public string FirstName
        {
            get
            {
                return strFirstName;
            }
            set
            {
                if (value != null && value.Length > 25)
                {
                    throw new ArgumentException("First Name should not be longer than 25 character.");
                }
                Set<string>(() => this.FirstName, ref strFirstName, value);
            }
        }

        // Property : LastName
        public string LastName
        {
            get
            {
                return strLastName;
            }
            set
            {
                if (value != null &&  value.Length > 25)
                {
                    throw new ArgumentException("Last Name should not be longer than 25 character.");
                }
                Set<string>(() => this.LastName, ref strLastName, value);
            }
        }

        // Property : Email
        public string Email
        {
            get
            {
                return strEmail;
            }
            set
            {
                if (value != null && value.Length > 25)
                {
                    throw new ArgumentException("Email should not be longer than 25 character.");
                }
                Set<string>(() => this.Email, ref strEmail, value);
            }
        }

        public string DisplayText
        {
            get => strFirstName + " " + strLastName + " - " + strEmail;
        }
    }
}
