//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ManagamentCompanyDiplom.ModelBD
{
    using System;
    using System.Collections.Generic;
    
    public partial class StatusApplicationForServices
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StatusApplicationForServices()
        {
            this.ApplicationsForServices = new HashSet<ApplicationsForServices>();
        }
    
        public int ID_StatusApplicationForServices { get; set; }
        public string NameStatus { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApplicationsForServices> ApplicationsForServices { get; set; }
    }
}