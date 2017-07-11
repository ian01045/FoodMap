using FoodDeliveryTemplete.Areas.Restaurant.Models;
using FoodDeliveryTemplete.Areas.Restaurant.Models.CRUD;
using FoodDeliveryTemplete.Areas.yuwang.ViewModel;
using FoodDeliveryTemplete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static FoodDeliveryTemplete.Models.tCategory;

namespace FoodDeliveryTemplete.Areas.Restaurant.Controllers
{
    public class CategoryController : Controller
    {
        FoodDeliveryEntities db = new FoodDeliveryEntities();
        Models.CRUD.Repository<tCategory> reposCategory = new Models.CRUD.Repository<tCategory>();
        // GET: Restaurant/Category
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult showcategory()
        {
            FoodCategorySelect food_category = new FoodCategorySelect();
            //找出父類別
            food_category.parent_food_category = reposCategory.GetAll().Where(c => c.fParentID == null);

            //找出子類別
            food_category.child_food_cateogry = reposCategory.GetAll().Where(c => c.fParentID != null);

            return PartialView(food_category);
        }
        public ActionResult CategoryCreateFunction()
        {
            return View();
        }

        
        [HttpPost]
        public ActionResult CategoryCreateFunction(tCategory datas)
        {
            int catid = 0;//要填入的parentID
            if (Request.Cookies["catid"] != null)
            {
                string lastCategoryID = Request.Cookies["catid"].Value;
                int.TryParse(lastCategoryID, out catid);
            }

            int? chackParentID;
            if (catid == 0)
            {
                chackParentID = null;
            }
            else { chackParentID = catid; }

            var checkdata = reposCategory.GetAll().Where(c => c.fCategoryName == datas.fCategoryName && c.fParentID == chackParentID);
            
            if (checkdata.Count() == 0) //此分類還沒輸入過
            {
                if (catid == 0)//如果catid == 0 表示目前輸入的為第一層的分類
                {
                    datas.fModifiedDate = DateTime.Today;
                    datas.fCategoryDescription = Request.Form["fCategoryDescription"];
                    datas.fParentID = null;
                    reposCategory.Create(datas);
                }
                else//如果catid != 0 表示目前輸入的不為第一層的分類
                {
                    //不管有沒有parentid 一律使用catid當作parentid(才不會繼承到錯誤的分類)
                    datas.fModifiedDate = DateTime.Today;
                    datas.fCategoryDescription = Request.Form["fCategoryDescription"];
                    datas.fParentID = catid;
                    reposCategory.Create(datas);
                }
                

                //將新增的CategoryID找出來給下一個繼承
                catid = reposCategory.GetAll().Where(c => c.fCategoryName == datas.fCategoryName && c.fParentID == chackParentID).Select(c => c.fCategoryID).Single();
            }
            else //已經輸入過則輸出繼承給別人的parentid
            {
                catid = reposCategory.GetAll().Where(c => c.fCategoryName == datas.fCategoryName && c.fParentID == chackParentID).Select(c => c.fCategoryID).Single();
            }

            Response.Cookies["catid"].Value = catid.ToString();
            Response.Cookies["catid"].Expires = DateTime.Now.AddSeconds(5);
            //Session["catid"] = catid;
            return RedirectToAction("CategoryCreateFunction");
        }
      
    }
}