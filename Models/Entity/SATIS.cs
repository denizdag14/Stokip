//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace POStock.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class SATIS
    {
        public int SatisID { get; set; }
        public Nullable<int> Urun { get; set; }
        public Nullable<int> Musteri { get; set; }
        public Nullable<int> Adet { get; set; }
        public Nullable<decimal> BirimFiyat { get; set; }
        public Nullable<decimal> ToplamFiyat { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        public virtual MUSTERI MUSTERI1 { get; set; }
        public virtual URUN URUN1 { get; set; }
    }
}
