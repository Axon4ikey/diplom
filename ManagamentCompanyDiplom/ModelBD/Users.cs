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
    
    public partial class Users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Users()
        {
            this.ApplicationsForServices = new HashSet<ApplicationsForServices>();
            this.Flat = new HashSet<Flat>();
        }
    
        public int ID_Users { get; set; }
        public string Surname { get; set; }
        public string Firstname { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Nullable<int> ID_StatusConfirmation { get; set; }
        public Nullable<int> ID_Roles { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApplicationsForServices> ApplicationsForServices { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Flat> Flat { get; set; }
        public virtual Roles Roles { get; set; }
        public virtual UsersStatusConfirmation UsersStatusConfirmation { get; set; }
    }
}
