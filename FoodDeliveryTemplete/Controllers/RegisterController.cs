using FoodDeliveryTemplete.Models;
using FoodDeliveryTemplete.ViewModel;
using FoodDeliveryTemplete.ViewModels.Haoming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace FoodDeliveryTemplete.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        private FoodDeliveryEntities db = new FoodDeliveryEntities();
        // GET: Member/Account
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Registed()
        {
            ViewBag.datas = db.tMember_City.ToList();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Registed(MemberRegistedViewModel member, HttpPostedFileBase memberPhoto)
        {
            ViewBag.datas = db.tMember_City.ToList();


            if (db.tMemeber.Any(t => t.fEmail == member.fEmail))
            {
                ViewBag.message = "此電子信箱帳號已被註冊";
            }
            else
            {

                if (ModelState.IsValid)
                {
                    if (memberPhoto != null && memberPhoto.ContentLength > 0)
                    {
                        tMemeber regMember = new tMemeber();

                        regMember.fRoleID = 3;
                        regMember.fJoinDate = DateTime.Today;

                        regMember.fMemeberName = member.fMemeberName;
                        regMember.fEmail = member.fEmail;
                        regMember.fPassword = member.fPassword;
                        regMember.fPasswordConfirm = member.fPasswordConfirm;
                        regMember.fGender = member.fGender;
                        regMember.fBirth = member.fBirth.Date;
                        regMember.fPhone = member.fPhone;
                        regMember.fTel = member.fTel;
                        regMember.fAreaID = member.fAreaID;
                        regMember.fAddress = member.fAddress;
                        //產生驗證碼
                        string ValidNumber = GenerateRandomPassword(8);
                        regMember.fValidNumber = ValidNumber;
                        //將帳號是否開通過寫成false
                        regMember.fActivated = false;

                        db.tMemeber.Add(regMember);
                        db.SaveChanges();

                        var memberId = db.tMemeber.Select(m => m.fMemberID).ToList().Last();

                        tPhoto regPhoto = new tPhoto();

                        regPhoto.fMemberID = memberId;
                        //存路徑
                        regPhoto.fPhotoPath = memberPhoto.FileName;

                        //將上傳的圖轉成二進位

                        var imgSize = memberPhoto.ContentLength;
                        byte[] imgByte = new byte[imgSize];
                        memberPhoto.InputStream.Read(imgByte, 0, imgSize);

                        regPhoto.fBytesImage = imgByte;
                        regPhoto.fDateTime = DateTime.Today;

                        db.tPhoto.Add(regPhoto);
                        db.SaveChanges();


                        //開始寫驗證信給使用者
                        var memberName = db.tMemeber.Where(m => m.fMemberID == memberId).Select(m => m.fMemeberName).FirstOrDefault();
                        var memberEmail = db.tMemeber.Where(m => m.fMemberID == memberId).Select(m => m.fEmail).FirstOrDefault();


                        string Title = "食速列車: 會員您好，驗證碼通知";
                        string Body = "<h3 style=" + "color:lightskyblue" + ">親愛的" + memberName + " 先生/小姐您好:</h3></br>  您的驗證碼碼如下　－ </br></br>";
                        Body += "<h4 style=" + "color:red" + ">" + ValidNumber + "</h4>";
                        Body += "</br></br> 請依據此密碼作為登入之驗證碼碼，謝謝您";


                        await Task.Run(() =>
                        {
                            SendEMail(memberEmail, Title, Body);

                        });



                        return RedirectToAction("activation", "Register", new { id = memberId });
                        //ViewBag.message = member.fMemeberName;
                        //註冊完後所要跳轉的頁面
                        //return RedirectToAction("Index", "Home", new { Area = "Member" });
                    }
                    else
                    {
                        ViewBag.message = "請選擇圖檔!!";
                    }
                }
            }

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LoginRestaurant()
        {
            return View();
        }

        //----------------------------

        public JsonResult GetArea(int id)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            try
            {
                var query = from a in db.tMember_Area
                            where a.fCityID == id
                            select a;


                foreach (var area in query)
                {
                    list.Add(new SelectListItem { Text = area.fAreaName, Value = area.fAreaID.ToString() });
                }


            }

            catch { }
            return Json(new SelectList(list, "Value", "Text"), JsonRequestBehavior.AllowGet);
        }

        public ActionResult ValidEmail(string email)
        {
            string str = "";
            var query = db.tMemeber.Where(m => m.fEmail == email).Count();

            if (query > 0)
            {
                str = "此電子郵件已經註冊過囉!!";
            }

            return Json(str, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult RestaurantRegisted()
        {
            ViewBag.datas = db.tMember_City.ToList();
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> RestaurantRegisted(RestaurantRegistedViewModel member, HttpPostedFileBase memberPhoto)
        {

            ViewBag.datas = db.tMember_City.ToList();

            if (db.tMemeber.Any(t => t.fEmail == member.fEmail))
            {
                ViewBag.message = "此電子信箱帳號已被註冊";
            }

            else
            {
                if (ModelState.IsValid)
                {
                    if (memberPhoto != null && memberPhoto.ContentLength > 0)
                    {
                        tMemeber regRestMember = new tMemeber();

                        regRestMember.fRoleID = 2;
                        regRestMember.fJoinDate = DateTime.Today;
                        //------
                        regRestMember.fMemeberName = member.fMemeberName;
                        regRestMember.fEmail = member.fEmail;
                        regRestMember.fPassword = member.fPassword;
                        regRestMember.fPasswordConfirm = member.fPasswordConfirm;
                        regRestMember.fBirth = member.fBirth.Date;
                        //regRestMember.fPhone = member.fPhone;
                        regRestMember.fTel = member.fTel;
                        regRestMember.fAreaID = member.fAreaID;
                        regRestMember.fAddress = member.fAddress;
                        //產生驗證碼
                        string ValidNumber = GenerateRandomPassword(8);
                        regRestMember.fValidNumber = ValidNumber;
                        //將帳號是否開通過寫成false
                        regRestMember.fActivated = false;



                        //存入座標--yuwnag
                        regRestMember.fLatitude = member.fLatitude;
                        regRestMember.fLongitude = member.fLongitude;

                        db.tMemeber.Add(regRestMember);
                        db.SaveChanges();

                        var memberId = db.tMemeber.Select(m => m.fMemberID).ToList().Last();

                        tPhoto regPhoto = new tPhoto();

                        regPhoto.fMemberID = memberId;
                        //存路徑
                        regPhoto.fPhotoPath = memberPhoto.FileName;

                        //將上傳的圖轉成二進位

                        var imgSize = memberPhoto.ContentLength;
                        byte[] imgByte = new byte[imgSize];
                        memberPhoto.InputStream.Read(imgByte, 0, imgSize);

                        regPhoto.fBytesImage = imgByte;
                        regPhoto.fDateTime = DateTime.Today;

                        db.tPhoto.Add(regPhoto);
                        db.SaveChanges();

                        TempData["name"] = member.fMemeberName;
                        //ViewBag.message = member.fMemeberName;

                        //開始寫驗證信給使用者
                        var memberName = db.tMemeber.Where(m => m.fMemberID == memberId).Select(m => m.fMemeberName).FirstOrDefault();
                        var memberEmail = db.tMemeber.Where(m => m.fMemberID == memberId).Select(m => m.fEmail).FirstOrDefault();


                        string Title = "食速列車: 會員您好，驗證碼通知";
                        string Body = "<h3 style=" + "color:lightskyblue" + ">親愛的" + memberName + " 先生/小姐您好:</h3></br>  您的驗證碼碼如下　－ </br></br>";
                        Body += "<h4 style=" + "color:red" + ">" + ValidNumber + "</h4>";
                        Body += "</br></br> 請依據此密碼作為登入之驗證碼碼，謝謝您";

                        await Task.Run(() =>
                        {
                            SendEMail(memberEmail, Title, Body);

                        });

                        return RedirectToAction("activation", "Register", new { id = memberId });
                    }
                    else
                    {
                        ViewBag.message = "請選擇圖檔!!";
                    }
                }
            }
            return View();

        }

        //驗證畫面
        [HttpGet]
        public ActionResult activation(int id)
        {
            ViewBag.memberName = db.tMemeber.Where(m => m.fMemberID == id).Select(m => m.fMemeberName).FirstOrDefault();
            Session["memberId"] = id;
            return View();
        }

        [HttpPost]
        public ActionResult activation(int id, ActivationViewModel vm)
        {
            ViewBag.memberName = db.tMemeber.Where(m => m.fMemberID == id).Select(m => m.fMemeberName).FirstOrDefault();
            Session["memberId"] = id;

            if (ModelState.IsValid)
            {

                if (vm.fValidNumber == db.tMemeber.Where(m => m.fMemberID == id).Select(m => m.fValidNumber).FirstOrDefault())
                {
                    tMemeber update = db.tMemeber.Find(id);

                    update.fActivated = true;

                    db.Entry(update).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("LoginView", "Account");
                }
                else
                {
                    ViewBag.message = "驗證碼有誤請重新輸入";
                }
            }

            return View();
        }

        //重寄驗證碼
        public async Task<ActionResult> ResendCode(int id)
        {
            var memberName = db.tMemeber.Where(m => m.fMemberID == id).Select(m => m.fMemeberName).FirstOrDefault();
            var memberEmail = db.tMemeber.Where(m => m.fMemberID == id).Select(m => m.fEmail).FirstOrDefault();


            tMemeber update = db.tMemeber.Find(id);
            string ValidNumber = GenerateRandomPassword(8);
            update.fValidNumber = ValidNumber;

            db.Entry(update).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            string Title = "食速列車: 會員您好，驗證碼通知";
            string Body = "<h3 style=" + "color:lightskyblue" + ">親愛的" + memberName + " 先生/小姐您好:</h3></br>  您的驗證碼碼如下　－ </br></br>";
            Body += "<h4 style=" + "color:red" + ">" + ValidNumber + "</h4>";
            Body += "</br></br> 請依據此密碼作為登入之驗證碼碼，謝謝您";

            await Task.Run(() =>
            {
                SendEMail(memberEmail, Title, Body);

            });
            TempData["message"] = "驗證碼已重寄囉!!";
            var message = "驗證碼已重寄囉!!";


            //return RedirectToAction("activation", "Register", new { id = id });

            return Json(new { message }, JsonRequestBehavior.AllowGet);
        }

        //激活帳號部分
        //隨機產生驗證碼
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
        //用非同步寄信
        private async void SendEMail(string emailid, string subject, string body)
        {
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Host = "smtp.live.com";
            client.Port = 25;


            NetworkCredential credentials = new NetworkCredential("foodtotrain@hotmail.com", "Food2017");
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("foodtotrain@hotmail.com");
            msg.To.Add(new MailAddress(emailid));

            msg.Subject = subject;
            msg.IsBodyHtml = true;
            msg.Body = body;

            await client.SendMailAsync(msg);
        }
    }
}