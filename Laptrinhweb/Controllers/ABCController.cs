using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Laptrinhweb.Models;
using PagedList;
using PagedList.Mvc;

namespace Laptrinhweb.Controllers
{
    public class ABCController : Controller
    {
        dbABCDataContext data = new dbABCDataContext();
     
        public ActionResult Index()
        {
         
            return View();
        }

        public ActionResult Product()
        {
            var product = from pro in data.Products select pro;
            return PartialView(product);
        }
        public ActionResult Details(int id)
        {
            var pro = from s in data.Products
                       where s.idpro == id
                       select s;
            return View(pro.Single());
        }
        public ActionResult All()
        {
            var cate = from cd in data.Categories select cd;
            return View(cate);
        }
        public ActionResult ProductAll(int id , int ? page)
        {
            int pageSize = 5;
            int pageNum = (page ?? 1);
            var proall = from s in data.Products where s.idcate == id select s;
          
            return View(proall);
        }
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            // Gán các giá trị người dùng nhập liệu cho các biến 
            var tendn = collection["username"];
            var matkhau = collection["password"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "You must enter login name";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "You must enter login password";
            }
            else
            {
                //Gán giá trị cho đối tượng được tạo mới (kh)
                Man m = data.Mans.SingleOrDefault(n => n.username == tendn && n.password == matkhau);
                if (m != null)
                {
                    // ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["Username"] = m;
                    return RedirectToAction("Index", "ABC");
                }
                else
                    ViewBag.Thongbao = "Username or password is in correct";
            }
            return View();
        }
    }
}