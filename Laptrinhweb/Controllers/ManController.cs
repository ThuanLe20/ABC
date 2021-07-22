using Laptrinhweb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Laptrinhweb.Controllers
{
    public class ManController : Controller
    {
        dbABCDataContext db = new dbABCDataContext();
      
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult signin()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult signin(FormCollection collection, Man kh)
        {
           
            var hoten = collection["HotenKH"];
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            var matkhaunhaplai = collection["Matkhaunhaplai"];
            var diachi = collection["Diachi"];
            var email = collection["Email"];
            var dienthoai = collection["Dienthoai"];
            var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["Ngaysinh"]);
            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Customer name cannot be left blank ";
            }
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "Username must be entered ";
            }
            if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Password must be entered ";
            }
            if (String.IsNullOrEmpty(matkhaunhaplai))
            {
                ViewData["Loi4"] = "Password must be re-entered ";
            }
            if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi5"] = "Email is not vacant ";
            }

            if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi6"] = "Must enter phone ";
            }
            else
            {
               

                kh.name = hoten;
                kh.username = tendn;
                kh.password = matkhau;
                kh.email = email;
                kh.address = diachi;
                kh.phone = dienthoai;
                kh.birth = DateTime.Parse(ngaysinh);
                db.Mans.InsertOnSubmit(kh);
                db.SubmitChanges();
              
            }
            return this.signin();
        }
       
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(FormCollection collection)
        {
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            if(String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Username must be entered ";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Password must be entered";
            }
            else
            {
                Man m = db.Mans.SingleOrDefault(n => n.username == tendn && n.password == matkhau);
                if(m != null)
                {
                    ViewBag.Thongbao = "Congratulations on successful login ";
                    Session["username"] = m;
                    return RedirectToAction("Index", "ABC");
                }
                else
                {
                    ViewBag.Thongbao = "Username or password is incorrect ";
                }
            }

            return View();
        }
    }
}
