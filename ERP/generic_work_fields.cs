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
    
    public partial class generic_work_fields
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public generic_work_fields()
        {
            this.specific_work_fields = new HashSet<specific_work_fields>();
        }
    
        public int id { get; set; }
        public string generic_work_field { get; set; }
        public Nullable<System.DateTime> date_added { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<specific_work_fields> specific_work_fields { get; set; }
    }
}
