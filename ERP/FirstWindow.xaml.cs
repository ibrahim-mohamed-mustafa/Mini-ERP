using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace ERP
{
    /// <summary>
    /// Interaction logic for FirstWindow.xaml
    /// </summary>
    public partial class FirstWindow : Window
    {
        erp_systemEntities db = new erp_systemEntities();
        int logid;

        public FirstWindow(int id)
        {
            logid = id;
            InitializeComponent();
        }

        private void Client_Click(object sender, RoutedEventArgs e)
        {
            ClientWindow client = new ClientWindow(logid);
         
            this.Hide();
            client.ShowDialog();
            this.ShowDialog();


        }

        private void Company_Click(object sender, RoutedEventArgs e)
        {
            CompanyWindow Company = new CompanyWindow(logid);
           
            this.Hide();
            Company.ShowDialog(); 
            this.ShowDialog();
        }
    }
}
