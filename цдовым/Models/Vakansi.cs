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
    
    public partial class Vakansi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vakansi()
        {
            this.Otklki_Soskateley = new HashSet<Otklki_Soskateley>();
        }
    
        public long id_vakansiy { get; set; }
        public long id_rabotadatel { get; set; }
        public string nazvaie_vakansii { get; set; }
        public string Opisanie { get; set; }
        public string trebovanie { get; set; }
        public System.DateTime open_data { get; set; }
        public long id_status { get; set; }
        public decimal zarplata1 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Otklki_Soskateley> Otklki_Soskateley { get; set; }
        public virtual rabotadatel rabotadatel { get; set; }
        public virtual status status { get; set; }
    }
}
