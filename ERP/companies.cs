using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP
{
    class companies
    {
        public int Company_name_company_series { get; set; }
        public int Company_address_address_serial { get; set; }
        public int Company_telephone_branch_serial { get; set; }
        public int District_id_fkcomadd_address { get; set; }
        public int Cites_id_fkdis_city { get; set; }
        public int States_governorates_id_fkcity_states_governorates { get; set; }
        public int County_id_fkstagov_country { get; set; }
        public int Specific_work_field_id { get; set; }
        public string Company_name_company_name1 { get; set; }
        public string Company_telephone_telephone_number { get; set; }
        public string District_district1 { get; set; }
        public string Cites_city1 { get; set; }
        public string State_governorate_state_governorate { get; set; }
        public string Country_country1 { get; set; }
        public string Specific_work_field_specific_work_field { get; set; }
        public string Generic_work_field_generic_work_field { get; set; }
    }
}
