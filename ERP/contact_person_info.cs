//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ERP
{
    using System;
    using System.Collections.Generic;
    
    public partial class contact_person_info
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public contact_person_info()
        {
            this.contact_person_mobile = new HashSet<contact_person_mobile>();
        }
    
        public int sales_person_id { get; set; }
        public int branch_serial { get; set; }
        public int contact_id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string gender { get; set; }
        public Nullable<int> department { get; set; }
        public Nullable<bool> is_valid { get; set; }
        public Nullable<System.DateTime> date_added { get; set; }
    
        public virtual company_address company_address { get; set; }
        public virtual employees_info employees_info { get; set; }
        public virtual departments_type departments_type { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<contact_person_mobile> contact_person_mobile { get; set; }
    }
}
