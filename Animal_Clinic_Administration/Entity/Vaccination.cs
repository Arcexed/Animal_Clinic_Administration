//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Animal_Clinic_Administration.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Vaccination
    {
        public int V_Id { get; set; }
        public string drug { get; set; }
        public DateTime? v_date { get; set; }
        public int? P_Id { get; set; }
    
        public virtual Pet Pet { get; set; }
    }
}