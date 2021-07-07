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
                ViewData["Loi1"] = "Họ tên khách hàng không được để trống";
            }
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "Phải nhập tên đăng nhập";
            }
            if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Phải nhập mật khẩu";
            }
            if (String.IsNullOrEmpty(matkhaunhaplai))
            {
                ViewData["Loi4"] = "Phải nhập lại mật khẩu";
            }
            if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi5"] = "Email không được bỏ trống";
            }

            if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi6"] = "Phải nhập điện thoai";
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
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                Man m = db.Mans.SingleOrDefault(n => n.username == tendn && n.password == matkhau);
                if(m != null)
                {
                    ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["username"] = m;
                    return RedirectToAction("Index", "ABC");
                }
                else
                {
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }

            return View();
        }
    }
}
