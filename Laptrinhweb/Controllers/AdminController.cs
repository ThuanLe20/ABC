using Laptrinhweb.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Laptrinhweb.Controllers
{
    public class AdminController : Controller
    {
        dbABCDataContext data = new dbABCDataContext();
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["TaikhoanAdmin"] == null)
                return RedirectToAction("login", "Admin");
            else
                return View();
        }
        /*---------------Product----------------*/
        public ActionResult Product(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 6;
            if (Session["TaikhoanAdmin"] != null)
                return View(data.Products.ToList().OrderByDescending(n => n.idpro).ToPagedList(pageNumber, pageSize));
            else
                return RedirectToAction("login", "Admin");

        }
        [HttpGet]
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
                Admin ad = data.Admins.SingleOrDefault(n => n.useradmin == tendn && n.passadmin == matkhau);
                if (ad != null)
                {
                    // ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["TaikhoanAdmin"] = ad;
                    return RedirectToAction("Index", "Admin");
                }
                else
                    ViewBag.Thongbao = "Username or password is in correct";
            }
            return View();
        }
        [HttpGet]
        public ActionResult AddProduct()
        {
            if (Session["TaikhoanAdmin"] != null)
            {
                ViewBag.idcate = new SelectList(data.Categories.ToList().OrderBy(n => n.name), "idcate", "name");
                return View();
            }
            else
                return RedirectToAction("login", "Admin");
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddProduct(Product pro, HttpPostedFileBase upload)
        {
            if (Session["TaikhoanAdmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                ViewBag.idcate = new SelectList(data.Categories.ToList().OrderBy(n => n.name), "idcate", "name");
                if (upload == null)
                {
                    ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                    return View();
                }
                //Them vao CSDL
                else
                {
                    if (ModelState.IsValid)
                    {
                        //Luu ten fie, luu y bo sung thu vien using System.IO;
                        var fileName = Path.GetFileName(upload.FileName);
                        //Luu duong dan cua file
                        var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                        //Kiem tra hình anh ton tai chua?
                        if (System.IO.File.Exists(path))
                            ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                        else
                        {
                            //Luu hinh anh vao duong dan
                            upload.SaveAs(path);
                        }
                        pro.image = fileName;
                        //Luu vao CSDL
                        data.Products.InsertOnSubmit(pro);
                        data.SubmitChanges();
                    }
                    return RedirectToAction("Product", "Admin");
                }
            }
        }
        public ActionResult Details(int id)
        {
            if (Session["TaikhoanAdmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var pro = from s in data.Products where s.idpro == id select s;
                return View(pro.Single());
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["TaikhoanAdmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var pro = from s in data.Products where s.idpro == id select s;
                return View(pro.Single());
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult ApplyDelete(int id)
        {
            if (Session["TaikhoanAdmin"] == null)
                return RedirectToAction("login", "Admin");
            else
            {
                Product pro = data.Products.SingleOrDefault(n => n.idpro == id);
                data.Products.DeleteOnSubmit(pro);
                data.SubmitChanges();
                return RedirectToAction("Product", "Admin");
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["TaikhoanAdmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                Product pro = data.Products.SingleOrDefault(n => n.idpro == id);
                if (pro == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                ViewBag.idcate = new SelectList(data.Categories.ToList().OrderBy(n => n.name), "idcate", "name");
                return View(pro);
            }
        }
        [HttpPost,ActionName("Edit")]
        [ValidateInput(false)]
        public ActionResult EditProduct(int id)
        {
            if (Session["TaikhoanAdmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                Product pro = data.Products.SingleOrDefault(n => n.idpro == id);
                UpdateModel(pro);
                data.SubmitChanges();
                return RedirectToAction("Product", "Admin");

            }
        }

        /*---------------Admin----------------*/
        public ActionResult Account(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 6;
            if (Session["TaikhoanAdmin"] != null)
                return View(data.Admins.ToList().OrderByDescending(n => n.useradmin).ToPagedList(pageNumber, pageSize));
            else
                return RedirectToAction("login", "Admin");
        }
        [HttpGet]
        public ActionResult AddAccount()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddAccount(Admin ad)
        {
            data.Admins.InsertOnSubmit(ad);
            data.SubmitChanges();
            return RedirectToAction("Account");
        }

        [HttpGet]
        public ActionResult DeleteAccount(string id)
        {
            if (Session["TaikhoanAdmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var ad = from s in data.Admins where s.useradmin == id select s;
                return View(ad.Single());
            }
        }
        [HttpPost, ActionName("DeleteAccount")]
        public ActionResult ApplyDeleteAccount(string id)
        {
            if (Session["TaikhoanAdmin"] == null)
                return RedirectToAction("login", "Admin");
            else
            {
                Admin ad = data.Admins.SingleOrDefault(n => n.useradmin == id);
                data.Admins.DeleteOnSubmit(ad);
                data.SubmitChanges();
                return RedirectToAction("Admin", "Admin");
            }
        }
        [HttpGet]
        public ActionResult EditAccount(string id)
        {
            if (Session["TaikhoanAdmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                Admin ad = data.Admins.SingleOrDefault(n => n.useradmin == id);
                if (ad == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }             
                return View(ad);
            }
        }
        [HttpPost, ActionName("EditAccount")]
        [ValidateInput(false)]
        public ActionResult EditAccount1(Admin ad)
        { 
                    UpdateModel(ad);
                    data.SubmitChanges();                
                return RedirectToAction("Account");         
        }

        /*---------------Category----------------*/
        public ActionResult Category(int ?page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            if (Session["TaikhoanAdmin"] != null)
                return View(data.Categories.ToList().OrderByDescending(n => n.idcate).ToPagedList(pageNumber, pageSize));
            else
                return RedirectToAction("login", "Admin");
        }
        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddCategory(Category ca)
        {
            data.Categories.InsertOnSubmit(ca);
            data.SubmitChanges();
            return RedirectToAction("Category");
        }
        [HttpGet]
        public ActionResult DeleteCategory(int id)
        {
            if (Session["TaikhoanAdmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var ca = from s in data.Categories where s.idcate == id select s;
                return View(ca.Single());
            }
        }
        [HttpPost, ActionName("DeleteCategory")]
        public ActionResult ApplyDeleteCategory(int id)
        {
            if (Session["TaikhoanAdmin"] == null)
                return RedirectToAction("login", "Admin");
            else
            {
                Category ca = data.Categories.SingleOrDefault(n => n.idcate == id);
                data.Categories.DeleteOnSubmit(ca);
                data.SubmitChanges();
                return RedirectToAction("Category", "Admin");
            }
        }
        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            if (Session["TaikhoanAdmin"] == null)
                return RedirectToAction("login", "Admin");
            else
            {
                Category ca = data.Categories.SingleOrDefault(n => n.idcate == id);
                if (ca == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(ca);
            }
        }
        [HttpPost, ActionName("EditCategory")]
        [ValidateInput(false)]
        public ActionResult EditCategory1(Category ca)
        {
            UpdateModel(ca);
            data.SubmitChanges();
            return RedirectToAction("Category");
        }
        /*---------------Bill----------------*/
        public ActionResult Bill(int ?page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            if (Session["TaikhoanAdmin"] != null)
                return View(data.Bills.ToList().OrderByDescending(n => n.idbill).ToPagedList(pageNumber, pageSize));
            else
                return RedirectToAction("login", "Admin");
        }
        /*---------------Man----------------*/

    }
}
