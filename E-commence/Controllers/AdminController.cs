using E_commence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_commence.Controllers
{
    public class AdminController : Controller
    {
      ecommenceEntities1 entities = new ecommenceEntities1();
         // ecommenceEntities entities = new ecommenceEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Urunler()
        {
            return View(entities.Urunlers.ToList());
        }
        public ActionResult UrunEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(Urunler urun)
        {
            try
            {
                using (ecommenceEntities1 entities = new ecommenceEntities1())
                // using (ecommenceEntities entities = new ecommenceEntities())
                {
                    entities.Urunlers.Add(urun);
                    entities.SaveChanges();
                }
                return RedirectToAction("Urunler");
            }
            catch (Exception ex)
            {

                return View(ex);
            }
        }
        public ActionResult UrunDuzenle(int id)
        {
            using (ecommenceEntities1 entities = new ecommenceEntities1())
            // using (ecommenceEntities entities = new ecommenceEntities())
            {
                return View(entities.Urunlers.Where(x => x.UrunId == id).FirstOrDefault());
            }
        }
        [HttpPost]
        public ActionResult UrunDuzenle(int id, Urunler duzenle)
        {
            try
            {
                using (ecommenceEntities1 entities = new ecommenceEntities1())
                // using (ecommenceEntities entities = new ecommenceEntities())
                {
                    entities.Entry(duzenle).State = System.Data.Entity.EntityState.Modified;
                    entities.SaveChanges();
                }
                return RedirectToAction("Urunler");
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        public ActionResult UrunSil(int id)
        {
            using (ecommenceEntities1 entities = new ecommenceEntities1())
            {
                return View(entities.Urunlers.Where(x => x.UrunId == id).FirstOrDefault());
            }
        }
        [HttpPost]
        public ActionResult UrunSil(int id, Urunler sil)
        {
            try
            {
                using (ecommenceEntities1 entities = new ecommenceEntities1())
                {
                    sil = entities.Urunlers.Where(x => x.UrunId == id).FirstOrDefault();
                    entities.Urunlers.Remove(sil);
                    entities.SaveChanges();
                }
                return RedirectToAction("Urunler");
            }
            catch (Exception)
            {

                return View();
            }
        }

        public ActionResult Kategoriler()
        {
            return View(entities.Kategorilers.ToList());
        }
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KategoriEkle(Kategoriler ekle)
        {
            try
            {
                using (ecommenceEntities1 entities = new ecommenceEntities1())
                {
                    entities.Kategorilers.Add(ekle);
                    entities.SaveChanges();
                }
                return RedirectToAction("Kategoriler");
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        public ActionResult KategoriDuzenle(int id)
        {
            using (ecommenceEntities1 entities = new ecommenceEntities1())
            {
                return View(entities.Kategorilers.Where(x => x.KategoriID == id).FirstOrDefault());
            }
        }
        [HttpPost]
        public ActionResult KategoriDuzenle(int id, Kategoriler duzenle)
        {
            try
            {
                using (ecommenceEntities1 entities = new ecommenceEntities1())
                {
                    entities.Entry(duzenle).State = System.Data.Entity.EntityState.Modified;
                    entities.SaveChanges();
                }
                return RedirectToAction("Kategoriler");
            }
            catch (Exception ex)
            {
                return View(ex);
            }
        }
        public ActionResult KategoriSil(int id)
        {
            using (ecommenceEntities1 entities = new ecommenceEntities1())
            {
                return View(entities.Kategorilers.Where(x => x.KategoriID == id).FirstOrDefault());
            }
        }
        [HttpPost]
        public ActionResult KategoriSil(int id, Kategoriler sil)
        {
            try
            {
                using (ecommenceEntities1 entities = new ecommenceEntities1())
                {
                    sil = entities.Kategorilers.Where(x => x.KategoriID == id).FirstOrDefault();
                    entities.Kategorilers.Remove(sil);
                    entities.SaveChanges();
                }
                return RedirectToAction("Kategoriler");
            }
            catch (Exception)
            {

                return View();
            }
        }
    }
}