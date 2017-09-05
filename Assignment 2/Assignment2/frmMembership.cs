using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment2
{
    public partial class frmMembership : Form
    {
        private MembershipList members;

        public frmMembership()
        {
            InitializeComponent();
            members = new MembershipList();
        }

        private void frmMembership_Load(object sender, EventArgs e)
        {
            //members.Changed += new MembershipList.ChangeHandler(fnUpdateChange);
            members.Changed += members =>
            {
                members.Save();
                fnLoadListBox();
            };
            members.Write();
            fnLoadListBox();
        }

        private void fnUpdateChange(MembershipList memberList)
        {
            members.Save();
            fnLoadListBox();
        }

        private void fnLoadListBox()
        {
            lstMembers.Items.Clear();
            for (int i = 0; i < members.Count; i++)
            {
                lstMembers.Items.Add(members[i].GetDisplayText());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmMembershipAdd newMember = new frmMembershipAdd();
            newMember.ShowDialog();
            Member member = newMember.member;
            if (member != null && !String.IsNullOrEmpty(member.FirstName)
                && !String.IsNullOrEmpty(member.LastName) && !String.IsNullOrEmpty(member.Email))
            {
                members += member;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int index = lstMembers.SelectedIndex;
            if (index != -1)
            {
                Member objMember = members[index];
                DialogResult button = MessageBox.Show("Are you sure you want to delete '" + objMember.FirstName + " " + objMember.LastName + "'"
                                                     , "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (button == DialogResult.Yes)
                {
                    members -= objMember;
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
