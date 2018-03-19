using System;
using System.Security;
using RFIDClient.Desktop.Core;

namespace RFIDClient.Desktop
{
    /// <summary>
    /// Interaction logic for NewUserPage.xaml
    /// </summary>
    public partial class NewUserPage : BasePage<UserViewModel>, IUser
    {
        
        public NewUserPage()
        {
            InitializeComponent();
        }

        public string FirstName => txtName.Text;

        public string LastName => txtSurname.Text;

        public string Email => txtEmail.Text;

        public string Username => txtusername.Text;

        public SecureString Password => txtPassword.SecurePassword;

        public SecureString Password2 => txtPasswordRetype.SecurePassword;

        private void ResetForm()
        {
            txtName.Text = "";
            txtSurname.Text = "";
            txtusername.Text = "";
            txtEmail.Text = "";
            txtPassword.SecurePassword.Clear();
            txtPasswordRetype.SecurePassword.Clear();
        }

        void IUser.ResetForm()
        {
            this.ResetForm();
        }
    }
}
