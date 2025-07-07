using System;
using System.Drawing;
using System.Windows.Forms;

namespace BankManagementSystem.Forms
{
    public partial class HomePageForm : Form
    {
        public HomePageForm()
        {
            InitializeComponent();
        }

        private void WelcomeForm_Load(object sender, EventArgs e)
        {
          

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm loginForm = new LoginForm(); // Navigate to Login
            loginForm.ShowDialog();
            
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegistrationForm regForm = new RegistrationForm(); // Navigate to Sign Up / Register
            regForm.ShowDialog();
            
        }

        private void btnOpenAccount_Click(object sender, EventArgs e)
        {
            // Same as Sign Up
            btnSignUp_Click(sender, e);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit(); // Close the app
        }

        

        private void buttonClose_Click(object sender, EventArgs e)
        {

            Application.Exit(); // Close the application

        }
      
    }
}