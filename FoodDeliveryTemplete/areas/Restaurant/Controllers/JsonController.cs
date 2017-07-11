using FoodDeliveryTemplete.Areas.Restaurant.Models.CRUD;
using FoodDeliveryTemplete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodDeliveryTemplete.Areas.Restaurant.Controllers
{
    public class JsonController : Controller
    {
        // GET: Restaurant/Json
        public ActionResult Index()
        {
            return View();
        }
        FoodDeliveryEntities db = new FoodDeliveryEntities();
        Models.CRUD.Repository<tCategory> reposCategory = new Models.CRUD.Repository<tCategory>();
        Models.CRUD.Repository<tRestaurant_FoodCustomizedCategory> reposCostomerCategory = new Models.CRUD.Repository<tRestaurant_FoodCustomizedCategory>();
        Models.CRUD.Repository<tFood_CustomizedOption> reposOption = new Models.CRUD.Repository<tFood_CustomizedOption>();
        Models.CRUD.Repository<tPhoto> reposPhoto = new Models.CRUD.Repository<tPhoto>();
        Models.CRUD.Repository<tRestaurant_Foods> reposRestaurantFood = new Models.CRUD.Repository<tRestaurant_Foods>();
        public ActionResult GetFirstCategoryData()
        {
            var tCategorydatas = reposCategory.GetAll().Where(c => c.fParentID == null).ToList();
            List<CategoryToJSON> CategoryToJSON = new List<CategoryToJSON>();
            CategoryToJSON data = new CategoryToJSON();
            data.CategoryName = "請選擇";
            CategoryToJSON.Add(data);
            foreach (var CategoryData in tCategorydatas)
            {
                data = new CategoryToJSON();
                data.CategoryId = CategoryData.fCategoryID;
                data.CategoryName = CategoryData.fCategoryName;
                data.CategoryParent = CategoryData.fParentID.ToString();
                CategoryToJSON.Add(data);
            }

            var json = Json(CategoryToJSON.ToList(), JsonRequestBehavior.AllowGet);

            return json;
        }
        public ActionResult GetFirstCategoryDataForUpdate(int categoryid)
        {
            var tCategorydatas = reposCategory.GetAll().Where(c => c.fParentID == null).ToList();
            List<CategoryToJSON> CategoryToJSON = new List<CategoryToJSON>();
            CategoryToJSON data = new CategoryToJSON();
            data.CategoryName = reposCategory.getbyid(categoryid).fCategoryName;
            data.CategoryId = reposCategory.getbyid(categoryid).fCategoryID;
            CategoryToJSON.Add(data);
            foreach (var CategoryData in tCategorydatas)
            {
                data = new CategoryToJSON();
                data.CategoryId = CategoryData.fCategoryID;
                data.CategoryName = CategoryData.fCategoryName;
                data.CategoryParent = CategoryData.fParentID.ToString();
                CategoryToJSON.Add(data);
            }

            var json = Json(CategoryToJSON.ToList(), JsonRequestBehavior.AllowGet);

            return json;
        }
        public ActionResult GetSECCategoryData(int CategoryID)
        {
            var tCategorydatas = reposCategory.GetAll().Where(c => c.fParentID == CategoryID).ToList();
            List<CategoryToJSON> CategoryToJSON = new List<CategoryToJSON>();
            CategoryToJSON data = new CategoryToJSON();
            data.CategoryName = "請選擇";
            CategoryToJSON.Add(data);
            foreach (var CategoryData in tCategorydatas)
            {
                data = new CategoryToJSON();
                data.CategoryId = CategoryData.fCategoryID;
                data.CategoryName = CategoryData.fCategoryName;
                data.CategoryParent = CategoryData.fParentID.ToString();
                CategoryToJSON.Add(data);
            }

            var json = Json(CategoryToJSON.ToList(), JsonRequestBehavior.AllowGet);

            return json;
        }
        public ActionResult GetAllCategoryData()
        {
            var tCategorydatas = reposCategory.GetAll().ToList();
            List<CategoryToJSON> CategoryToJSON = new List<CategoryToJSON>();
            CategoryToJSON data = new CategoryToJSON();
            data.CategoryName = "請選擇";
            CategoryToJSON.Add(data);
            foreach (var CategoryData in tCategorydatas)
            {
                data = new CategoryToJSON();
                data.CategoryId = CategoryData.fCategoryID;
                data.CategoryName = CategoryData.fCategoryName;
                data.CategoryParent = CategoryData.fParentID.ToString();
                CategoryToJSON.Add(data);
            }

            var json = Json(CategoryToJSON.ToList(), JsonRequestBehavior.AllowGet);

            return json;
        }

        public ActionResult GetAllCostomized()
        {
            int RestaurantID = int.Parse(Request.Cookies["RestaurantID"].Value);
            var tCostomerCategorydatas = reposCostomerCategory.GetAll().Where(c=>c.fRestaurantID== RestaurantID).ToList();
            List<CostomerCategoryToJson> CostomerCategoryToJson = new List<CostomerCategoryToJson>();
            CostomerCategoryToJson data = new CostomerCategoryToJson();
            data.CustomizedCategoryName = "請選擇";
            CostomerCategoryToJson.Add(data);
            foreach (var CategoryData in tCostomerCategorydatas)
            {
                data = new CostomerCategoryToJson();
                data.CustomizedCategoryID = CategoryData.fCustomizedCategoryID;
                data.CustomizedCategoryName = CategoryData.fCustomizedCategoryName;
                data.ParentID = CategoryData.fParentID;
                data.OptionCount = reposOption.GetAll().Where(c => c.fCustomizedCategoryID == CategoryData.fCustomizedCategoryID).Count();
                CostomerCategoryToJson.Add(data);
            }

            var json = Json(CostomerCategoryToJson.ToList(), JsonRequestBehavior.AllowGet);

            return json;
        }

        public ActionResult GetAllCostomizedForUpdate(int categoryid)
        {
            int RestaurantID = int.Parse(Request.Cookies["RestaurantID"].Value);
            var tCostomerCategorydatas = reposCostomerCategory.GetAll().Where(c => c.fRestaurantID == RestaurantID).ToList();
            List<CostomerCategoryToJson> CostomerCategoryToJson = new List<CostomerCategoryToJson>();

            CostomerCategoryToJson data = new CostomerCategoryToJson();

            if (reposCostomerCategory.getbyid(categoryid).fCustomizedCategoryName != null)
            {
                data.CustomizedCategoryName = reposCostomerCategory.getbyid(categoryid).fCustomizedCategoryName;
                data.CustomizedCategoryID = reposCostomerCategory.getbyid(categoryid).fCustomizedCategoryID;
            }
            CostomerCategoryToJson.Add(data);
            foreach (var CategoryData in tCostomerCategorydatas)
            {
                data = new CostomerCategoryToJson();
                data.CustomizedCategoryID = CategoryData.fCustomizedCategoryID;
                data.CustomizedCategoryName = CategoryData.fCustomizedCategoryName;
                data.ParentID = CategoryData.fParentID;
                data.OptionCount = reposOption.GetAll().Where(c => c.fCustomizedCategoryID == CategoryData.fCustomizedCategoryID).Count();
                CostomerCategoryToJson.Add(data);
            }

            var json = Json(CostomerCategoryToJson.ToList(), JsonRequestBehavior.AllowGet);

            return json;
        }

        public ActionResult GetCostomizedParentIsNull()
        {
            int RestaurantID = int.Parse(Request.Cookies["RestaurantID"].Value);
            var tCostomerCategorydatas = reposCostomerCategory.GetAll().Where(c=>c.fParentID==null && c.fRestaurantID== RestaurantID).ToList();
            List<CostomerCategoryToJson> CostomerCategoryToJson = new List<CostomerCategoryToJson>();
            CostomerCategoryToJson data = new CostomerCategoryToJson();
            data.CustomizedCategoryName = "請選擇";
            CostomerCategoryToJson.Add(data);
            foreach (var CategoryData in tCostomerCategorydatas)
            {
                data = new CostomerCategoryToJson();
                data.CustomizedCategoryID = CategoryData.fCustomizedCategoryID;
                data.CustomizedCategoryName = CategoryData.fCustomizedCategoryName;
                data.ParentID = CategoryData.fParentID;
                CostomerCategoryToJson.Add(data);
            }

            var json = Json(CostomerCategoryToJson.ToList(), JsonRequestBehavior.AllowGet);

            return json;
        }
        public ActionResult GetCostomizedParentForUpdate(int CustomizedCategoryID)
        {
            int RestaurantID = int.Parse(Request.Cookies["RestaurantID"].Value);
            List<CostomerCategoryToJson> CostomerCategoryToJson = new List<CostomerCategoryToJson>();

            var first = reposCostomerCategory.getbyid(CustomizedCategoryID);//找出原本產品設定的客製化分類資料
            CostomerCategoryToJson data = new CostomerCategoryToJson();
            data.CustomizedCategoryID = first.fCustomizedCategoryID;//存入原本設定的客製化分類ID
            data.CustomizedCategoryName = first.fCustomizedCategoryName;//存入原本設定的客製化分類名稱
            CostomerCategoryToJson.Add(data);

            CostomerCategoryToJson secdata = new CostomerCategoryToJson();
            secdata.CustomizedCategoryName = "請選擇";//第二個選項
            CostomerCategoryToJson.Add(secdata);

            var tCostomerCategorydatas = reposCostomerCategory.GetAll().Where(c => c.fParentID == null && c.fRestaurantID == RestaurantID).ToList();
            foreach (var CategoryData in tCostomerCategorydatas)
            {
                data = new CostomerCategoryToJson();
                data.CustomizedCategoryID = CategoryData.fCustomizedCategoryID;
                data.CustomizedCategoryName = CategoryData.fCustomizedCategoryName;
                data.ParentID = CategoryData.fParentID;
                CostomerCategoryToJson.Add(data);
            }

            var json = Json(CostomerCategoryToJson.ToList(), JsonRequestBehavior.AllowGet);

            return json;
        }
        public ActionResult GetCostomizedParentSec(int CategoryID)
        {
            int RestaurantID = int.Parse(Request.Cookies["RestaurantID"].Value);
            var tCostomerCategorydatas = reposCostomerCategory.GetAll().Where(c => c.fParentID == CategoryID && c.fRestaurantID == RestaurantID).ToList();
            List<CostomerCategoryToJson> CostomerCategoryToJson = new List<CostomerCategoryToJson>();
            CostomerCategoryToJson data = new CostomerCategoryToJson();
            data.CustomizedCategoryName = "請選擇";
            CostomerCategoryToJson.Add(data);
            foreach (var CategoryData in tCostomerCategorydatas)
            {
                data = new CostomerCategoryToJson();
                data.CustomizedCategoryID = CategoryData.fCustomizedCategoryID;
                data.CustomizedCategoryName = CategoryData.fCustomizedCategoryName;
                data.ParentID = CategoryData.fParentID;
                CostomerCategoryToJson.Add(data);
            }

            var json = Json(CostomerCategoryToJson.ToList(), JsonRequestBehavior.AllowGet);

            return json;
        }
        public ActionResult GetCostomizedOption(int Categoryid)
        {
            var tCostomerOptions = reposOption.GetAll().Where(c => c.fCustomizedCategoryID == Categoryid).ToList();
            List<CostomerOptionToJSON> CostomerOptionToJSON = new List<CostomerOptionToJSON>();
          
            foreach (var OptionData in tCostomerOptions)
            {
                CostomerOptionToJSON data = new CostomerOptionToJSON();
                data.fCustomizedCategoryID = OptionData.fCustomizedCategoryID;
                data.fCustomizedOptionName = OptionData.fCustomizedOptionName;
                data.fCustomizedOptionID = OptionData.fCustomizedOptionID;

                CostomerOptionToJSON.Add(data);
            }

            var json = Json(CostomerOptionToJSON.ToList(), JsonRequestBehavior.AllowGet);

            return json;
        }
        //由下層找最上層的分類
        public ActionResult GetRestaurantCategorys()
        {
            int RestaurantID = int.Parse(Request.Cookies["RestaurantID"].Value);
            var RestaurantCategorys = reposRestaurantFood.GetAll().Where(c => c.fRestaurantID == RestaurantID).Select(c=>c.fCategoryID);
            var allRestaurantCategorys = RestaurantCategorys.Distinct().ToList();
            List<int?> allcategorys = new List<int?>();//找餐廳產品所有分類ParentID=null的分類
            foreach (int childcategory in allRestaurantCategorys)
            {
                int? findfathercategory = reposCategory.getbyid(childcategory).fParentID;
                if (findfathercategory == null)//找到的分類ID沒有子分類>>存進List
                {
                    allcategorys.Add(childcategory);
                }
                while (findfathercategory != null)//找到的分類ID有子分類>>繼續往上找
                {
                    int? fatherid = findfathercategory;
                    findfathercategory = reposCategory.getbyid(findfathercategory).fParentID;
                    if (findfathercategory == null)//找到的分類ID沒有子分類>>存進List
                    {
                        allcategorys.Add(fatherid);
                    }
                }
                
            }
            //做好由小到大後去除相同物件存進fathercategorylist
            allcategorys.Sort();
            IEnumerable<int?> fathercategorylist = allcategorys.Distinct();
            
            List<CategoryToJSON> CategoryToJSONs = new List<Models.CRUD.CategoryToJSON>();
            CategoryToJSON products = new Models.CRUD.CategoryToJSON();
            products.CategoryId = 0;
            products.CategoryName = "請選擇產品分類";
            CategoryToJSONs.Add(products);

            foreach (var fathercategoryid in fathercategorylist)//把所有父類別的名稱及類別ID轉Json回傳
            {
                products = new Models.CRUD.CategoryToJSON();
                products.CategoryId =(int)fathercategoryid;
                products.CategoryName = reposCategory.getbyid(fathercategoryid).fCategoryName;
                CategoryToJSONs.Add(products);
            }
           
            var json = Json(CategoryToJSONs.ToList(), JsonRequestBehavior.AllowGet);

            return json;
        }
       

        public ActionResult GetSECCategory(int categoryid)
        {
            var tCategorydatas = reposCategory.GetAll().Where(c => c.fParentID == categoryid).ToList();
            List<CategoryToJSON> CategoryToJSON = new List<CategoryToJSON>();
            
            foreach (var CategoryData in tCategorydatas)
            {
                CategoryToJSON data = new CategoryToJSON();
                data.CategoryId = CategoryData.fCategoryID;
                data.CategoryName = CategoryData.fCategoryName;
                data.CategoryParent = CategoryData.fParentID.ToString();
                CategoryToJSON.Add(data);
            }

            var json = Json(CategoryToJSON.ToList(), JsonRequestBehavior.AllowGet);

            return json;
        }
        public ActionResult GetProducts(int id, int page)
        {
            var products = reposRestaurantFood.GetAll().Where(c => c.fRestaurantID == id)
                                              .ToList().Skip((page - 1) * 2).Take(2);
            List<ProductToJson> ProductCJson = new List<ProductToJson>();

            foreach (var productsdata in products)
            {
                ProductToJson data = new ProductToJson();
                var photo = reposPhoto.GetAll().Where(c => c.fFoodID == productsdata.fFoodID).Select(c => c.fBytesImage);
                if (photo.Count() != 0)
                {
                    data.LargePhoto = photo.FirstOrDefault();
                }
                data.fFoodID = productsdata.fFoodID;
                data.fFoodName = productsdata.fFoodName;
                data.fPrice = productsdata.fPrice;
                data.fFoodDescription = productsdata.fFoodDescription;
                //data.fCookTime = productsdata.fCookTime;
                data.fModifiedDate = productsdata.fModifiedDate;
                ProductCJson.Add(data);
            }

            var json = Json(ProductCJson.ToList(), JsonRequestBehavior.AllowGet);

            return json;
        }
        public ActionResult DataCount(string isMeal,string Available)
        {
            int pagecount = 5;//一頁的筆數
            bool available = true;
            if (Available == "true") { available = true; }
            else { available = false; }
            bool meal =true;
            if (isMeal == "false") { meal = false; }
            else { meal = true; }
            int RestaurantID = int.Parse(Request.Cookies["RestaurantID"].Value);
            var count = db.tRestaurant_Foods.Where(c => c.fIsMeal == meal && c.fAvailable == available && c.fRestaurantID == RestaurantID);
            int counts = (count.Count() / pagecount)+1;
            var json = Json(counts, JsonRequestBehavior.AllowGet);

            return json;
        }
        public ActionResult GetPhoto(int Foodid)
        {
            //沒有圖片預設會給的圖IDtPhoto nophoto = reposPhoto.getbyid(101);學校:101 在家:8
            tPhoto nophoto = reposPhoto.getbyid(101);
            byte[] img = nophoto.fBytesImage;
            if (reposPhoto.GetAll().Where(c => c.fFoodID == Foodid).Select(c => c.fBytesImage).SingleOrDefault() != null)
            {
                 img = reposPhoto.GetAll().Where(c => c.fFoodID == Foodid).Select(c => c.fBytesImage).SingleOrDefault();
            }
            return File(img, "image/jpeg");
        }
        public ActionResult GetMemberPhoto(int id)
        {
            //沒有圖片預設會給的圖IDtPhoto nophoto = reposPhoto.getbyid(101);學校:101 在家:8
            //tPhoto nophoto = reposPhoto.getbyid(101);
            byte[] img = reposPhoto.GetAll().Where(c=>c.fMemberID==id).Select(c=>c.fBytesImage).SingleOrDefault();
            //if (reposPhoto.GetAll().Where(c => c.fMemberID == id).Select(c => c.fBytesImage).SingleOrDefault() != null)
            //{
            //    img = reposPhoto.GetAll().Where(c => c.fMemberID == id).Select(c => c.fBytesImage).SingleOrDefault();
            //}
            
            return File(img, "image/jpeg");
        }
        public ActionResult GetResataurantPhoto(int id)
        {
            byte[] img = reposPhoto.GetAll().Where(c => c.fRestaurantID == id).Select(c => c.fBytesImage).SingleOrDefault();
           
            return File(img, "image/jpeg");
        }
        public ActionResult GetFoodPhoto(int id)
        {
            byte[] img = reposPhoto.GetAll().Where(c => c.fFoodID == id).Select(c => c.fBytesImage).SingleOrDefault();
           
            return File(img, "image/jpeg");
        }
        public ActionResult GetNewPhoto(int id)
        {
            byte[] img = reposPhoto.GetAll().Where(c => c.fPhotoID == id).Select(c => c.fBytesImage).SingleOrDefault();

            return File(img, "image/jpeg");
        }
        //public ActionResult RestaurantBackImage()
        //{
        //    //沒有圖片預設會給的圖IDtPhoto nophoto = reposPhoto.getbyid(101);學校:101 在家:8
        //    tPhoto nophoto = reposPhoto.getbyid(134);
        //    byte[] img = nophoto.fBytesImage;

        //    return File(img, "image/jpeg");
        //}

    }
}