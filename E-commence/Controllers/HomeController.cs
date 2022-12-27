using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using E_commence.Models;

namespace E_commence.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        // ecommenceEntities entities = new ecommenceEntities();
        ecommenceEntities1 entities = new ecommenceEntities1();
        public ActionResult Index()
        {
            return View(entities.Urunlers.ToList());
        }


        public ActionResult KategoriByUrunList(int? id)
        {
            var urunler = entities.Urunlers.AsQueryable();
            if (id != null)
            {
                urunler = entities.Urunlers.Where(x => x.KategoriID == id);
            }
            return View(urunler.ToList());

        }
        public PartialViewResult KategoriMenu()
        {
            return PartialView(entities.Kategorilers.ToList());
        }
        public PartialViewResult SepetToplam()
        {
            int kullaniciID = Convert.ToInt32(Session["UserID"]);
            return PartialView(entities.Sepets.Where(c => c.KullaniciID == kullaniciID && c.SiparisID == null).ToList());
        }
        public PartialViewResult KategoriListIndex()
        {
            return PartialView(entities.Kategorilers.ToList());
        }
        public ActionResult ProductByID(int id)
        {
            return View(entities.Urunlers.Where(c => c.UrunId == id).FirstOrDefault());
        }

        public ActionResult SepeteEkle(int id = 0)
        {
            //sepete ekle sadece
            int kullaniciID = Convert.ToInt32(Session["UserID"]);
            //sepette bu kullanıcının aynı üründen iki tane istediği anlamına gelir

            var sepetkontrol = entities.Sepets.Where(c => c.KullaniciID == kullaniciID && c.UrunID == id).FirstOrDefault();

            Urunler urun = entities.Urunlers.Where(c => c.UrunId == id).FirstOrDefault();
            if (Session["UserID"] != null)
            {
                Sepet newsepet = new Sepet();
                if (sepetkontrol != null)
                {
                    sepetkontrol.Adet += 1;
                    newsepet.Adet = sepetkontrol.Adet;
                    newsepet.Toplam = urun.Fiyat * sepetkontrol.Adet;
                }
                else
                {
                    newsepet.Adet = 1;
                    newsepet.Toplam = urun.Fiyat * 1;

                }

                newsepet.Fiyat = urun.Fiyat;
                newsepet.UrunID = urun.UrunId;
                newsepet.KullaniciID = Convert.ToInt32(Session["UserID"]);

                using (ecommenceEntities1 entities = new ecommenceEntities1())
                {

                    entities.Sepets.Add(newsepet);
                    entities.SaveChanges();
                }

                return RedirectToAction("KategoriByUrunList");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
        public ActionResult Sepet()
        {
            int kullaniciID = Convert.ToInt32(Session["UserID"]);
            return PartialView(entities.Sepets.Where(c => c.KullaniciID == kullaniciID && c.SiparisID == null).ToList());
        }
        public ActionResult SepetSil(int id)
        {
            using (ecommenceEntities1 entities = new ecommenceEntities1())
            {
                Sepet sepetsil = entities.Sepets.Where(x => x.SepetID == id).FirstOrDefault();
                entities.Sepets.Remove(sepetsil);
                entities.SaveChanges();
                TempData["SuccessMessage"] = "silme başarılı";
            }
            return RedirectToAction("Sepet");

        }
        public ActionResult SiparisEkle(double toplam)
        {
            //sepete ekle sadece
            int kullaniciID = Convert.ToInt32(Session["UserID"]);

            //kullanıcının sepetteki siparisID boş olanlara Id ata ve toplam tutarı siparis tablosuna ekle. 
            List<Sepet> sepetlistesi = entities.Sepets.Where(c => c.KullaniciID == kullaniciID && c.SiparisID == null).ToList();

            if (Session["UserID"] != null)
            {

                Siparisler siparis = new Siparisler();
                siparis.Tarih = DateTime.Now;
                siparis.Toplam = Convert.ToDecimal(toplam);
                var sipariseklenen = entities.Siparislers.Add(siparis);
                entities.SaveChanges();

                //sepetdeki siparisIDleri güncelle
                foreach (Sepet sepet in sepetlistesi)
                {
                    sepet.SiparisID = sipariseklenen.SiparisID;
                    entities.Entry(sepet).State = System.Data.Entity.EntityState.Modified;
                    entities.SaveChanges();
                }
                return RedirectToAction("KategoriByUrunList");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public ActionResult SiparisListesi()
        {
            int kullaniciID = Convert.ToInt32(Session["UserID"]);
            if (Session["UserID"] != null)
            {
                //kullanıcının sepetteki siparisID boş olanlara Id ata ve toplam tutarı siparis tablosuna ekle. 

                return View(entities.Siparislers.Where(c => c.KullaniciID == kullaniciID).ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public ActionResult SepetDetaylari(int siparisID)
        {
            int kullaniciID = Convert.ToInt32(Session["UserID"]);
            if (Session["UserID"] != null)
            {
                //gelen sipariş id ye göre sepete atılanları gösterir
                //var listesepet = (from se in entities.Sepets
                //              join si in entities.Siparislers on se.SiparisID equals si.SiparisID
                //              join urun in entities.Urunlers on se.UrunID equals urun.UrunId
                //              where  se.KullaniciID == kullaniciID && se.SiparisID == siparisID
                //              select new SiparislerSepetDetay
                //              {
                //                  SiparisID= si.SiparisID,
                //                  SepetID=se.SepetID,
                //                  UrunID=urun.UrunId,
                //                  UrunAdi=urun.UrunAdi,
                //                  Adet=se.Adet,
                //                  Fiyat=se.Fiyat,
                //                  Aratoplam=se.Toplam,
                //                  SiparisTarihi=si.Tarih

                //              }).ToList();

                //return View(listesepet);
                List<Sepet> liste = entities.Sepets.Where(c => c.KullaniciID == kullaniciID && c.SiparisID == siparisID).ToList();
                return View(liste);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
    }
}