using FoodDeliveryTemplete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodDeliveryTemplete.Areas.Restaurant.Controllers
{
    public class CustomizedCategoryController : Controller
    {
        FoodDeliveryEntities db = new FoodDeliveryEntities();
        Repository<tRestaurant_FoodCustomizedCategory> reposCostomer = new Repository<tRestaurant_FoodCustomizedCategory>();
        Repository<tFood_CustomizedOption> reposOption = new Repository<tFood_CustomizedOption>();
        // GET: Restaurant/CustomizedCategory
        public ActionResult Index()
        {
            return View();
        }
        //新增客製化分類
        public ActionResult CreateCustomizedCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCustomizedCategory(tRestaurant_FoodCustomizedCategory customerdata)
        {
            int Customizedcatid = 0;//要填入的parentID
            if (Request.Cookies["Customizedcatid"] != null)
            {
                string lastCategoryID = Request.Cookies["Customizedcatid"].Value;
                int.TryParse(lastCategoryID, out Customizedcatid);
            }

            int? chackParentID;
            if (Customizedcatid == 0)
            {
                chackParentID = null;
            }
            else { chackParentID = Customizedcatid; }

            var checkdata = reposCostomer.GetAllInfo().Where(c => c.fCustomizedCategoryName == customerdata.fCustomizedCategoryName 
                                                               && c.fParentID == chackParentID
                                                               && c.fRestaurantID == customerdata.fRestaurantID);
            
            if (checkdata.Count() == 0) //此分類還沒輸入過
            {
                if (Customizedcatid == 0)//如果catid == 0 表示目前輸入的為第一層的分類
                {
                    customerdata.fModifiedDate = DateTime.Today;
                    //customerdata.fCategoryDescription = Request.Form["fCategoryDescription"];
                    customerdata.fParentID = null;
                    reposCostomer.Insert(customerdata);
                }
                else//如果catid != 0 表示目前輸入的不為第一層的分類
                {
                    //不管有沒有parentid 一律使用catid當作parentid(才不會繼承到錯誤的分類)
                    customerdata.fModifiedDate = DateTime.Today;
                    //customerdata.fCategoryDescription = Request.Form["fCategoryDescription"];
                    customerdata.fParentID = Customizedcatid;
                    reposCostomer.Insert(customerdata);
                }
                //將新增的CategoryID找出來給下一個繼承
                Customizedcatid = reposCostomer.GetAllInfo().Where(c => c.fCustomizedCategoryName == customerdata.fCustomizedCategoryName 
                                                                && c.fParentID == chackParentID
                                                                &&c.fRestaurantID == customerdata.fRestaurantID)
                                                                .Select(c => c.fCustomizedCategoryID).Single();
            }
            else //已經輸入過則輸出繼承給別人的parentid
            {
                Customizedcatid = reposCostomer.GetAllInfo().Where(c => c.fCustomizedCategoryName == customerdata.fCustomizedCategoryName 
                                                                     && c.fParentID == chackParentID 
                                                                     && c.fRestaurantID == customerdata.fRestaurantID).Select(c => c.fCustomizedCategoryID).Single();
            }

            Response.Cookies["Customizedcatid"].Value = Customizedcatid.ToString();
            Response.Cookies["Customizedcatid"].Expires = DateTime.Now.AddSeconds(5);
            //Session["catid"] = catid;
            return RedirectToAction("CreateCustomizedCategory", "CustomizedCategory");
        }

        public ActionResult CustomizedOption(int CostomizedCategoryID)
        {
            var datas= reposOption.GetAllInfo().Where(c => c.fCustomizedCategoryID == CostomizedCategoryID).ToList();
            return PartialView(datas);
        }

        //新增客製化選項
        public ActionResult CreateCustomizedOption()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult CreateCustomizedOption(tFood_CustomizedOption options)
        {
            reposOption.Insert(options);
            return RedirectToAction("CreateCustomizedOption");
        }
        public ActionResult DeletCustomizedOption(int id)
        {
            reposOption.Delete(reposOption.GetID(id));
            return RedirectToAction("CreateCustomizedOption");
        }
    }
}