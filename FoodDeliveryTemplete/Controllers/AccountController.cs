using FoodDeliveryTemplete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;
using System.Net.Mail;
using System.Net;

namespace FoodDeliveryTemplete.Controllers
{
    public class AccountController : Controller
    {
        
        FoodDeliveryEntities db = new FoodDeliveryEntities();
        // GET: AccountForget
        public ActionResult Index()
        {            
            return View();
        }

        public ActionResult Login(string Account, string Password,string Check=null)
        {
            //主頁面的Login          
            if (ModelState.IsValid)
            {               
                var userLogin = db.tMemeber.Where(c => c.fEmail == Account & c.fPassword == Password).FirstOrDefault();           
                var MemberID = db.tMemeber.Where(c => c.fEmail == Account).Select(c => c.fMemberID).FirstOrDefault();             
                if (userLogin != null)
                {                  
                    Response.Cookies["MemberID"].Value = MemberID.ToString();
                    Response.Cookies["AccountLogin"].Value = Account;
                    var roleNumber = db.tMemeber.Where(c => c.fEmail == Account).Select(c => c.fRoleID).FirstOrDefault();

                    if (Check == "Yes")
                    {                      
                        Response.Cookies["MemberID"].Expires = DateTime.Now.AddDays(5);
                        Response.Cookies["AccountLogin"].Expires = DateTime.Now.AddDays(5);
                        Response.Cookies["RoleNum"].Expires = DateTime.Now.AddDays(5);
                    }

                    if (roleNumber == 1)
                    {
                        Session["MemberID"] = MemberID;
                        Response.Cookies["RoleNum"].Value = "1";
                        return RedirectToAction("Index", "Home", new { Area = "Administrator" });
                    }
                    if (roleNumber == 2)
                    {
                        var RestaurantID = (from i in db.tRestaurant
                                            where i.fMemberID == MemberID
                                            select i.fRestaurantID).SingleOrDefault(); 
                        Response.Cookies["RestaurantID"].Value = RestaurantID.ToString();
                        Session["MemberID"] = MemberID;
                        
                        Response.Cookies["RoleNum"].Value = "2";                        
                        return RedirectToAction("Index", "Restaurant", new { Area = "Restaurant" });
                    }
                    if (roleNumber == 3)
                    {
                        Session["MemberID"] = MemberID;
                        Response.Cookies["RoleNum"].Value = "3";
                        return RedirectToAction("Index", "Home", new { Area = "Member" });
                    }
                    //string returnUrl = "~/Home/Index";
                    //if (Request.QueryString["returnUrl"] != null)
                    //{
                    //    returnUrl = Request.QueryString["returnUrl"];
                    //}
                }
            }
            return View();
        }

        public ActionResult failLogin(string Account,string Password, string fail = null)
        {
            //判斷登入的資訊或帳號是否有誤
            if (string.IsNullOrEmpty(Account) || string.IsNullOrEmpty(Password))
            {
                fail = "請輸入帳號密碼";
                return Json(new {fail}, JsonRequestBehavior.AllowGet);
            }
            //Server端判別帳號密碼是否有效
            if (ModelState.IsValid)
            {
                var userLogin = db.tMemeber.Where(c => c.fEmail == Account & c.fPassword == Password).FirstOrDefault();
                if (userLogin != null)
                {
                    fail = null;
                    return Json(new { fail }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    fail = "帳號或密碼有誤，請重新輸入";
                    return Json(new { fail }, JsonRequestBehavior.AllowGet);
                }
            }
            fail = "帳號或密碼不正確，請重新輸入";
            return Json(new { fail }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult LoginView()
        {
            if (Request.Cookies["AccountLogin"]!= null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult LoginView(LoginView form)
        {          
            ViewBag.Error = null;
            if (ModelState.IsValid)
            {
                var userLogin = db.tMemeber.Where(c => c.fEmail == form.Account & c.fPassword == form.Password).FirstOrDefault();
                var MemberID = db.tMemeber.Where(c => c.fEmail == form.Account).Select(c => c.fMemberID).FirstOrDefault();
                if (userLogin != null)
                {
                    Response.Cookies["MemberID"].Value = MemberID.ToString();
                    Response.Cookies["AccountLogin"].Value = form.Account;
                    var roleNumber = db.tMemeber.Where(c => c.fEmail == form.Account).Select(c => c.fRoleID).FirstOrDefault();

                    if (form.Check == "Yes")
                    {
                        Response.Cookies["MemberID"].Expires = DateTime.Now.AddDays(5);
                        Response.Cookies["AccountLogin"].Expires = DateTime.Now.AddDays(5);
                        Response.Cookies["RoleNum"].Expires = DateTime.Now.AddDays(5);
                    }

                    if (roleNumber == 1)
                    {
                        Response.Cookies["RoleNum"].Value = "1";
                        return RedirectToAction("Index", "Home", new { Area = "Administrator" });
                    }
                    if (roleNumber == 2)
                    {
                        Response.Cookies["RoleNum"].Value = "2";
                        return RedirectToAction("Index", "Restaurant", new { Area = "Restaurant" });
                    }
                    if (roleNumber == 3)
                    {
                        Response.Cookies["RoleNum"].Value = "3";
                        return RedirectToAction("Index", "Home", new { Area = "Member" });
                    }                    
                }
                ViewBag.Error = "<div class=\"alert alert-danger\" role=\"alert\"><span class=\"glyphicon glyphicon-exclamation-sign\" aria-hidden=\"true\"></span><span class=\"sr-only\">Error:</span>帳號或密碼有誤，請重新輸入</div>";
                return View();
            }
            
            return View();
        }
        
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]          
        public ActionResult ForgotPassword(tMemeber member, string fail = null)
        {
            if (ModelState.IsValid)
            {                
                var Email = db.tMemeber.Where(c => c.fEmail == member.fEmail).Select(c => c.fEmail).FirstOrDefault();       
                if (Email == null)
                {
                    fail = "此電子信箱尚未註冊，請重新確認";
                    return Json(new { fail }, JsonRequestBehavior.AllowGet);                    
                }
                var MemberName = db.tMemeber.Where(c => c.fEmail == member.fEmail).Select(c => c.fMemeberName).FirstOrDefault();
                var MemberID = db.tMemeber.Where(c => c.fEmail == member.fEmail).Select(c => c.fMemberID).FirstOrDefault();
                tMemeber UpdatePsd = db.tMemeber.Find(MemberID);
                string NewPassword = GenerateRandomPassword(8);
                string Title = "食宿列車: 會員您好，更新密碼通知";
                string Body = "<h3 style="+ "color:lightskyblue" + ">親愛的" + MemberName + " 先生/小姐您好:</h3></br>  您的更新密碼如下　－ </br></br>";
                Body += "<h4 style=" + "color:red" + ">" + NewPassword+"</h4>";
                Body += "</br></br> 請依據此密碼作為登入之密碼，謝謝您";
                try
                {
                    UpdatePsd.fPassword = NewPassword;
                    db.Entry(UpdatePsd).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    SendEMail(Email, Title, Body);
                    //TempData["Message"] = "Mail Sent.";
                }
                catch (Exception ex)
                {
                    //TempData["Message"] = "Error occured while sending email." + ex.Message;
                }
                fail = null;
                return Json(new {  }, JsonRequestBehavior.AllowGet);
            }
            fail = "填寫信箱，如foodtotrain@gmail.com";
            return Json(new { fail }, JsonRequestBehavior.AllowGet);
        }

        private void SendEMail(string emailid, string subject, string body)
        {
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Host = "smtp.live.com";
            client.Port = 25;


            NetworkCredential credentials = new NetworkCredential("henry0223@hotmail.com", "kobe080223kobe");
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("henry0223@hotmail.com");
            msg.To.Add(new MailAddress(emailid));

            msg.Subject = subject;
            msg.IsBodyHtml = true;
            msg.Body = body;

            client.Send(msg);
        }
        
        public ActionResult ResetPassword()
        {          
            return View();
        }

        public ActionResult ResetPasswordFail(LoginBinding form,string state=null)
        {
            if (string.IsNullOrEmpty(form.Account) || string.IsNullOrEmpty(form.Password) || string.IsNullOrEmpty(form.ConfirmResetPassword) || string.IsNullOrEmpty(form.ResetPassword))
            {
                state = "請輸入完整資訊";
                return Json(new { state }, JsonRequestBehavior.AllowGet);
            }
            if (form.ConfirmResetPassword != form.ResetPassword)
            {
                state = "確認密碼與新密碼不相符";
                return Json(new { state }, JsonRequestBehavior.AllowGet);
            }

            var Condition = db.tMemeber.Where(c => c.fEmail == form.Account && c.fPassword == form.Password).FirstOrDefault();

            if (Condition == null)
            {
                state = "帳號不存在或密碼錯誤，請重新輸入";
                return Json(new { state }, JsonRequestBehavior.AllowGet);
            }
            var MemberID = db.tMemeber.Where(c => c.fEmail == form.Account).Select(c => c.fMemberID).FirstOrDefault();
            tMemeber member = db.tMemeber.Find(MemberID);
            member.fPassword = form.ResetPassword;
            db.Entry(member).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Json(new { state }, JsonRequestBehavior.AllowGet);

        }

        private string GenerateRandomPassword(int length)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            char[] chars = new char[length];
            Random rd = new Random();
            for (int i = 0; i < length; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }
            return new string(chars);
        }

    }   
}