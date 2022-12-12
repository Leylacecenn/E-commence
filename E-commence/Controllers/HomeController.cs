using System;
using System.Collections.Generic;
using System.Linq;
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
        public PartialViewResult KategoriListIndex()
        {
            return PartialView(entities.Kategorilers.ToList());
        }
        public ActionResult ProductByID(int id)
        {
            return View(entities.Urunlers.Where(c => c.UrunId == id).FirstOrDefault());
        }
        public void SepeteEkle(int id)
        {
            //burada sadece veri ekle

        }
    }
}