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
    
    public partial class MetersData
    {
        public int ID_MetersData { get; set; }
        public Nullable<decimal> ElectricityReading { get; set; }
        public Nullable<decimal> ColdWaterReading { get; set; }
        public Nullable<decimal> HotWaterReading { get; set; }
        public Nullable<decimal> HeatingReading { get; set; }
        public Nullable<System.DateTime> DateOfTestimony { get; set; }
        public Nullable<int> ID_PersonalAccount { get; set; }
    
        public virtual PersonalАccount PersonalАccount { get; set; }
    }
}
