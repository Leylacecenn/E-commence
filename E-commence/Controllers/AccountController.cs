using E_commence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.PeerToPeer;
using System.Web;
using System.Web.Mvc;

namespace E_commence.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        ecommenceEntities1 entities = new ecommenceEntities1();
        public ActionResult Index()
        {
            using (ecommenceEntities1 db = new ecommenceEntities1())
            {
                return View(db.Kullanicilars.ToList());
            }
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(Kullanicilar account)
        {
            try
            {
                using (ecommenceEntities1 db = new ecommenceEntities1())
                {
                    db.Kullanicilars.Add(account);
                    db.SaveChanges();
                    ModelState.Clear();
                    ViewBag.Message = account.AdSoyad + " " + " successfully registered.";
                }
                return View();
            }
            catch (Exception ex)
            {
                return View(ex);
            }
           
           
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Kullanicilar user)
        {
            using (ecommenceEntities1 db = new ecommenceEntities1())
            {
                try
                {
                     var kullanici = db.Kullanicilars.Where(u => u.KullaniciAdi == user.KullaniciAdi && u.Sifre == user.Sifre).FirstOrDefault();
                    if (kullanici != null)
                    {
                        Session["UserID"] = kullanici.KullaniciID.ToString();
                        Session["Username"] = kullanici.KullaniciAdi.ToString();
                        return RedirectToAction("LoggedIn");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Şifre veya Kullanıcı Hatalı");
                    }

                    return View();
                }
                catch (Exception ex)
                {
                    return View(ex);
                }
                
            }

        }
        public ActionResult LoggedIn()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}