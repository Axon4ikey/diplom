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
    
    public partial class PersonalАccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonalАccount()
        {
            this.Invoice = new HashSet<Invoice>();
            this.MetersData = new HashSet<MetersData>();
        }
    
        public int ID_PersonalAccount { get; set; }
        public string Number_PersonalAccount { get; set; }
        public Nullable<int> NumberOfResidents { get; set; }
        public Nullable<int> ID_Flat { get; set; }
        public Nullable<int> ID_Accruals { get; set; }
        public Nullable<int> ID_TypeOfPersonalAccount { get; set; }
    
        public virtual Accruals Accruals { get; set; }
        public virtual Flat Flat { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invoice> Invoice { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MetersData> MetersData { get; set; }
        public virtual TypeOfPersonalAccount TypeOfPersonalAccount { get; set; }
    }
}
