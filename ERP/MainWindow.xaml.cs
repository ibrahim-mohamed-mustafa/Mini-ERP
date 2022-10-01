using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ERP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        erp_systemEntities db = new erp_systemEntities();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void sign_up_Click(object sender, RoutedEventArgs e)
        {
           
            Signup signup = new Signup();
            this.Hide();
            signup.ShowDialog();
            this.Show();
           
        }

        private void sign_in_Click(object sender, RoutedEventArgs e)
        {
          
            try
            {
               
                var email = TxtEmail.Text;
                var passw = TxtPass.Password;
                var quary = (from emp in db.employees_business_emails
                          join pass in db.employees_passwords
                          on emp.id equals pass.id
                          where emp.email== email
                          where pass.password == passw
                          select emp.id ).ToList();
                int userid = quary.FirstOrDefault();
                if (userid >= 1)
                {

                    FirstWindow firstWindow = new FirstWindow(userid);
                    this.Hide();
                    firstWindow.ShowDialog();
                    this.ShowDialog();
                }
                else
                {
                    if (TxtEmail.Text == "" | TxtPass.Password == "" )
                    {
                        MessageBox.Show("Some Fields is empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else if (!email.Contains("@01electronics.net"))
                    {
                        MessageBox.Show("Your Email must using pre-known \"@01electronics.net\" ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else { 
                    MessageBox.Show("Incorrect Email or Password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex  )
            {
                MessageBox.Show(ex.Message);
               
            }
            
        }
    }
}
