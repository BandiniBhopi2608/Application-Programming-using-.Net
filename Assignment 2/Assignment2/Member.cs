using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    public class Member
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
        public Member(string strFirstName, string strLastName, string strEmail)
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
                if (strFirstName.Length > 25)
                {
                    throw new ArgumentException("First Name should not be longer than 25 character.");
                }
                strFirstName = value;
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
                if (strLastName.Length > 25)
                {
                    throw new ArgumentException("Last Name should not be longer than 25 character.");
                }
                strLastName = value;
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
                if (strEmail.Length > 25)
                {
                    throw new ArgumentException("Email should not be longer than 25 character.");
                }
                strEmail = value;
            }
        }

        public string GetDisplayText() => strFirstName + " " + strLastName + " - " + strEmail;
    }
}
