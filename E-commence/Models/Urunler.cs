//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace E_commence.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Urunler
    {
        public int UrunId { get; set; }
        public string UrunAdi { get; set; }
        public Nullable<decimal> Fiyat { get; set; }
        public string Aciklama { get; set; }
        public string Gorsel1 { get; set; }
        public string Gorsel2 { get; set; }
        public Nullable<int> KategoriID { get; set; }
    
        public virtual Kategoriler Kategoriler { get; set; }
    }
}
