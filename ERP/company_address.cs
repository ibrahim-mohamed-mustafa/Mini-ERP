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
    
    public partial class company_address
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public company_address()
        {
            this.company_telephone = new HashSet<company_telephone>();
            this.contact_person_info = new HashSet<contact_person_info>();
        }
    
        public int address_serial { get; set; }
        public Nullable<int> company_serial { get; set; }
        public Nullable<int> address { get; set; }
        public Nullable<int> added_by { get; set; }
        public Nullable<System.DateTime> date_added { get; set; }
    
        public virtual company_name company_name { get; set; }
        public virtual district district { get; set; }
        public virtual employees_info employees_info { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<company_telephone> company_telephone { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<contact_person_info> contact_person_info { get; set; }
    }
}
