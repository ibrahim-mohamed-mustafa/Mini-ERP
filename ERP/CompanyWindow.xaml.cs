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
    /// Interaction logic for CompanyWindow.xaml
    /// </summary>
    public partial class CompanyWindow : Window
    {
        erp_systemEntities db = new erp_systemEntities();
        bool ch = false;
        int logid;

        public CompanyWindow(int id)
        {
            logid = id;
            InitializeComponent();
            LoadGrid();
        }
        List<companies> Listcompanies;
        int index;
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void LoadGrid()
        {
            var company_with_ids = (from nc in db.company_name
                                join ac in db.company_address
                                on nc.company_serial equals ac.company_serial
                                join tc in db.company_telephone
                                on ac.address_serial equals tc.branch_serial
                                join adc in db.districts
                                on ac.address equals adc.id
                                join acc in db.cities
                                on adc.city equals acc.id
                                join astc in db.states_governorates
                                on acc.state_governorate equals astc.id
                                join aconc in db.countries
                                on astc.country equals aconc.id
                                join fc in db.company_field_of_work
                                on nc.company_serial equals fc.company_serial
                                join spfc in db.specific_work_fields
                                on fc.work_field equals spfc.id
                                join gfc in db.generic_work_fields
                                on spfc.generic_work_field equals gfc.id
                                orderby nc.company_name1
                                select new
                                {
                                    nc.company_serial,
                                    ac.address_serial,
                                    tc.branch_serial,
                                    ac.address,
                                    adc.city,
                                    astc.id,
                                    astc.country,
                                    fc.work_field,
                                    nc.company_name1,
                                    tc.telephone_number,
                                    adc.district1,
                                    acc.city1,
                                    astc.state_governorate,
                                    aconc.country1,
                                    gfc.generic_work_field,
                                    spfc.specific_work_field,
                                    
                                }).ToList();
            Listcompanies = new List<companies>();
            foreach (var e in company_with_ids)
            {
                Listcompanies.Add(new companies()
                {
                    Company_name_company_series = e.company_serial,
                    Company_address_address_serial = e.address_serial,
                    Company_telephone_branch_serial = e.branch_serial,
                    District_id_fkcomadd_address = (int)e.address,
                    Cites_id_fkdis_city = (int)e.city,
                    States_governorates_id_fkcity_states_governorates = e.id,
                    County_id_fkstagov_country = (int)e.country,
                    Specific_work_field_id = (int)e.work_field,
                    Company_name_company_name1 = e.company_name1,
                    Company_telephone_telephone_number = e.telephone_number,
                    District_district1 = e.district1,
                    Cites_city1 = e.city1,
                    State_governorate_state_governorate = e.state_governorate,
                    Country_country1 = e.country1,
                    Generic_work_field_generic_work_field = e.generic_work_field,
                    Specific_work_field_specific_work_field = e.specific_work_field
                    
                });
            }

            var company_info = (from nc in db.company_name
                      join ac in db.company_address
                      on nc.company_serial equals ac.company_serial
                      join tc in db.company_telephone
                      on ac.address_serial equals tc.branch_serial
                      join adc in db.districts
                      on ac.address equals adc.id
                      join acc in db.cities
                      on adc.city equals acc.id
                      join astc in db.states_governorates
                      on acc.state_governorate equals astc.id
                      join aconc in db.countries
                      on astc.country equals aconc.id
                      join fc in db.company_field_of_work
                      on nc.company_serial equals fc.company_serial
                      join spfc in db.specific_work_fields
                      on fc.work_field equals spfc.id
                      join gfc in db.generic_work_fields
                      on spfc.generic_work_field equals gfc.id  
                      orderby nc.company_name1
                      select new {nc.company_name1 , tc.telephone_number , adc.district1 , acc.city1 ,
                                  astc.state_governorate , aconc.country1 , gfc.generic_work_field,
                                  spfc.specific_work_field
                      }).ToList();

            DataGrid1.ItemsSource = company_info;
            disable_delete();
            disable_update();
            enable_add();
            enable_Enable_B();
            enable_Disable_B();
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
            BtnAdd.Foreground = Brushes.White ;
        }

        private void enable_Enable_B()
        {
            BtnE_B.IsEnabled = true;
            BtnE_B.Foreground = Brushes.White;
        }


        private void disable_Enable_B()
        {
            BtnE_B.IsEnabled = false;
            BtnE_B.Foreground = Brushes.Black;
        }

        private void enable_Disable_B()
        {
            BtnD_B.IsEnabled = true;
            BtnD_B.Foreground = Brushes.White;
        }

        private void disable_Disable_B()
        {
            BtnD_B.IsEnabled = false;
            BtnD_B.Foreground = Brushes.Black;
        }

        private void disable_sAddress()
        {
            City.IsEnabled = false;
            State.IsEnabled = false;
            Country.IsEnabled = false;
        }

        private void enable_sAddress()
        {
            City.IsEnabled = true;
            State.IsEnabled = true;
            Country.IsEnabled = true;
        }
        private void disable_Gn_Sp_Work()
        {
            Gn_Work.IsEnabled = false;
            Sp_Work.IsEnabled = false;
        }

        private void enable_Gn_Sp_Work()
        {
            Gn_Work.IsEnabled = true;
            Sp_Work.IsEnabled = true;
        }

        private void Clear()
        {
            TxtCname.Text = "";
            TxtCtel.Text = "";
            TxtCdis.Text = "";
            TxtCcity.Text = "";
            C_Name_Branch.SelectedValue = "";
            State.SelectedValue = "";
            Country.SelectedValue = "";
            Dist.SelectedValue = "";
            City.SelectedValue = "";
            Sp_Work.SelectedValue = "";
            Gn_Work.SelectedValue = "";
            TxtCtel.IsEnabled = true;
            disable_delete();
            disable_update();
            enable_add();
            enable_Enable_B();
            enable_Disable_B();
            enable_Gn_Sp_Work();
            enable_sAddress();
            visable_combo();
        }

        private void disable_Txt()
        {
            TxtCtel.IsEnabled = false;    
        }
        private void visable_combo()
        {
            City.Visibility = Visibility.Visible;
            Dist.Visibility = Visibility.Visible;
        }

        private void DataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Clear();
            enable_delete();
            enable_update();
            disable_add();
            disable_Enable_B();
            disable_Disable_B();
            disable_branch();
            DataGrid gd = (DataGrid)sender;
            index = gd.SelectedIndex;
            disable_Txt();
            if (ch == false)
            {
              
                TxtCname.Text = Listcompanies[index].Company_name_company_name1;
                TxtCtel.Text = Listcompanies[index].Company_telephone_telephone_number;
                Dist.Text = Listcompanies[index].District_district1;
                City.Text = Listcompanies[index].Cites_city1;
                State.Text = Listcompanies[index].State_governorate_state_governorate;
                Country.Text = Listcompanies[index].Country_country1;
                Gn_Work.Text = Listcompanies[index].Generic_work_field_generic_work_field;
                Sp_Work.Text = Listcompanies[index].Specific_work_field_specific_work_field;

            }
            ch = false;

           
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            
            company_name company_Name = new company_name();
            company_address company_Address = new company_address();
            company_telephone company_Telephone = new company_telephone();
            district district = new district();
            city city = new city();
            states_governorates states_Governorates = new states_governorates();
            country country = new country();
            company_field_of_work company_Field_Of_Work = new company_field_of_work();
            specific_work_fields specific_Work_Fields = new specific_work_fields();
            generic_work_fields generic_Work_Fields = new generic_work_fields();
            if (TxtCname.Visibility == Visibility.Hidden) TxtCname.Text = "Not Empty";
            if (TxtCname.Text == "" | TxtCtel.Text == "" | (string)Dist.SelectedValue == "" |
               (string)Country.SelectedValue == "" | (string)State.SelectedValue == "" |
               (string)City.SelectedValue == "" | (string)Gn_Work.SelectedValue == "" |
               (string)Sp_Work.SelectedValue == "")
            {
                MessageBox.Show("Some Fields is empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else
            {

                var Query_country = (from i in db.countries
                                     where i.country1 == (string)Country.SelectedValue
                                     select i.id).ToList();
                int country_id = Query_country.FirstOrDefault();

                var Query_states = (from i in db.states_governorates
                                    where i.state_governorate == (string)State.SelectedValue
                                    select i.id).ToList();
                int states_id = Query_states.FirstOrDefault();

                var Query_cities = (from i in db.cities
                                    where i.city1 == (string)City.SelectedValue
                                    select i.id).ToList();
                int cities_id = Query_cities.FirstOrDefault();
                if (Query_cities.Count() == 0)
                {
                    var Query_citiesnew = (from i in db.cities
                                           select i.id).ToList();
                    city.id = Query_citiesnew.Max() + 1;
                    cities_id = city.id;
                    city.state_governorate = states_id;
                    city.city1 = TxtCcity.Text;
                    city.date_added = DateTime.Now;
                    db.cities.Add(city);
                }

                var Query_districts = (from i in db.districts
                                       where i.district1 == (string)Dist.SelectedValue
                                       select i.id).ToList();
                int districts_id = Query_districts.FirstOrDefault();
                if (Query_districts.Count() == 0)
                {
                    var Query_districtsnew = (from i in db.districts
                                              select i.id).ToList();
                    district.id = Query_districtsnew.Max() + 1;
                    districts_id = district.id;
                    district.city = cities_id;
                    district.district1 = TxtCdis.Text;
                    district.date_added = DateTime.Now;
                    db.districts.Add(district);
                }
                int company_serial_id = 0;
                var ch_company_serial = (from i in db.company_name
                                         where i.company_name1 == (string)C_Name_Branch.SelectedValue
                                         select i.company_serial).ToList();

                if (ch_company_serial.Count() != 0)
                {
                    company_serial_id = ch_company_serial.FirstOrDefault();
                }
                else
                {
                    var Query_max_company_serial = (from i in db.company_name
                                                    select i.company_serial).ToList();

                    company_Name.company_serial = Query_max_company_serial.Max() + 100;
                    company_serial_id = company_Name.company_serial;
                    company_Name.company_name1 = TxtCname.Text;
                    company_Name.added_by = logid;
                    company_Name.date_added = DateTime.Now;
                    db.company_name.Add(company_Name);
                }
                var Query_Company_address_branch_serial = (from i in db.company_address
                                                           select i.address_serial).ToList();

                company_Address.address_serial = Query_Company_address_branch_serial.Max() + 100;
                company_Address.company_serial = company_serial_id;
                company_Address.address = districts_id;
                company_Address.added_by = logid;
                company_Address.date_added = DateTime.Now;
                db.company_address.Add(company_Address);

                company_Telephone.branch_serial = company_Address.address_serial;
                company_Telephone.telephone_number = TxtCtel.Text;
                company_Telephone.added_by = logid;
                company_Telephone.date_added = DateTime.Now;
                db.company_telephone.Add(company_Telephone);

                var Query_generic_id = (from i in db.generic_work_fields
                                        where i.generic_work_field == (string)Gn_Work.SelectedValue
                                        select i.id).ToList();
                int generic_id = Query_generic_id.FirstOrDefault();

                var Query_specific_id = (from i in db.specific_work_fields
                                         where i.generic_work_field == generic_id
                                         where i.specific_work_field == (string)Sp_Work.SelectedValue
                                         select i.id).ToList();
                int specific_id = Query_specific_id.FirstOrDefault();
                if (ch_company_serial.Count() == 0)
                {
                    
                    company_Field_Of_Work.company_serial = company_serial_id;
                    company_Field_Of_Work.work_field = specific_id;
                    company_Field_Of_Work.added_by = logid;
                    company_Field_Of_Work.date_added = DateTime.Now;
                    db.company_field_of_work.Add(company_Field_Of_Work);
                }
                db.SaveChanges();
                MessageBox.Show("Data added Successfully");
                visable_combo();
                enable_Gn_Sp_Work();
                disable_branch();
                LoadGrid();
                Clear();
            }

        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            company_name company_Name = db.company_name.Find(Listcompanies[index].Company_name_company_series);
            company_field_of_work company_field_of_work = db.company_field_of_work.Find(Listcompanies[index].Company_name_company_series);
            company_address company_Address ;
            if (TxtCname.Text == "" | TxtCtel.Text == "" | (string)Dist.SelectedValue == "" |
              (string)Country.SelectedValue == "" | (string)State.SelectedValue == "" |
              (string)City.SelectedValue == "" | (string)Gn_Work.SelectedValue == "" |
              (string)Sp_Work.SelectedValue == "")
            {
                MessageBox.Show("Some Fields is empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            else
            {
                company_Name.company_name1 = TxtCname.Text;
                district district = new district();
                city city = new city();
               
                var Query_country = (from i in db.countries
                                     where i.country1 == (string)Country.SelectedValue
                                    select i.id).ToList();
                int country_id = Query_country.FirstOrDefault();

                var Query_states = (from i in db.states_governorates
                                    where i.state_governorate == (string)State.SelectedValue
                                    select i.id).ToList();
                int states_id = Query_states.FirstOrDefault();

                var Query_cities = (from i in db.cities
                                    where i.city1 == (string)City.SelectedValue
                                    select i.id).ToList();
                int cities_id = Query_cities.FirstOrDefault();

                var Query_districts = (from i in db.districts
                                       where i.district1 == (string)Dist.SelectedValue
                                       select i.id).ToList();
                int districts_id = Query_districts.FirstOrDefault();
               
                if (Query_cities.Count() == 0)
                {
                    var Query_citiesnew = (from i in db.cities
                                           select i.id).ToList();
                    city.id = Query_citiesnew.Max() + 1;
                    cities_id = city.id;
                    city.state_governorate = states_id;
                    city.city1 = TxtCcity.Text;
                    city.date_added = DateTime.Now;
                    db.cities.Add(city);
                }
               
                if ( Query_districts.Count() == 0)
                {
                    var Query_districtsnew = (from i in db.districts
                                              select i.id).ToList();
                    district.id = Query_districtsnew.Max() + 1;
                    districts_id = district.id;
                    district.city = cities_id;
                    district.district1 = TxtCdis.Text;
                    district.date_added = DateTime.Now;
                    db.districts.Add(district);
                }
               
                company_Address = db.company_address.Find(Listcompanies[index].Company_address_address_serial);
                company_Address.address = districts_id;

                var Query_generic_id = (from i in db.generic_work_fields
                                        where i.generic_work_field == (string)Gn_Work.SelectedValue
                                        select i.id).ToList();
                int generic_id = Query_generic_id.FirstOrDefault();

                var Query_specific_id = (from i in db.specific_work_fields
                                         where i.generic_work_field == generic_id
                                         where i.specific_work_field == (string)Sp_Work.SelectedValue
                                         select i.id).ToList();
                int specific_id = Query_specific_id.FirstOrDefault();

                company_field_of_work.work_field = specific_id;
                db.SaveChanges();
                ch = true;
                MessageBox.Show("Data updated Successfully");
                visable_combo();
                LoadGrid();
                Clear();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            company_name company_Name = db.company_name.Find(Listcompanies[index].Company_name_company_series);
            company_telephone company_Telephone = db.company_telephone.Find(Listcompanies[index].Company_telephone_branch_serial, Listcompanies[index].Company_telephone_telephone_number);
            company_address company_Address = db.company_address.Find(Listcompanies[index].Company_address_address_serial);
            company_field_of_work company_Field_Of_Work = db.company_field_of_work.Find(Listcompanies[index].Company_name_company_series);

            if (TxtCname.Text == "" | TxtCtel.Text == "" | (string)Dist.SelectedValue == "" |
              (string)Country.SelectedValue == "" | (string)State.SelectedValue == "" |
              (string)City.SelectedValue == "" | (string)Gn_Work.SelectedValue == "" |
              (string)Sp_Work.SelectedValue == "")
            {
                MessageBox.Show("Some Fields is empty");
            }

            else
            {

                var Query_c_info= (from i in db.company_name
                                    where i.company_name1 == TxtCname.Text
                                    join ad in db.company_address
                                    on i.company_serial equals ad.company_serial
                                    join tel in db.company_telephone
                                    on ad.address_serial equals tel.branch_serial
                                    join fwork in db.company_field_of_work
                                    on i.company_serial equals fwork.company_serial
                                    select new {
                                        i.company_name1,
                                        ad.address,
                                        tel.telephone_number    
                                    }).ToList();

                if (Query_c_info.Count() == 1)
                { 
                 db.company_name.Remove(company_Name);
                 db.company_field_of_work.Remove(company_Field_Of_Work);
                }
               
                db.company_address.Remove(company_Address);
                db.company_telephone.Remove(company_Telephone);
                
                db.SaveChanges();
                ch = true;
                MessageBox.Show("Data deleted Successfully");
                LoadGrid();
                Clear();
            }
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            MessageBox.Show("Data cleared Successfully"); 
        }

        private void Load_State(object sender, RoutedEventArgs e)
        {
            var Query_states = (from i in db.states_governorates
                                select i.state_governorate).ToList();
            State.ItemsSource = Query_states;           
        }
        private void ComboBox_SelectionChanged_State(object sender, SelectionChangedEventArgs e)
        {
            var Query_states = (from i in db.states_governorates
                                   where i.state_governorate == (string)State.SelectedValue
                                   select i.country).ToList();
            if (Query_states.Count ()!= 0) { 
                
                int states_country_id = (int)Query_states.FirstOrDefault();

            var Query_selected_country =
                                    (from  aconc in db.countries
                                     where  aconc.id == states_country_id
                                     select aconc.country1
                                     ).ToList();

            Country.SelectedItem = Query_selected_country[0];
            Country.IsEnabled = false;
           }
        }

        private void Load_Country(object sender, RoutedEventArgs e)
        {
            var Query_country = (from i in db.countries
                                 select i.country1).ToList();
            Country.ItemsSource = Query_country;
        }

        private void Load_Dist(object sender, RoutedEventArgs e)
        {
            var Query_districts = (from i in db.districts
                                   select i.district1).ToList();
            Query_districts.Insert(0, "Add New");
            Dist.ItemsSource = Query_districts;
        }

        private void ComboBox_SelectionChanged_Dist(object sender, SelectionChangedEventArgs e)
        {
            if ((string)Dist.SelectedValue == "Add New")
            {
                Dist.Visibility = Visibility.Hidden;
                enable_sAddress();

            }
            else if ((string)Dist.SelectedValue == null)
            {
                enable_sAddress();
            }
            else {
                var Query_districts = (from i in db.districts
                                       where i.district1 == (string)Dist.SelectedValue
                                       select i.city).ToList();
                int districts_city_id = (int)Query_districts.FirstOrDefault();

                var Query_selected_address = 
                                        (from  adc in db.districts
                                        join acc in db.cities
                                        on districts_city_id equals acc.id
                                        join astc in db.states_governorates
                                        on acc.state_governorate equals astc.id
                                        join aconc in db.countries
                                        on astc.country equals aconc.id
                                        select new
                                        {
                                            acc.city1,
                                            astc.state_governorate,
                                            aconc.country1,
                                            
                                        }).ToList();
               
                City.SelectedItem = Query_selected_address[0].city1;
                State.SelectedItem = Query_selected_address[0].state_governorate;
                Country.SelectedItem = Query_selected_address[0].country1;              
                disable_sAddress();

            }
         
        }

        private void Load_City(object sender, RoutedEventArgs e)
        {
            var Query_cities = (from i in db.cities
                                select i.city1).ToList();
            Query_cities.Insert(0,"Add New");

            City.ItemsSource = Query_cities;
        }
        private void ComboBox_SelectionChanged_City(object sender, SelectionChangedEventArgs e)
        {
            if ((string)City.SelectedValue == "Add New")
            {
                City.Visibility = Visibility.Hidden;

            }
        }

        private void Load_Gn_Work(object sender, RoutedEventArgs e)
        {
            var Query_Gn_Work = (from i in db.generic_work_fields
                                select i.generic_work_field).ToList();
            Gn_Work.ItemsSource = Query_Gn_Work;
        }
        private void Gn_Work_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var Query_Gn_Work_id = (from i in db.generic_work_fields
                                 where i.generic_work_field == (string)Gn_Work.SelectedValue
                                 select i.id).ToList();
            int Gn_Work_id = (int)Query_Gn_Work_id.FirstOrDefault();

            var Query_Sp_Work = (from spfc in db.specific_work_fields
                                 where spfc.generic_work_field == Gn_Work_id
                                 select spfc.specific_work_field
                                ).ToList();
            Sp_Work.IsEnabled = true;
            Sp_Work.ItemsSource = Query_Sp_Work;

        }

        private void C_Name_Branch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            var ch_Gn_Sp_CName = (from i in db.company_name
                                  where i.company_name1 == (string)C_Name_Branch.SelectedValue
                                  join fc in db.company_field_of_work
                                  on i.company_serial equals fc.company_serial
                                  join spfc in db.specific_work_fields
                                  on fc.work_field equals spfc.id
                                  join gfc in db.generic_work_fields
                                  on spfc.generic_work_field equals gfc.id
                                  select new
                                  {
                                      gfc.generic_work_field,
                                      spfc.specific_work_field
                                  }).ToList();

            if (ch_Gn_Sp_CName.Count != 0)
            {    
                Gn_Work.SelectedValue = ch_Gn_Sp_CName[0].generic_work_field;
                Sp_Work.SelectedValue = ch_Gn_Sp_CName[0].specific_work_field;
            }
            disable_Gn_Sp_Work();
            
        }
        private void Load_Name_Branch(object sender, RoutedEventArgs e)
        {
            var Query_C_Name = (from i in db.company_name
                                orderby i.company_name1
                                select i.company_name1).ToList();
            C_Name_Branch.ItemsSource = Query_C_Name;
        }

        private void BtnEnableB_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Enable Add Branch Successfully");
            Enable_branch();
            disable_Enable_B();
            enable_Disable_B();

        }

        private void BtnDisableB_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Disable Add Branch Successfully");
            disable_branch();
            Clear();
            disable_Disable_B();
            enable_Enable_B();
        }

        private void Enable_branch() 
        {
            TxtCname.Visibility = Visibility.Hidden;
            C_Name_Branch.Visibility = Visibility.Visible;
            disable_Gn_Sp_Work();
        }

        private void disable_branch()
        {
            TxtCname.Visibility = Visibility.Visible;
            C_Name_Branch.Visibility = Visibility.Hidden;
            enable_Gn_Sp_Work();
        }
    }
}
