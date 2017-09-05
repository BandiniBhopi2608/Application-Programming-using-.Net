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
    public partial class frmMembershipAdd : Form
    {
        private Member objMember;

        public frmMembershipAdd()
        {
            InitializeComponent();
            objMember = new Member();
        }

        public Member member
        {
            get
            {
                return objMember;
            }
        }

        private void frmMembershipAdd_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!Validator.IsPresent(txtFirstName) || !Validator.IsPresent(txtLastName)
               || !Validator.IsPresent(txtEmail) || !Validator.IsValidEmail(txtEmail))
            {
                return;
            }
            objMember.FirstName = txtFirstName.Text.Trim();
            objMember.LastName = txtLastName.Text.Trim();
            objMember.Email = txtEmail.Text.Trim();
            Close();
        }
    }
}
