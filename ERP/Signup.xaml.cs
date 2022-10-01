using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ERP
{
    /// <summary>
    /// Interaction logic for Signup.xaml
    /// </summary>
    public partial class Signup : Window
    {
        erp_systemEntities db = new erp_systemEntities();
        public Signup()
        {
            InitializeComponent();
        }

        private static bool IsEmailAllowed(string text)
        {
            bool blnValidEmail = false;
            Regex regEMail = new Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
            if (text.Length > 0)
            {
                blnValidEmail = regEMail.IsMatch(text);
            }

            return blnValidEmail;
        }

        bool IsValidPassword(string password)
        {
           
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var haslowerrChar = new Regex(@"[a-z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");

            var isValidated = hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password)
                              && haslowerrChar.IsMatch(password) && hasMinimum8Chars.IsMatch(password);
            return isValidated;
        }
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var P_email = TxtPEmail.Text;
                var B_email = TxtBEmail.Text;
                var passw = TxtPass.Password;
                var passw_copy = TxtPass_Copy.Password;
                bool ch = IsValidPassword(passw);
                var Query_ch_business_emails = (from i in db.employees_business_emails
                                                where i.email == B_email
                                                select i.id).ToList();

                int ch_emails = Query_ch_business_emails.FirstOrDefault();

                if (TxtBEmail.Text == "" | TxtPEmail.Text == "" | TxtPass.Password == "" | TxtPass_Copy.Password=="")
                {
                    MessageBox.Show("Some Fields is empty");
                }
                else if (!B_email.Contains("@01electronics.net"))
                {
                    MessageBox.Show("Your Business Email must using pre-known \"@01electronics.net\"  " , "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (ch_emails == 0)
                {
                    MessageBox.Show("This Business Email is not exists. Please try again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (IsEmailAllowed(P_email.Trim()) == false)
                {
                    MessageBox.Show("Email invalid", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    //TxtEmail.Text = "";
                    TxtPEmail.Focus();
                }
                else if (!ch)
                {
                    MessageBox.Show("Password must contain \"A lowercase letter\"  and \"A uppercase letter\" and" +
                        " \"A number\" and \"Minimum 8 characters\"  ");
                }
                else if (passw != passw_copy)
                {
                    MessageBox.Show("The two passwords entered do not match. Please try again" ,"Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
               
                else
                {
                    employees_personal_emails employees_Personal_Emails = new employees_personal_emails();
                    employees_passwords employees_passwords = new employees_passwords();
                   
                   
                    employees_Personal_Emails.id = ch_emails;
                    employees_Personal_Emails.email = P_email;
                    employees_Personal_Emails.date_added = DateTime.Now;
                    employees_passwords.id = ch_emails;
                    employees_passwords.password = passw;
                    employees_passwords.date_added = DateTime.Now;

                    db.employees_personal_emails.Add(employees_Personal_Emails);
                    db.employees_passwords.Add(employees_passwords);
                    db.SaveChanges();

                    MessageBox.Show("Registration completed successfully");
                    MainWindow MainWindow = new MainWindow();
                    this.Close();
                    MainWindow.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }
    }
}
