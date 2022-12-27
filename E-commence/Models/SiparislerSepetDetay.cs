using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_commence.Models
{
    public class SiparislerSepetDetay
    {
        [Key]
        public int SepetID { get; set; }
        public int UrunID { get; set; }
        public int SiparisID { get; set; }
        public string UrunAdi { get; set; }
        public int? Adet { get; set; }
        public decimal? Fiyat { get; set; }
        public decimal? Aratoplam { get; set; }
        public DateTime? SiparisTarihi { get; set; }
    }
}