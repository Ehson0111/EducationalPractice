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
    
    public partial class Predlozhenie_o_rabote
    {
        public long id_predlozhenite { get; set; }
        public long id_otklik { get; set; }
        public long id_status { get; set; }
        public long id_rabotadatel { get; set; }
    
        public virtual Otklki_Soskateley Otklki_Soskateley { get; set; }
        public virtual rabotadatel rabotadatel { get; set; }
        public virtual rabotadatel rabotadatel1 { get; set; }
        public virtual status status { get; set; }
    }
}
