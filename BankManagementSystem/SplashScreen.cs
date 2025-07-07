using BankManagementSystem.Forms;
using BankManagementSystem.Forms.Admin;
using System;
using System.Windows.Forms;

namespace BankManagementSystem
{
    public partial class SplashScreen : Form
    {
       
        //private int waitCounter = 0;
        

        public SplashScreen()
        {
            InitializeComponent();
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            progressBar.Value += 2;

            if (progressBar.Value >= 100)
            {
                timer.Stop();
                this.Hide(); // Or: this.Close();
                new HomePageForm().Show(); // Replace with your main form
            }
        }

    }
}
