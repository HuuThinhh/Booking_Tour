using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Text;
using MyClass.Models;

namespace WebDuLich.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            //Kiểm tra Email và Password trống?
            if (string.IsNullOrEmpty(email) == true | string.IsNullOrEmpty(password) == true)
            {
                ViewBag.thongbao = "Hãy điền đầy đủ Email và Password";
                return View();
            }
            //Tìm acc trong db
            var customer = new mapCustomer().TimKiem(email);
            //Kiểm tra tồn tại
            if (customer == null)
            {
                ViewBag.thongbao = "Tài khoản không tồn tại";
                ViewBag.email = email;
                return View();
            }
            if (customer.Password != password)
            {
                ViewBag.thongbao = "Email hoặc mật khẩu chưa đúng";
                ViewBag.email = email;
                return View();
            }
            //Lưu session
            Session["user"] = customer;
            //Chuyển về trang chủ
            return RedirectToAction("Index");
        }
        public ActionResult CamNang()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session.Remove("user");
            return RedirectToAction("Login");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Customer model)
        {
            mapCustomer map = new mapCustomer();
            model.Address = "";
            if (ModelState.IsValid)
            {
                if (map.TimKiem(model.Email) != null)
                {
                    ViewBag.thongbao = "Email hoặc Số điện thoại đã tồn tại";
                    return View(model);
                }
                if (map.ThemMoi(model) == true)
                {                  
                    TempData["message"] = new XMessage("success", "Bạn đã đăng ký thành công");
                    return RedirectToAction("Login");
                }
                else
                {
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }
        public ActionResult ThongTin()
        {
            return View();
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> ForgotPassword(string email, string phone)
        {
            if (string.IsNullOrEmpty(email) == true | string.IsNullOrEmpty(phone) == true)
            {
                ViewBag.thongbao = "Hãy điền đầy đủ Email và Số điện thoại";
                return View();
            }
            //Tìm acc trong db
            var customer = new mapCustomer().TimKiem(email);
            //Kiểm tra tồn tại
            if (customer == null)
            {
                ViewBag.thongbao = "Tài khoản không tồn tại";
                ViewBag.email = email;
                return View();
            }
            if (customer.Phone != phone)
            {
                ViewBag.thongbao = "Số điện thoại không đúng";
                ViewBag.phone = phone;
                return View();
            }

            await Library.Mail.SendMailGoogleSmtp("QuangDaTour@gmail.com", customer.Email, "Gửi lại mật khẩu", "Mật khẩu của bạn là: " + customer.Password, "baovuong158@gmail.com", "ljze qnww mvrc hljl");
            ViewBag.thongbao = "Kiểm tra địa chỉ email để lấy lại mật khẩu";
            return View();
        }
    }
}