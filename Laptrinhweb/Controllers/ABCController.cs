using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Laptrinhweb.Models;

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
        public ActionResult ProductAll(int id)
        {
            var proall = from s in data.Products where s.idcate == id select s;
            return View(proall);
        }
       
    }
}