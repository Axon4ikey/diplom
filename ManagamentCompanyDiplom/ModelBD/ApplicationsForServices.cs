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
    
    public partial class ApplicationsForServices
    {
        public int ID_ApplicationsForServices { get; set; }
        public Nullable<System.DateTime> DateOfApplication { get; set; }
        public Nullable<System.DateTime> DateOfApplicationChange { get; set; }
        public Nullable<int> ID_Users { get; set; }
        public Nullable<int> ID_Services { get; set; }
        public Nullable<int> ID_StatusApplicationForServices { get; set; }
    
        public virtual Services Services { get; set; }
        public virtual StatusApplicationForServices StatusApplicationForServices { get; set; }
        public virtual Users Users { get; set; }
    }
}
