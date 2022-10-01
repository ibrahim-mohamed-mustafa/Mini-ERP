using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
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
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        erp_systemEntities db = new erp_systemEntities();
        bool ch = false;
        int logid;

        public ClientWindow(int id)
        {
            logid = id;
            InitializeComponent();
            LoadGrid();
        }

        List<Clients> Listclient;
        int index;

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

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void LoadGrid()
        {

            var clients_with_ids = (from i in db.contact_person_info
                                    join s in db.contact_person_mobile
                                    on i.sales_person_id equals s.sales_person_id
                                    where i.contact_id == s.contact_id
                                    where i.branch_serial == s.branch_serial
                                    join c in db.company_address
                                    on i.branch_serial equals c.address_serial
                                    join nc in db.company_name
                                    on c.company_serial equals nc.company_serial
                                    select new { i.contact_id, i.branch_serial, i.sales_person_id, i.email, i.name, i.gender, s.mobile, s.telephone_id, nc.company_name1, nc.company_serial }).ToList();


            Listclient = new List<Clients>();
            foreach (var e in clients_with_ids)
            {
                Listclient.Add(new Clients()
                {
                    branch_serial = e.branch_serial,
                    contact_id = e.contact_id,
                    sales_person_id = e.sales_person_id,
                    email = e.email,
                    name = e.name,
                    gender = e.gender,
                    mobile = e.mobile,
                    telephone_id = e.telephone_id,
                    Company_name = e.company_name1,
                    Company_name_company_series = e.company_serial
                });
            }


            var clients_info = (from i in db.contact_person_info
                                join s in db.contact_person_mobile
                                on i.sales_person_id equals s.sales_person_id
                                where i.contact_id == s.contact_id
                                where i.branch_serial == s.branch_serial
                                join c in db.company_address
                                on i.branch_serial equals c.address_serial
                                join nc in db.company_name
                                on c.company_serial equals nc.company_serial
                                select new { i.email, i.name, i.gender, s.mobile, nc.company_name1 }).ToList();

            DataGrid1.ItemsSource = clients_info;
            disable_delete();
            disable_update();
            enable_add();
            C_Address.IsEnabled = true;

        }
        private void disable_update()
        {
            BtnUpdate.IsEnabled = false;
            BtnUpdate.Foreground = Brushes.Black;
        }

        private void disable_delete()
        {
            BtnDelete.IsEnabled = false;
            BtnDelete.Foreground = Brushes.Black;
        }

        private void disable_add()
        {
            BtnAdd.IsEnabled = false;
            BtnAdd.Foreground = Brushes.Black;
        }

        private void enable_update()
        {
            BtnUpdate.IsEnabled = true;
            BtnUpdate.Foreground = Brushes.White;
        }

        private void enable_delete()
        {
            BtnDelete.IsEnabled = true;
            BtnDelete.Foreground = Brushes.White;
        }

        private void enable_add()
        {
            BtnAdd.IsEnabled = true;
            BtnAdd.Foreground = Brushes.White;
        }

        private void Clear()
        {

            TxtEmail.Text = "";
            TxtName.Text = "";
            Gender.Text = "";
            TxtMobile.Text = "";
            C_Name.Text = "";
            C_Name.IsEnabled = true;
            C_Address.Text = "";
            C_Address.IsEnabled = true;
            disable_add_mob();
            disable_delete();
            disable_update();
            enable_Enable_add_mob();
            enable_Disable_add_mob();
            enable_add();
            enable_texts();
        }

        private void DataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            index = gd.SelectedIndex;

            C_Name.IsEnabled = false;
            C_Address.IsEnabled = false;
            disable_add_mob();
            disable_Enable_add_mob();
            disable_Disable_add_mob();
            enable_delete();
            enable_update();
            disable_add();

            if (ch == false)
            {

                TxtEmail.Text = Listclient[index].email;
                TxtName.Text = Listclient[index].name;
                Gender.Text = Listclient[index].gender;
                TxtMobile.Text = Listclient[index].mobile;
                C_Name.Text = Listclient[index].Company_name;
                string Company_Name = Listclient[index].Company_name;
                string Email = TxtEmail.Text;
                var Query_C_address = (from i in db.company_name
                                       where i.company_name1 == Company_Name
                                       join ac in db.company_address
                                       on i.company_serial equals ac.company_serial
                                       join cl in db.contact_person_info
                                       on  ac.address_serial equals cl.branch_serial
                                       where cl.email == Email
                                       join adc in db.districts
                                       on ac.address equals adc.id
                                       join acc in db.cities
                                       on adc.city equals acc.id
                                       join astc in db.states_governorates
                                       on acc.state_governorate equals astc.id
                                       join aconc in db.countries
                                       on astc.country equals aconc.id
                                       select new
                                       {
                                           adc.district1,
                                           acc.city1,
                                           astc.state_governorate,
                                           aconc.country1,
                                       }).ToList();
                C_Address.Text=string.Join(" , ", Query_C_address[0].district1, Query_C_address[0].city1, Query_C_address[0].state_governorate, Query_C_address[0].country1);
                C_Address.IsEnabled = false;
            }
            ch = false;

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            contact_person_info contact_person_info = new contact_person_info();
            contact_person_mobile contact_person_mobile = new contact_person_mobile();
            if (TxtEmail.Visibility == Visibility.Hidden) TxtEmail.Text = "Validmail22@gmail.com";
            if (TxtEmail.Text == "" | TxtMobile.Text == "" | Gender.Text == "" | C_Name.Text == "" | TxtName.Text == "" | C_Address.Text == "")
            {
                MessageBox.Show("Some Fields is empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
          
            else if (IsEmailAllowed(TxtEmail.Text.Trim()) == false)
            {
                MessageBox.Show("Email invalid", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                TxtEmail.Focus();
            }
            else
            {
                var address = (string)C_Address.SelectedValue;
                string address_trim = address.Replace(" ", ""); ;
                string[] address_arr = address_trim.Split(',');
                string[] address_arrSpace = address.Split(',');
                string dis_W = address_arrSpace[0];
                string dis = dis_W.TrimEnd();
                string city_W = address_arrSpace[1];
                string city = city_W.TrimEnd().TrimStart();
                string state = address_arr[2];
                string country = address_arr[3];
                
                var Query_selected_address =
                                        (from adc in db.districts
                                         where adc.district1 == dis
                                         join acc in db.cities
                                         on adc.city equals acc.id
                                         where acc.city1 == city
                                         join astc in db.states_governorates
                                         on acc.state_governorate equals astc.id
                                         where astc.state_governorate == state
                                         join aconc in db.countries
                                         on astc.country equals aconc.id
                                         where aconc.country1 == country
                                         select adc.id).ToList();
                int C_address_id = Query_selected_address.FirstOrDefault();

                var QueryCh_Company_name = (from i in db.company_name
                                            where i.company_name1 == (string)C_Name.SelectedValue
                                            select i.company_serial).ToList();
                int company_serial = QueryCh_Company_name.FirstOrDefault();
                var Query_branch_serial = (from i in db.company_address
                                           where i.company_serial == company_serial
                                           where i.address == C_address_id
                                           select i.address_serial).ToList();
                int branch_serial = Query_branch_serial.FirstOrDefault();
                
                contact_person_info.branch_serial = branch_serial;
                contact_person_info.sales_person_id = logid;
                contact_person_info.email = TxtEmail.Text;
                contact_person_info.name = TxtName.Text;
                contact_person_info.gender = Gender.Text;
                contact_person_info.date_added = DateTime.Now;

                var Query_tel_id = (from i in db.contact_person_info
                                        where i.branch_serial == branch_serial
                                        where i.email == (string)C_Email.SelectedValue
                                        join mo in db.contact_person_mobile
                                        on i.branch_serial equals mo.branch_serial
                                        where i.contact_id == mo.contact_id
                                        select mo.telephone_id).ToList();
                var Query_tel_contact_id = (from i in db.contact_person_info
                                    where i.branch_serial == branch_serial
                                    where i.email == (string)C_Email.SelectedValue
                                    join mo in db.contact_person_mobile
                                    on i.branch_serial equals mo.branch_serial
                                    where i.contact_id == mo.contact_id
                                    select i.contact_id).ToList();
                int tel_contact_id = Query_tel_contact_id.FirstOrDefault();

                if (Query_tel_id.Count() != 0)
                {
                    contact_person_mobile.contact_id = tel_contact_id;
                    contact_person_mobile.telephone_id = Query_tel_id.Max() + 1;
                }
                else 
                { 
                    contact_person_mobile.telephone_id = 1;
                    contact_person_info.contact_id = 1;
                    contact_person_mobile.contact_id = contact_person_info.contact_id;
                }

                var Query_contact_id = (from i in db.contact_person_info
                                        where i.branch_serial == branch_serial
                                        where i.email != (string)C_Email.SelectedValue
                                        select i.contact_id).ToList();
                if (Query_contact_id.Count() != 0 && Query_tel_id.Count() == 0)
                {
                    contact_person_info.contact_id = Query_contact_id.Max() + 1;
                    contact_person_mobile.contact_id = contact_person_info.contact_id;   
                }
                
                contact_person_mobile.sales_person_id = logid;
                contact_person_mobile.branch_serial = contact_person_info.branch_serial;
                
                contact_person_mobile.mobile = TxtMobile.Text;
                contact_person_mobile.date_added = DateTime.Now;
                if (Query_tel_id.Count() == 0)
                    db.contact_person_info.Add(contact_person_info);
                db.contact_person_mobile.Add(contact_person_mobile);
                db.SaveChanges();
                Clear();
                MessageBox.Show("Data added Successfully");
                LoadGrid();
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            contact_person_info contact_Person_Info = db.contact_person_info.Find(Listclient[index].sales_person_id, Listclient[index].branch_serial, Listclient[index].contact_id);
            contact_person_mobile contact_Person_Mobile = db.contact_person_mobile.Find(Listclient[index].sales_person_id, Listclient[index].branch_serial, Listclient[index].contact_id, Listclient[index].telephone_id);

            if (TxtEmail.Text == "" | TxtMobile.Text == "" | Gender.Text == "" | C_Name.Text == "" | TxtName.Text == "" | C_Address.Text=="")
            {
                MessageBox.Show("Some Fields is empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else
            { 
                contact_Person_Info.email = TxtEmail.Text;
                contact_Person_Info.name = TxtName.Text;
                contact_Person_Info.gender =  Gender.Text;  
                contact_Person_Mobile.mobile = TxtMobile.Text;
                db.SaveChanges();
                ch = true;
                MessageBox.Show("Data updated Successfully");
                enable_texts();
                LoadGrid();
                Clear();
            }
        }
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            contact_person_info contact_Person_Info = db.contact_person_info.Find(Listclient[index].sales_person_id, Listclient[index].branch_serial, Listclient[index].contact_id);
            contact_person_mobile contact_Person_Mobile = db.contact_person_mobile.Find(Listclient[index].sales_person_id, Listclient[index].branch_serial, Listclient[index].contact_id, Listclient[index].telephone_id);
            if (TxtEmail.Text == "" | TxtMobile.Text == "" | Gender.Text == "" | C_Name.Text == "" | TxtName.Text=="" | C_Address.Text == "")
            {
                MessageBox.Show("Some Fields is empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else
            {
                var Query_tel_id = (from i in db.contact_person_info
                                    where i.email == TxtEmail.Text
                                    join mo in db.contact_person_mobile
                                    on i.branch_serial equals mo.branch_serial
                                    where i.contact_id == mo.contact_id
                                    select mo.telephone_id).ToList();
                if (Query_tel_id.Count() == 1)
                    db.contact_person_info.Remove(contact_Person_Info);
                db.contact_person_mobile.Remove(contact_Person_Mobile);
                db.SaveChanges();
                ch = true;

                MessageBox.Show("Data deleted Successfully");
                LoadGrid();
                Clear();
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Data cleared Successfully");
            Clear();
        }
        private void Load_C_Name(object sender, RoutedEventArgs e)
        {
            var Query_C_Name = (from i in db.company_name
                                orderby i.company_name1
                                 select i.company_name1).ToList();
            C_Name.ItemsSource = Query_C_Name;
        }

        private void C_Name_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            C_Address.Items.Clear();
            var Query_C_address = (from i in db.company_name
                                   where i.company_name1 == (string)C_Name.SelectedValue
                                   join ac in db.company_address
                                   on i.company_serial equals ac.company_serial
                                   join adc in db.districts
                                   on ac.address equals adc.id
                                   join acc in db.cities
                                   on adc.city equals acc.id
                                   join astc in db.states_governorates
                                   on acc.state_governorate equals astc.id
                                   join aconc in db.countries
                                   on astc.country equals aconc.id
                                   select new
                                   {
                                       adc.district1,
                                       acc.city1,
                                       astc.state_governorate,
                                       aconc.country1,
                                   }).ToList();
            foreach (var el in Query_C_address)
            {
                C_Address.Items.Add(string.Join(" , ", el.district1, el.city1, el.state_governorate, el.country1));   
            }
            
        }

        private void BtnE_Mob_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Enable Add Mobile Successfully");
            Enable_add_mob();
            disable_texts();
            disable_Enable_add_mob();
            enable_Disable_add_mob();
        }

        private void BtnD_Mob_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Disable Add Mobile Successfully");
            disable_add_mob();
            enable_texts();
            Clear();
            disable_Disable_add_mob();
            enable_Enable_add_mob();
        }

        private void Load_C_Email(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
            var Query_C_Email = (from i in db.contact_person_info
                                orderby i.email
                                select i.email).ToList();
            C_Email.ItemsSource = Query_C_Email;
        }

        private void disable_texts()
        {
            TxtName.IsEnabled = false;
            Gender.IsEnabled = false;
            C_Name.IsEnabled = false;
            C_Address.IsEnabled = false;

        }

        private void enable_texts()
        {
            TxtName.IsEnabled = true;
            Gender.IsEnabled = true;
            C_Name.IsEnabled = true;
            C_Address.IsEnabled = true;
           
        }

        private void Enable_add_mob()
        {
            TxtEmail.Visibility = Visibility.Hidden;
            C_Email.Visibility = Visibility.Visible;
            TxtEmail.Text = "";
            C_Email.Text = "";
        }

        private void disable_add_mob()
        {
            TxtEmail.Visibility = Visibility.Visible;
            C_Email.Visibility = Visibility.Hidden;
            TxtEmail.Text = "";
            C_Email.Text = "";
        }

        private void enable_Enable_add_mob()
        {
            BtnE_Mob.IsEnabled = true;
            BtnE_Mob.Foreground = Brushes.White;
        }
        private void disable_Enable_add_mob()
        {
            BtnE_Mob.IsEnabled = false;
            BtnE_Mob.Foreground = Brushes.Black;
        }

        private void enable_Disable_add_mob()
        {
            BtnD_Mob.IsEnabled = true;
            BtnD_Mob.Foreground = Brushes.White;
        }

        private void disable_Disable_add_mob()
        {
            BtnD_Mob.IsEnabled = false;
            BtnD_Mob.Foreground = Brushes.Black;
        }

        private void C_Email_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var Query_C_Email = (from i in db.contact_person_info
                                 where i.email == (string)C_Email.SelectedValue
                                 join cn in db.company_name
                                 on i.company_address.company_serial equals cn.company_serial
                                 join adc in db.districts
                                 on i.company_address.address equals adc.id
                                 join acc in db.cities
                                 on adc.city equals acc.id
                                 join astc in db.states_governorates
                                 on acc.state_governorate equals astc.id
                                 join aconc in db.countries
                                 on astc.country equals aconc.id

                                 select new { 
                                     i.name , 
                                     i.gender ,
                                     cn.company_name1 ,
                                     adc.district1,
                                     acc.city1,
                                     astc.state_governorate,
                                     aconc.country1,
                                 }).ToList();

            if (Query_C_Email.Count() != 0)
            {
                TxtName.Text = Query_C_Email[0].name;
                Gender.Text= Query_C_Email[0].gender; 
                C_Name.SelectedValue = Query_C_Email[0].company_name1;

                C_Address.SelectedValue = string.Join(" , ", Query_C_Email[0].district1, Query_C_Email[0].city1, Query_C_Email[0].state_governorate, Query_C_Email[0].country1);
               
            }
        }
    }
}
