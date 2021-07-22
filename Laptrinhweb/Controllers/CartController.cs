using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Laptrinhweb.Models;
namespace Laptrinhweb.Controllers
{
    public class CartController : Controller
    {
        dbABCDataContext data = new dbABCDataContext();

        public List<Cart> ListCart()
        {
            List<Cart> getcart = Session["Cart"] as List<Cart>;
            if (getcart == null)
            {
                getcart = new List<Cart>();
                Session["Cart"] = getcart;
            }
            return getcart;
        }
        public ActionResult AddCart(int iidpro,string strURL)
        {
            List<Cart> getcart = ListCart();
            Cart c = getcart.Find(n => n.iidpro == iidpro);
            if(c == null)
            {
                c = new Cart(iidpro);
                getcart.Add(c);
                return Redirect(strURL);
            }
            else
            {
                c.icount++;
                return Redirect(strURL);
            }
        }
        private int TotalCount()
        {
            int itotalcount = 0;
            List<Cart> getcart = Session["Cart"] as List<Cart>;
            if(getcart!=null)
            {
                itotalcount = getcart.Sum(n => n.icount);
            }
            return itotalcount;
        }

        private double TotalPrice()
        {
            double itotalprice = 0;
            List<Cart> getcart = Session["Cart"] as List<Cart>;
            if (getcart != null)
            {
                itotalprice = getcart.Sum(n => n.dintoprice);
            }
            return itotalprice;
        }
        public ActionResult Cart()
        {
            List<Cart> getcart = ListCart();
            if(getcart.Count ==0)
            {                 
                return RedirectToAction("All", "ABC");
            }
            ViewBag.TotalCount = TotalCount(); 
            ViewBag.TotalPrice = TotalPrice();
            return View(getcart);
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CartPartial()
        {
            ViewBag.totalcount = TotalCount();
            ViewBag.totalprice = TotalPrice();
            return PartialView();
        }
        public ActionResult DeleteCart(int iidsp)
        {
            List<Cart> getcart = ListCart();
            Cart p = getcart.SingleOrDefault(n => n.iidpro == iidsp);
                if(p != null)
            {
                getcart.RemoveAll(n => n.iidpro == iidsp);
                return RedirectToAction("Cart");
            }
                if(getcart.Count == 0)
            {
                return RedirectToAction("Index", "ABC");
            }
            return RedirectToAction("Cart");
        }
        public ActionResult UpdateCart(int iidsp , FormCollection f)
        {
            List<Cart> getcart = ListCart();
            Cart u = getcart.SingleOrDefault(n => n.iidpro == iidsp);
            if(u != null)
            {
                u.icount = int.Parse(f["txtcount"].ToString());
            }
            return RedirectToAction("Cart");
        }
        public ActionResult DeleteAll()
        {
            List<Cart> getcart = ListCart();
            getcart.Clear();
            return RedirectToAction("All", "ABC");
        }
        [HttpGet]
        public ActionResult Order()
        {
            if (Session["Username"] == null || Session["Username"].ToString() == "")
            {
                return RedirectToAction("login", "Man");
            }
            if (Session["Cart"] == null)
            {
                return RedirectToAction("All", "ABC");
            }
            List<Cart> getcart = ListCart();
            ViewBag.Totalcount = TotalCount();
            ViewBag.Totalprice = TotalPrice();

            return View(getcart);
        }
        [HttpPost]
        public ActionResult Order(FormCollection collection)
        {
            Bill b = new Bill();
            Man m = (Man)Session["Username"];
            List<Cart> getcart = ListCart();
            b.idcus = m.idman;
            b.datein = DateTime.Now;
            var dateout = String.Format("{0:MM/dd/yyyy}", collection["dateout"]);
            b.dateout = DateTime.Parse(dateout);
            b.status = false;
            data.Bills.InsertOnSubmit(b);
            data.SubmitChanges();
            foreach(var item in getcart)
            {
                InfoBill ib = new InfoBill();
                ib.idbill = b.idbill;
                ib.idpro = item.iidpro;
                ib.price = item.dprice;
                ib.count = item.icount;
                data.InfoBills.InsertOnSubmit(ib);
            }
            data.SubmitChanges();
            Session["Cart"] = null;
            return RedirectToAction("AccessOrder", "Cart");
        }
        public ActionResult AccessOrder()
        {
            return View();
        }
    }
}