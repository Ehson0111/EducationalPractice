//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace цдовым.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sobesedovanie
    {
        public long id_sobesedovanie { get; set; }
        public long id_otklik { get; set; }
        public System.DateTimeOffset data_sobesedovanie { get; set; }
        public string lokatsiya { get; set; }
        public long id_status { get; set; }
        public long id_itog { get; set; }
    
        public virtual itog_sobesedovanie itog_sobesedovanie { get; set; }
        public virtual Otklki_Soskateley Otklki_Soskateley { get; set; }
        public virtual status status { get; set; }
    }
}
