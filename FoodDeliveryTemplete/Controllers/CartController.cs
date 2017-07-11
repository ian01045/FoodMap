using FoodDeliveryTemplete.Models;
using FoodDeliveryTemplete.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace FoodDeliveryTemplete.Controllers
{
    public class CartController : Controller
    {
        FoodDeliveryEntities db = new FoodDeliveryEntities();
        string strConn = ConfigurationManager.ConnectionStrings["FoodDeliveryConnection"].ConnectionString;

        string str_Ado_Conn = ConfigurationManager.ConnectionStrings["FoodDeliveryConnection"].ConnectionString;


        #region 餐廳餐點類別詳細資料
        public ActionResult Indexx(int id = 0, string IsMeal = "Y")
        {
            double distance = 1.0;
            //fRestaurantID = 10;//TODO待移除
            //distance = 2.5;//TODO待移除
            int fRestaurantID = Convert.ToInt32(Request.Cookies["RestaurantBlogID"].Value);

            distance = Convert.ToDouble(Request.Cookies["distance"].Value);
            if (distance < 0)
            {
                distance = 1;
            }
            int categoryid = id;
            CartListViewModel _CartListViewModel = new CartListViewModel();
            _CartListViewModel.IsMeal = IsMeal;

            //#region 處理Distance
            ////若distance沒傳值，且cookies[distance]也沒有值，導向搜尋餐廳頁面
            //if (distance == -1.0 && Request.Cookies["distance"] == null)
            //{
            //    return RedirectToAction("Index", "Blog");
            //}
            ////若distance有值，而cookies[distance]沒值，把distance值指派給cookies[distance]
            //else if (Request.Cookies["distance"] == null)
            //{
            //    Response.Cookies["distance"].Value = distance.ToString();
            //}
            ////若distance、cookies[distance]都有值，但值不同，以distance值為優先，再次把distance值指派給cookies[distance]
            //else if (Request.Cookies["distance"].Value != distance.ToString())
            //{
            //    Response.Cookies["distance"].Value = distance.ToString();
            //}

            //#endregion

            //#region 處理fRestaurantID
            ////先判斷fRestaurantID是否為0
            ////若不為0(第一次進來),存入Cookies中做備份供後面使用
            ////若為0(第二次以上進來),由Cookies中的fRestaurantID存回到fRestaurantID變數
            ////若為0且cookies沒有值,則導向搜尋餐廳頁面
            //if (fRestaurantID != 0)
            //{
            //    Response.Cookies["fRestaurantID"].Value = fRestaurantID.ToString();
            //    Response.Cookies["fRestaurantID"].Expires = DateTime.Now.AddDays(5);
            //}
            //else if (Request.Cookies["fRestaurantID"] == null)
            //{
            //    return RedirectToAction("Index", "Blog");
            //}
            //else
            //{
            //    fRestaurantID = Convert.ToInt32(Request.Cookies["fRestaurantID"].Value);
            //}
            //#endregion

            #region categoryid=0代表未進過此頁面 並指一個categoryid給它(只有單點會用到)
            if (categoryid == 0)
            {
                //第一次進來時把fCategoryID給categoryid
                //要把fRestaurantID下的fCategoryID撈出來 取第一筆值
                categoryid = (from c in db.tCategory
                              join d in db.tRestaurant_Foods
                              on c.fCategoryID equals d.fCategoryID
                              where d.fRestaurantID == fRestaurantID
                              group c by d.fCategoryID into categorygroup
                              select new FoodNameContentViewModel
                              {
                                  fCustomizedCategoryID = categorygroup.FirstOrDefault().fCategoryID,
                              }).ToList().FirstOrDefault().fCategoryID;
            }
            #endregion
            TimeSpan fDeliveryTime = TimeSpan.FromMinutes(distance * 2.7);
            #region 給餐點資料
            if (IsMeal.Equals("N", StringComparison.OrdinalIgnoreCase))
            {
                //單點餐點圖片及說明 
                #region 沒有客製化選項
                //var products = (from c in db.tRestaurant_Foods
                //                join d in db.tRestaurant_FoodCustomizedCategory
                //                on c.fCustomizedCategoryID equals d.fCustomizedCategoryID
                //                join e in db.tPhoto
                //                on c.fFoodID equals e.fFoodID
                //                where c.fRestaurantID == fRestaurantID && c.fCustomizedCategoryID == categoryid && c.fIsMeal == false
                //                select new FoodNameContentViewModel
                //                {
                //                    fFoodID = c.fFoodID,
                //                    fFoodImage = e.fPhotoPath,
                //                    fCustomizedCategoryName = d.fCustomizedCategoryName,
                //                    fFoodName = c.fFoodName,
                //                    fFoodDescription = c.fFoodDescription,
                //                    fPrice = c.fPrice,
                //                }).ToList();
                #endregion

                var products = (from c in db.tCategory
                                join d in db.tRestaurant_Foods
                                on c.fCategoryID equals d.fCategoryID
                                where d.fCategoryID == id && d.fRestaurantID == fRestaurantID
                                join e in db.tPhoto
                                on d.fFoodID equals e.fFoodID
                                #region left join
                                //join g in db.tFood_CustomizedOption
                                //on c.fCustomizedCategoryID equals g.fCustomizedCategoryID into cg
                                //from co in cg.DefaultIfEmpty()
                                #endregion
                                //目的=>CCID如果等於null時,塞0的值給CCID
                                //let創造一個變數 同等於在linq中使用 var XXX
                                //運算子的優先順序
                                //CCID=>該單點菜色客製化類別ID
                                let CCID = d.fCustomizedCategoryID == null || d.fCustomizedCategoryID <= 0 ? 0 : (int)d.fCustomizedCategoryID
                                select new FoodNameContentViewModel
                                {
                                    fFoodID = d.fFoodID,
                                    fFoodImage = e.fPhotoPath,
                                    fCategoryName = c.fCategoryName,
                                    fFoodName = d.fFoodName,
                                    fFoodDescription = d.fFoodDescription,
                                    fPrice = d.fPrice,
                                    fIsMeal = d.fIsMeal,
                                    fCookTime = d.fCookTime,
                                    fDeliveryTime = fDeliveryTime,
                                    //客製化選項
                                    CustomerOptions =
                                        (from x in db.tFood_CustomizedOption
                                         where CCID == x.fCustomizedCategoryID
                                         select new CustomerOption
                                         {
                                             fCustomizedOptionID = x.fCustomizedOptionID,
                                             fCustomizedCategoryID = x.fCustomizedCategoryID,
                                             fCustomizedOptionName = x.fCustomizedOptionName,
                                             //目前暫時沒用到
                                             fPrice = x.fPrice == null ? 0 : (decimal)x.fPrice
                                         }).ToList()
                                }).ToList();
                _CartListViewModel.FoodNameContentViewModels = products;
            }
            else
            {
                //套餐餐點圖片及說明
                #region 僅得到主餐(沒有子項目、客製化選項)
                /*
                var products = (from c in db.tRestaurant_Foods
                                join d in db.tRestaurant_FoodCustomizedCategory
                                on c.fCustomizedCategoryID equals d.fCustomizedCategoryID
                                join e in db.tPhoto
                                on c.fFoodID equals e.fFoodID
                                where c.fRestaurantID == fRestaurantID && c.fCustomizedCategoryID == categoryid && c.fIsMeal == true
                                select new FoodNameContentViewModel
                                {
                                    fFoodID = c.fFoodID,
                                    fFoodImage = e.fPhotoPath,
                                    fCustomizedCategoryName = d.fCustomizedCategoryName,
                                    fFoodName = c.fFoodName,
                                    fFoodDescription = c.fFoodDescription,
                                    fPrice = c.fPrice,
                                }).ToList();


                _CartListViewModel.FoodNameContentViewModels = products;
                _CartListViewModel.fRestaurantID = fRestaurantID;
                */
                #endregion
                var maindish = (from c in db.tRestaurant_Foods
                                join d in db.tCategory
                                on c.fCategoryID equals d.fCategoryID
                                join e in db.tPhoto
                                on c.fFoodID equals e.fFoodID
                                where c.fRestaurantID == fRestaurantID && c.fIsMeal == true
                                select new FoodNameContentViewModels_meal
                                {
                                    fCustomizedCategoryID = (int)(c.fCustomizedCategoryID == null ? 0 : c.fCustomizedCategoryID),
                                    fFoodName = c.fFoodName,
                                    fFoodID = c.fFoodID,
                                    fFoodImage = e.fPhotoPath,
                                    // fCustomizedCategoryName = d.fCustomizedCategoryName,
                                    fFoodDescription = c.fFoodDescription,
                                    fPrice = c.fPrice,
                                    fIsMeal = c.fIsMeal,
                                    fCookTime = c.fCookTime,
                                    fDeliveryTime = fDeliveryTime,
                                    //分類後的副餐項目
                                    SubDishCategories = (from x in
                                                             //撈出副餐項目
                                                            (from d in db.tRestaurantMeal_Detail
                                                             join e in db.tRestaurant_Foods
                                                             on d.fFoodID equals e.fFoodID
                                                             #region left join
                                                             //join g in db.tFood_CustomizedOption
                                                             //on e.fCustomizedCategoryID equals g.fCustomizedCategoryID into cg
                                                             //from co in cg.DefaultIfEmpty()
                                                             #endregion
                                                             where e.fRestaurantID == fRestaurantID && e.fIsMeal == false && c.fFoodID == d.fMealID
                                                             join f in db.tCategory
                                                             on e.fCategoryID equals f.fCategoryID
                                                             let CCID = e.fCustomizedCategoryID == null || e.fCustomizedCategoryID <= 0 ? 0 : (int)e.fCustomizedCategoryID
                                                             select new FoodNameContentViewModel
                                                             {
                                                                 fFoodID = e.fFoodID,
                                                                 fCategoryID = f.fCategoryID,
                                                                 fCategoryName = f.fCategoryName,
                                                                 fFoodName = e.fFoodName,
                                                                 CustomerOptions =
                                                                     (from x in db.tFood_CustomizedOption
                                                                      where CCID == x.fCustomizedCategoryID
                                                                      select new CustomerOption
                                                                      {
                                                                          fCustomizedOptionID = x.fCustomizedOptionID,
                                                                          fCustomizedCategoryID = x.fCustomizedCategoryID,
                                                                          fCustomizedOptionName = x.fCustomizedOptionName,
                                                                          fPrice = x.fPrice == null ? 0 : (decimal)x.fPrice
                                                                      }).ToList()
                                                             })//.Distinct()
                                                         group x by x.fCategoryName
                                                         ).ToList(),
                                }).ToList();
                _CartListViewModel.FoodNameContentViewModels_meal = maindish;
            }
            #endregion

            _CartListViewModel.fRestaurantID = fRestaurantID;

            return PartialView("Index", _CartListViewModel);
        }
        #endregion

        #region 導覽列
        public ActionResult Nav(int _fRestaurantID)
        {
            //導覽列的單點類別Btn(套餐Btn全撈，不取得類別)
            var Category = (from c in db.tCategory
                            join d in db.tRestaurant_Foods
                            on c.fCategoryID equals d.fCategoryID
                            where d.fRestaurantID == _fRestaurantID
                            group c by d.fCategoryID into categorygroup
                            select new FoodNameContentViewModel
                            {
                                fCategoryID = categorygroup.FirstOrDefault().fCategoryID,
                                fCategoryName = categorygroup.FirstOrDefault().fCategoryName,
                            }).ToList();

            return PartialView(Category);
        }
        #endregion

        #region 從檔案中取圖片
        public ActionResult GetImageFile(string fileName)
        {
            return File("~/fFoodImage/" + fileName, "image/jpeg");
        }
        #endregion

        #region 回傳二進位圖片

        public ActionResult GetImageByte(int id)
        {
            tPhoto photo = db.tPhoto.Where(p => p.fFoodID == id).Single();
            byte[] img = photo.fBytesImage;
            return File(img, "image/jpeg");
        }

        #endregion

        #region 加入購物車
        /// <summary>
        /// 選購單點
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AddToCart(int fFoodID = 0, int id = 1, int CCID = 0)
        {
            if (fFoodID != 0)
            {
                //string fFoodID = Request.QueryString["fFoodID"];               
                #region 判斷身分
                string _fCartID;
                if (Session["fCartID"] == null)
                {
                    if (Request.Cookies["MemberID"] != null)
                    {
                        Session["fCartID"] = Request.Cookies["MemberID"].Value;
                    }
                    else
                    {
                        Session["fCartID"] = Guid.NewGuid().ToString();
                    }
                }
                _fCartID = Session["fCartID"].ToString();
                #endregion
                #region 預存程序加資料
                using (SqlConnection conn = new SqlConnection(str_Ado_Conn))
                {
                    using (SqlCommand cmd = new SqlCommand("AddCartToItem", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@fCartID", _fCartID.ToString());
                        cmd.Parameters.AddWithValue("@fQuantity", id);
                        cmd.Parameters.AddWithValue("@fFoodID", fFoodID);
                        cmd.Parameters.AddWithValue("@CCID", CCID);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                #endregion
                //fCartID為CartList的傳入參數
                return Json(new { _fCartID }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("Indexx");//轉到食物首頁
            }
        }
        /// <summary>
        /// 選購套餐-複雜模型繫結
        /// </summary>
        /// <param name="fFoodID">此套餐的fFoodID</param>
        /// <param name="fFoodIDs">被選購的子項目(Radio Button選的)</param>
        /// <param name="quantity">套餐數量</param>
        /// <returns></returns>
        //public ActionResult AddToCartMeal(int fFoodID, List<int> fFoodIDs, int quantity)//簡單模型繫結
        public ActionResult AddToCartMeal(mealOrder mo)
        {

            //tshoppingcart新增欄位 fMealRecordID
            //欄位=-1 代表單點 (同步修正單點的storedprocessed)
            //欄位<0 代表為子項目
            //欄位=0 代表套餐
            if (mo != null)
            {
                #region 判斷身分
                string _fCartID;
                if (Session["fCartID"] == null)
                {
                    if (Request.Cookies["MemberID"] != null)
                    {
                        Session["fCartID"] = Request.Cookies["MemberID"].Value;
                    }
                    else
                    {
                        Session["fCartID"] = Guid.NewGuid().ToString();
                    }
                }
                _fCartID = Session["fCartID"].ToString();
                #endregion
                #region 預存程序加資料
                using (SqlConnection conn = new SqlConnection(str_Ado_Conn))
                {
                    //主項目儲存使用預存程序AddCartToItem_Meal
                    int RecordID;
                    using (SqlCommand cmd = new SqlCommand("AddCartToItem_Meal", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@fCartID", _fCartID.ToString());
                        cmd.Parameters.AddWithValue("@fQuantity", mo.quantity);
                        cmd.Parameters.AddWithValue("@fFoodID", mo.fFoodID);
                        cmd.Parameters.Add("@RecordID", SqlDbType.Int);
                        cmd.Parameters["@RecordID"].Direction = ParameterDirection.Output;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        RecordID = Convert.ToInt32(cmd.Parameters["@RecordID"].Value);
                        conn.Close();
                    }
                    //子項目儲存使用預存程序AddCartToItem_Sub
                    foreach (var subFoodID in mo.fFoods)
                    {
                        using (SqlCommand cmd = new SqlCommand("AddCartToItem_Sub", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@fCartID", _fCartID.ToString());
                            cmd.Parameters.AddWithValue("@fMealRecordID", RecordID);
                            cmd.Parameters.AddWithValue("@subFoodID", subFoodID.fFoodID);
                            cmd.Parameters.AddWithValue("@CCID", subFoodID.CCID);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                }
                #endregion
                //fCartID為CartList的傳入參數
                return RedirectToAction("CartList", new { fCartID = _fCartID });
            }
            else
            {
                return RedirectToAction("Index");//轉到食物首頁
            }
        }
        #endregion

        #region 顯示購物車頁面

        //購物車頁面
        public ActionResult CartList(string fCartID)
        {
            #region 判斷身分
            if (fCartID == null)
            {
                if (Session["fCartID"] == null)
                {
                    if (Request.Cookies["MemberID"] == null)
                    {
                        return RedirectToAction("Index");//轉到食物首頁
                    }
                    else
                    {
                        Session["fCartID"] = Request.Cookies["MemberID"].Value;
                        fCartID = Request.Cookies["MemberID"].Value;
                    }
                }
                else
                {
                    fCartID = Session["fCartID"].ToString();
                }
            }
            #endregion

            CartListViewModel _CartListViewModel = new CartListViewModel();

            //顯示選購內容
            var query = (from c in db.tShoppingCart
                         join d in db.tRestaurant_Foods
                         on c.fFoodID equals d.fFoodID
                         let _fCustomizedOptionID_1 = c.fCustomizedOptionID == null ? 0 : (int)c.fCustomizedOptionID
                         //用left join
                         join r in db.tFood_CustomizedOption
                         on c.fCustomizedOptionID equals r.fCustomizedOptionID into x
                         from j in x.DefaultIfEmpty()
                             //配合自訂資料結構 預存程序 0:套餐 -1:單點 1以上:副餐
                         where c.fCartID == fCartID && c.fMealRecordID <= 0
                         select new FoodNameShowViewModel
                         {
                             fRecordID = c.fRecordID,
                             fFoodName = d.fFoodName,
                             fPrice = d.fPrice,
                             fQuantity = (int)c.fQuantity,
                             fIsMeal = d.fIsMeal,
                             fCustomizedCategoryName = j.fCustomizedOptionName,
                             fCustomizedCategoryID = _fCustomizedOptionID_1,
                             //副餐
                             SubDishCategories = (from n in db.tShoppingCart
                                                  join m in db.tRestaurant_Foods
                                                  on n.fFoodID equals m.fFoodID
                                                  let _fCustomizedOptionID_2 = n.fCustomizedOptionID == null ? 0 : (int)n.fCustomizedOptionID
                                                  //left join
                                                  join p in db.tFood_CustomizedOption
                                                  on n.fCustomizedOptionID equals p.fCustomizedOptionID into b
                                                  from y in b.DefaultIfEmpty()
                                                  where n.fCartID == fCartID && n.fMealRecordID == c.fRecordID && c.fMealRecordID == 0
                                                  select new FoodNameContentViewModel
                                                  {
                                                      fRecordID = n.fRecordID,
                                                      fFoodName = m.fFoodName,
                                                      fCustomizedCategoryName = y.fCustomizedOptionName,
                                                      fCustomizedCategoryID = _fCustomizedOptionID_2
                                                  }
                                                  ).ToList()
                         }).ToList();
            _CartListViewModel.FoodNameShowViewModels = query;
            _CartListViewModel.fRestaurantID = Convert.ToInt32(Request.Cookies["fRestaurantID"].Value);

            #region 若是會員，自動帶出會員基本資料
            if (Request.Cookies["MemberID"] != null)
            {
                int cMID = Convert.ToInt32(Request.Cookies["MemberID"].Value);
                //由fCartID找到fMemeberName fAddress fPhone
                var check = (from c in db.tMemeber
                             where c.fMemberID == cMID
                             select new
                             {
                                 c.fMemberID,
                                 c.fMemeberName,
                                 c.fAddress,
                                 c.fPhone,
                                 c.fEmail
                             }).FirstOrDefault();
                _CartListViewModel.fMemberID = check.fMemberID;
                _CartListViewModel.fMemeberName = check.fMemeberName;
                _CartListViewModel.fAddress = check.fAddress;
                _CartListViewModel.fPhone = check.fPhone;
                _CartListViewModel.fEmail = check.fEmail;
            }
            #endregion

            #region 付款方式
            _CartListViewModel.PaymentWays = (from c in db.tPaymentWay
                                              select new PaymentWayViewModel
                                              {
                                                  fPaymentwayID = c.fPaymentwayID,
                                                  fPaymentway = c.fPaymentway
                                              }).ToList();
            #endregion

            return View(_CartListViewModel);//轉到購物車頁面
        }
        #endregion

        #region 點下電子錢包所跑的ajax取得電子錢包資訊
        public ActionResult GetWalletInfo()
        {
            decimal Wallet_Surplus = -1;

            #region 若是會員，自動帶出會員基本資料
            if (Request.Cookies["MemberID"] != null)
            {
                int cMID = Convert.ToInt32(Request.Cookies["MemberID"].Value);
                //取得使用者的餘額
                var q = (from c in db.tMember_Wallat
                         where c.fMemberID == cMID
                         select new
                         {
                             fWallet_Surplus = c.fWallat_Surplus
                         }).ToList();
                //效果同上(LinQ擴充方法)
                //var qq = db.tMember_Wallat.Where(m => m.fMemberID == cMID).Select(m => new { fWallet_Surplus = m.fWallat_Surplus });
                if (q.Count > 0)
                {
                    Wallet_Surplus = q.LastOrDefault().fWallet_Surplus;
                }
            }
            #endregion

            return Json(new { fWallet_Surplus = Wallet_Surplus }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 加入購物車後刪除
        public ActionResult Delete(int fRecordID = 0)
        {
            if (Session["fCartID"] != null || fRecordID == 0)
            {
                string _fCartID = Session["fCartID"].ToString();

                db.tShoppingCart.Remove(db.tShoppingCart.Find(fRecordID));
                db.SaveChanges();

                return RedirectToAction("CartList", new { fCartID = _fCartID });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        #endregion                         

        #region 加入購物車後更新數量
        //使用複雜模型繫結
        public ActionResult Update(List<ShoppingCartForm> ALaCaret, List<ShoppingCartForm> SetMenu)
        {
            //讀到修改的數量
            //產品編號
            //fCartID->Session["fCartID"]

            //是集合使用foreach來取
            if (Session["fCartID"] != null)
            {
                string _fCartID = Session["fCartID"].ToString();
                if (ALaCaret != null)
                    foreach (var _aLaCaret in ALaCaret)
                    {
                        //Update
                        tShoppingCart UpdateShoppingCart = db.tShoppingCart.Find(_aLaCaret.fRecordID);
                        UpdateShoppingCart.fQuantity = _aLaCaret.fQuantity;
                        db.Entry(UpdateShoppingCart).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                if (SetMenu != null)
                    foreach (var _setMenu in SetMenu)
                    {
                        //Update
                        tShoppingCart UpdateShoppingCart = db.tShoppingCart.Find(_setMenu.fRecordID);
                        UpdateShoppingCart.fQuantity = _setMenu.fQuantity;
                        db.Entry(UpdateShoppingCart).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                return RedirectToAction("CartList", new { fCartID = _fCartID });
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        #endregion

        #region  購物車結帳
        //結帳使用複雜模型繫結
        public ActionResult Checkout(CartListViewModel form)
        {
            //判斷使用者是否登入
            //檢查cookie中有沒有登入帳號
            //並取得distance
            double distance = 0.0;
            if (Request.Cookies["MemberID"] == null || !double.TryParse(Request.Cookies["distance"].Value, out distance))
            {
                if (distance < 0)
                {
                    distance = 1;
                }
                #region 還沒有登入
                Session["fCartID"] = null;
                Session.Abandon();
                return RedirectToAction("LoginView", "Account");
                #endregion
            }
            else
            {
                form.fMemberID = int.Parse(Request.Cookies["MemberID"].Value);
                #region 登入成功後 資料庫處理(把購物車轉訂單)
                //將ShoppingCart的資料轉到Order及OrderDetails的資料中
                //將ShoppingCart的資料刪除
                //取得OrderID及使用者的地址和電話顯示給使用者看
                using (SqlConnection conn = new SqlConnection(str_Ado_Conn))
                {
                    using (SqlCommand cmd = new SqlCommand("OrderCreate", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@fCartID", Session["fCartID"].ToString());
                        cmd.Parameters.AddWithValue("@fMemberID", Request.Cookies["MemberID"].Value.ToString());
                        cmd.Parameters.AddWithValue("@fOrderDatetime", DateTime.Now);
                        cmd.Parameters.AddWithValue("@fDeliveryAddress", form.fAddress);
                        cmd.Parameters.AddWithValue("@fRestaurantID", form.fRestaurantID);
                        cmd.Parameters.AddWithValue("@fDeliveryTime", TimeSpan.FromMinutes(distance * 2.7));//乘2.7倍(單位：分鐘)
                        cmd.Parameters.AddWithValue("@fPayment", form.tPaymentWay);
                        cmd.Parameters.Add("@fOrderID", SqlDbType.Int);
                        cmd.Parameters["@fOrderID"].Direction = ParameterDirection.Output;
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        form.fOrderID = Convert.ToInt32(cmd.Parameters["@fOrderID"].Value);
                        conn.Close();
                    }
                }
                #endregion

                #region 若結帳方式為"電子錢包"，進行資料庫處理
                //判斷fPaymentwayID == 3，才進行下面動作
                form.newSurplus = -1;//先設為-1
                if (form.tPaymentWay == 3)
                {
                    //取得原來的餘額
                    var obj = db.tMember_Wallat.Where(m => m.fMemberID == form.fMemberID).Select(m => new { fID = m.fID }).ToList();
                    if (obj.Count > 0)
                    {
                        int fID = obj.LastOrDefault().fID;//取得最新筆數(DB最後一筆)的ID
                        Repository<tMember_Wallat> db_tMember_Wallat = new Repository<tMember_Wallat>();
                        tMember_Wallat tMember_Wallat = db_tMember_Wallat.GetById(fID);
                        form.newSurplus = tMember_Wallat.fWallat_Surplus - form.SumTotal;//新餘額 : 原餘額 - form.SumTotal
                        tMember_Wallat.fWallat_Surplus = form.newSurplus;
                        tMember_Wallat.fWithdraw = form.SumTotal;//提領金額 : form.SumTotal
                        tMember_Wallat.fDeposit = null;//存款 : null
                        tMember_Wallat.ModifiedDate = DateTime.Now;
                        db_tMember_Wallat.Insert(tMember_Wallat);//新增此筆資料

                        Repository<tMemeber> db_tMember = new Repository<tMemeber>();
                        tMemeber tMemeber = db_tMember.GetById(form.fMemberID);
                        tMemeber.fWalletTotalMoney = form.newSurplus.ToString();
                        db_tMember.Update(tMemeber);
                    }

                }
                #endregion

                #region 撈出結帳明細
                #region 舊寫法，合併到靜態類別AmyMemberOrder.GetOrderDetail()中
                /*
                var query = (from c in db.tRestaurantOrder_Details
                             join d in db.tRestaurant_Foods
                             on c.fFoodID equals d.fFoodID
                             let _fCustomizedOptionID_1 = c.fCustomizedOptionID == null ? 0 : (int)c.fCustomizedOptionID
                             join r in db.tFood_CustomizedOption
                             on c.fCustomizedOptionID equals r.fCustomizedOptionID into x
                             from j in x.DefaultIfEmpty()
                             where c.fOrderID == form.fOrderID && (c.fRemark.Substring(0, 1) == "0" || c.fRemark == "-1")
                             select new FoodNameShowViewModel
                             {
                                 fFoodName = d.fFoodName,
                                 fPrice = d.fPrice,
                                 fQuantity = (int)c.fQuantity,
                                 fIsMeal = d.fIsMeal,
                                 fCustomizedCategoryName = j.fCustomizedOptionName,
                                 fCustomizedCategoryID = _fCustomizedOptionID_1,
                                 SubDishCategories = (from n in db.tRestaurantOrder_Details
                                                      join m in db.tRestaurant_Foods
                                                      on n.fFoodID equals m.fFoodID
                                                      let _fCustomizedOptionID_2 = n.fCustomizedOptionID == null ? 0 : (int)n.fCustomizedOptionID
                                                      join p in db.tFood_CustomizedOption
                                                      on n.fCustomizedOptionID equals p.fCustomizedOptionID into b
                                                      from y in b.DefaultIfEmpty()
                                                      where n.fOrderID == form.fOrderID && n.fRemark == c.fRemark.Substring(1, c.fRemark.Length - 1) && c.fRemark.Substring(0, 1) == "0"
                                                      select new FoodNameContentViewModel
                                                      {
                                                          fFoodName = m.fFoodName,
                                                          fCustomizedCategoryName = y.fCustomizedOptionName,
                                                          fCustomizedCategoryID = _fCustomizedOptionID_2
                                                      }
                                                      ).ToList()
                             }).ToList();
                */
                //form.FoodNameShowViewModels = query;
                #endregion
                form.FoodNameShowViewModels = AmyMemberOrder.GetOrderDetail(db, form.fMemberID, form.fOrderID);
                #endregion

                return View("Order", form);
            }
        }
        #endregion

        #region 寄信
        public ActionResult SendOrderList(CartListViewModel form)
        {
            //int cartid = Convert.ToInt32(Session["fCartID"]);
            //int cartid = form.fMemberID;
            int cartid = Convert.ToInt32(Request.Cookies["MemberID"].Value);
            var Email = db.tMemeber.Where(c => c.fMemberID == cartid).Select(c => c.fEmail).FirstOrDefault();
            string Title = "食宿列車: 會員您好，訂單通知";
            string Body = "<h3 style=" + "color:lightskyblue" + ">親愛的" + form.fMemeberName + " 先生/小姐您好:</h3></br>";
            Body += "</br></br> 已收到您的訂單，謝謝您";
            Body += "</br></br> 食宿列車團隊敬上";
            SendEMail(Email, Title, Body);
            int strfId = Convert.ToInt32(Request.Cookies["RestaurantBlogID"].Value);
            return RedirectToAction("RestaurantBlog", "Blog", new { Area = "Member", id = strfId });
        }

        private void SendEMail(string emailid, string subject, string body)
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

            client.Send(msg);
        }
        #endregion

        public ActionResult PartialCartList(string fCartID)
        {
            var query = (from c in db.tShoppingCart
                         join d in db.tRestaurant_Foods
                         on c.fFoodID equals d.fFoodID                         
                         where c.fCartID == fCartID                        
                         select new FoodNameContentViewModel
                         {
                             fFoodName = d.fFoodName,
                             fPrice = d.fPrice,
                             fQuantity = (int)c.fQuantity,
                             fRecordID = c.fRecordID,
                             fRestaurantID = d.fRestaurantID,                          
                             

                         }).ToList();
            return PartialView(query);

            //return RedirectToAction("CartList", new { fCartID = fCartID });
        }

        public ActionResult PartialDelete(int fRecordID = 0)
        {
            if (Session["fCartID"] != null || fRecordID == 0)
            {
                string _fCartID = Session["fCartID"].ToString();
                db.tShoppingCart.Remove(db.tShoppingCart.Find(fRecordID));
                db.SaveChanges();
                _fCartID = Session["fCartID"].ToString();
                return Json(new { _fCartID }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string fail = "唉呀! 好像有些地方出錯了";
                return Json(new { fail }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult PartialModifiedQuan(int fRecordID = 0, string quantity = "1")
        {
            if (Session["fCartID"] != null || fRecordID == 0)
            {
                string _fCartID = Session["fCartID"].ToString();
                tShoppingCart shopping = db.tShoppingCart.Find(fRecordID);
                shopping.fQuantity = Convert.ToInt32(quantity);
                db.Entry(shopping).State = EntityState.Modified;
                db.SaveChanges();
                _fCartID = Session["fCartID"].ToString();
                return Json(new { _fCartID }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string fail = "唉呀! 好像有些地方出錯了";
                return Json(new { fail }, JsonRequestBehavior.AllowGet);
            }
        }

      
      

        
    }
}