using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment2
{
    public class MembershipList
    {
        private List<Member> members;
        // Declare the delegate
        public delegate void ChangeHandler(MembershipList members);
        // Declare the Event
        public event ChangeHandler Changed;
        private MembershipData objMembershipData;

        // Default argument constructor
        public MembershipList()
        {
            members = new List<Member>();
            objMembershipData = new MembershipData();
        }

        // Indexer 
        public Member this[int i]
        {
            get
            {
                if (i < 0 || i >= members.Count)
                {
                    throw new ArgumentOutOfRangeException(i.ToString());
                }
                return members[i];
            }
            set
            {
                members[i] = value;
                Changed(this);
            }
        }

        public int Count => members != null ? members.Count : 0;

        // Add a specified Membership object to the list.
        private void Add(Member objMember)
        {
            members.Add(objMember);
            Changed(this);
        }

        // Remove the specified Membership from the list.
        private void Remove(Member objMember)
        {
            members.Remove(objMember);
            Changed(this);
        }

        // Operator Overloading: Add the member to the membership list
        public static MembershipList operator +(MembershipList memberList,Member member)
        {
            memberList.Add(member);
            return memberList;
        }

        // Operator Overloading: Remove the member from the membership list
        public static MembershipList operator -(MembershipList members, Member member)
        {
            members.Remove(member);
            return members;
        }

        // Fetch membership data from File
        public void Write() => members = objMembershipData.GetMemberships();

        // Saves the memberships
        public void Save() => objMembershipData.SaveMemberships(members);
    }
}
