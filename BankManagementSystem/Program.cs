using System;
using System.Windows.Forms;

namespace BankManagementSystem
{
    static class Program
    {
        /// <summary>
        /// Main Entry Point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();              // Enable Windows themes
            Application.SetCompatibleTextRenderingDefault(false);

            // Check if demo mode is requested
            if (args.Length > 0 && args[0].ToLower() == "/demo")
            {
                // Run the Validation Demo Form directly
                Application.Run(new Forms.Demo.ValidationDemoForm());
            }
            else
            {
                // 🏁 Start app with the Welcome Screen (normal mode)
                //Application.Run(new Forms.HomePageForm());
                Application.Run(new SplashScreen());
            }
        }
    }
}