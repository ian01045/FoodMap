using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDeliveryTemplete.Areas.yuwang.Models;
using FoodDeliveryTemplete.Models;
using PagedList;
using PagedList.Mvc;
using FoodDeliveryTemplete.ViewModel;

namespace FoodDeliveryTemplete.Areas.yuwang.Controllers
{
    public class ManageController : Controller
    {
        public FoodDeliveryEntities dc = new FoodDeliveryEntities();

        // GET: yuwang/Manage
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChartReport()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetRegisteredMembers()
        {
             ReportData data = new ReportData();

            List<int> consumers_per_month = new List<int>();
            for (int i = 1; i<=4; i++)
            {
                var consumers = dc.tMemeber.Where(m => m.fJoinDate.Month == i&&m.fRoleID==3).Count();
                consumers_per_month.Add(consumers);
            }

            List<int> restaurarestaurants_per_month = new List<int>();
            for (int i = 1; i <=4; i++)
            {
                var restaurants = dc.tMemeber.Where(m => m.fJoinDate.Month == i && m.fRoleID == 2).Count();
                restaurarestaurants_per_month.Add(restaurants);
            }


            data.consumers_per_month = consumers_per_month;
            data.restaurants_per_month = restaurarestaurants_per_month;

            //用MVC內建的JsonResult丟回資料。
            return Json(data);
        }

        [HttpPost]
        public ActionResult GetResSalesAmount_ByCitys_PerMonth()
        {

            //dc.tRestaurantOrders.Where(r => r.tRestaurant.tMemeber.tMember_Area.tMember_City.fCityID == 1).Where(o => o.fOrderDatetime.Month == ).Count() > 0

            ReportData data = new ReportData();

            List<decimal> res_sales_amount_city1 = new List<decimal>();
            for (int i = 1; i <= 4; i++)
            {  
                if(dc.tRestaurantOrders.Where(r => r.tRestaurant.tMemeber.tMember_Area.tMember_City.fCityID == 1).Where(o => o.fOrderDatetime.Month == i).Count()>0)
                {
                    //decimal city_res_sales_per_month = dc.tRestaurantOrders.Where(r => r.tRestaurant.tMemeber.tMember_Area.tMember_City.fCityID == 1).Where(o => o.fOrderDatetime.Month == i).Select(o =>o.tRestaurantOrder_Details;
                    decimal city_res_sales_per_month = dc.tRestaurantOrder_Details.Where(d => d.tRestaurantOrders.tRestaurant.tMemeber.tMember_Area.tMember_City.fCityID == 1 && d.tRestaurantOrders.fOrderDatetime.Month == i).Select(d => d.fPrice).Sum();
                    res_sales_amount_city1.Add(city_res_sales_per_month);
                }
                else
                {
                    res_sales_amount_city1.Add(0);
                }
            }

            List<decimal> res_sales_amount_city2 = new List<decimal>();
            for (int i = 1; i <= 4; i++)
            {
                if(dc.tRestaurantOrders.Where(r => r.tRestaurant.tMemeber.tMember_Area.tMember_City.fCityID == 4).Where(o => o.fOrderDatetime.Month == i).Count() > 0)
                {
                    decimal city_res_sales_per_month = dc.tRestaurantOrder_Details.Where(d => d.tRestaurantOrders.tRestaurant.tMemeber.tMember_Area.tMember_City.fCityID == 4 && d.tRestaurantOrders.fOrderDatetime.Month == i).Select(d => d.fPrice).Sum();
                    res_sales_amount_city2.Add(city_res_sales_per_month);
                }
                else
                {
                    res_sales_amount_city2.Add(0);
                }
            }

            List<decimal> res_sales_amount_city3 = new List<decimal>();
            for (int i = 1; i <= 4; i++)
            {
                if(dc.tRestaurantOrders.Where(r => r.tRestaurant.tMemeber.tMember_Area.tMember_City.fCityID == 5).Where(o => o.fOrderDatetime.Month == i).Count()>0)
                {
                    decimal city_res_sales_per_month = dc.tRestaurantOrder_Details.Where(d => d.tRestaurantOrders.tRestaurant.tMemeber.tMember_Area.tMember_City.fCityID == 5 && d.tRestaurantOrders.fOrderDatetime.Month == i).Select(d => d.fPrice).Sum();
                    res_sales_amount_city3.Add(city_res_sales_per_month);
                }
                else
                {
                    res_sales_amount_city3.Add(0);
                }
                
            }

            List<decimal> res_sales_amount_city4 = new List<decimal>();
            for (int i = 1; i <= 4; i++)
            {
                if(dc.tRestaurantOrders.Where(r => r.tRestaurant.tMemeber.tMember_Area.tMember_City.fCityID == 12).Where(o => o.fOrderDatetime.Month == i).Count()>0)
                {
                    decimal city_res_sales_per_month = dc.tRestaurantOrder_Details.Where(d => d.tRestaurantOrders.tRestaurant.tMemeber.tMember_Area.tMember_City.fCityID == 12 && d.tRestaurantOrders.fOrderDatetime.Month == i).Select(d => d.fPrice).Sum();
                    res_sales_amount_city4.Add(city_res_sales_per_month);
                }
                else
                {
                    res_sales_amount_city4.Add(0);
                }
                
            }

            List<string> city_names = new List<string>();
            string c1_name = dc.tMember_City.Where(c => c.fCityID == 1).Select(c => c.fCityName).Single();
            string c2_name = dc.tMember_City.Where(c => c.fCityID == 4).Select(c => c.fCityName).Single();
            string c3_name = dc.tMember_City.Where(c => c.fCityID == 5).Select(c => c.fCityName).Single();
            string c4_name = dc.tMember_City.Where(c => c.fCityID == 12).Select(c => c.fCityName).Single();
            city_names.Add(c1_name);city_names.Add(c2_name);city_names.Add(c3_name);city_names.Add(c4_name);

            data.res_sales_amount_city1 = res_sales_amount_city1;
            data.res_sales_amount_city2 = res_sales_amount_city2;
            data.res_sales_amount_city3 = res_sales_amount_city3;
            data.res_sales_amount_city4 = res_sales_amount_city4;

            Name_Data nd1 = new Name_Data();  nd1.name = c1_name; nd1.data = res_sales_amount_city1;
            Name_Data nd2 = new Name_Data();  nd2.name = c2_name; nd2.data = res_sales_amount_city2;
            Name_Data nd3 = new Name_Data();  nd3.name = c3_name; nd3.data = res_sales_amount_city3;
            Name_Data nd4 = new Name_Data();  nd4.name = c4_name; nd4.data = res_sales_amount_city4;

            List<Name_Data> names_datas = new List<Name_Data>();
            names_datas.Add(nd1);
            names_datas.Add(nd2);
            names_datas.Add(nd3);
            names_datas.Add(nd4);


            //用MVC內建的JsonResult丟回資料。
            return Json(names_datas);
        }


        public class Name_Data
        {
           public string name { get; set; }
            public IEnumerable<decimal> data{ get; set; }
        }

        //int under30 = 0;
        //int up30under40 = 0;
        //int up40under50 = 0;
        //int up50under60 = 0;
        //int up60 = 0;
        //年齡分組方法
        private age_group_ordercounts Age_Group(int memeberid,decimal price)
        {
            var birth = dc.tMemeber.Where(m => m.fMemberID == memeberid).Select(m => m.fBirth).Single();
            var age = (DateTime.Now.AddYears(-birth.Year)).Year;
            age_group_ordercounts age_group = new age_group_ordercounts();

            if (age <= 30)
            {
                age_group.name = "年輕客群(30歲以下)";
                age_group.y =price ;
                return age_group;
            }

            else if (age <= 40)
            {
                age_group.name = "中壯年客群(30~40歲)";
                age_group.y = price;
                return age_group;
            }
            else if (age <= 50)
            {
                age_group.name = "中年客群(40~50歲)";
                age_group.y = price;
                return age_group;
            }
            else if (age <= 60)
            {
                age_group.name = "中老年客群(50~60歲)";
                age_group.y = price;
                return age_group;
            }
            else
            {
                age_group.name = "老年客群";
                age_group.y = price;
                return age_group;
            }
        }
        public class age_group_ordercounts
        {
            public string name { get; set; }
            public decimal y { get; set; }
        }
        [HttpPost]
        public ActionResult GetPortion_ByAgeGroup_PerCity(int cityID)
        {
            var orders_in_city = dc.tRestaurantOrder_Details.Where(d => d.tRestaurantOrders.tRestaurant.tMemeber.tMember_Area.tMember_City.fCityID == cityID).Select(d => d).ToList();


            var orders_in_city_groupby_age = from d in orders_in_city
                                             group d by Age_Group(d.tRestaurantOrders.fMemberID, d.fPrice) into g
                                             select new
                                             {
                                                 Group_Name = g.Key.name,
                                                 Group_Orders_Sales_Amount = g.Key.y

                                             };

            decimal sales_under30 = orders_in_city_groupby_age.ToList().Where(c => c.Group_Name == "年輕客群(30歲以下)").Select(c => c.Group_Orders_Sales_Amount).Sum();
            decimal sales_up30under40 = orders_in_city_groupby_age.ToList().Where(c => c.Group_Name == "中壯年客群(30~40歲)").Select(c => c.Group_Orders_Sales_Amount).Sum();
            decimal sales_up40under50 = orders_in_city_groupby_age.ToList().Where(c => c.Group_Name == "中年客群(40~50歲)").Select(c => c.Group_Orders_Sales_Amount).Sum();
            decimal sales_up50under60 = orders_in_city_groupby_age.ToList().Where(c => c.Group_Name == "中老年客群(50~60歲)").Select(c => c.Group_Orders_Sales_Amount).Sum();
            decimal sales_up60 = orders_in_city_groupby_age.ToList().Where(c => c.Group_Name == "老年客群").Select(c => c.Group_Orders_Sales_Amount).Sum();

            List<age_group_ordercounts> age_group_sales_amount = new List<age_group_ordercounts>();
            age_group_ordercounts under30 = new age_group_ordercounts();
            age_group_ordercounts up30under40 = new age_group_ordercounts();
            age_group_ordercounts up40under50 = new age_group_ordercounts();
            age_group_ordercounts up50under60 = new age_group_ordercounts();
            age_group_ordercounts up60 = new age_group_ordercounts();
            under30.name = "年輕客群(30歲以下)";under30.y = sales_under30;
            up30under40.name = "中壯年客群(30~40歲)";up30under40.y = sales_up30under40;
            up40under50.name = "中年客群(40~50歲)";up40under50.y = sales_up40under50;
            up50under60.name = "中老年客群(50~60歲)";up50under60.y = sales_up50under60;
            up60.name = "老年客群";up60.y = sales_up60;

            age_group_sales_amount.Add(under30);
            age_group_sales_amount.Add(up30under40);
            age_group_sales_amount.Add(up40under50);
            age_group_sales_amount.Add(up50under60);
            age_group_sales_amount.Add(up60);

            return Json(age_group_sales_amount);
        }

        [HttpPost]
        public ActionResult GetResAreas(int cityID)
        {

            //List<string> restaurants_citys = new List<string>();
            //List<double> restaurants_citys_portion = new List<double>();

            var areas = dc.tMember_Area.Where(a => a.tMemeber.Where(m => m.fAreaID == a.fAreaID).Where(m => m.fRoleID == 2).Count() > 0).Where(a => a.fCityID == cityID).Select(a => a);

            var res_areas_taipeicity = from a in areas.ToList()
                                       join m in dc.tMemeber.Where(m => m.fRoleID == 2).ToList()
                                       on a.fAreaID equals m.fAreaID
                                       group m by m.fAreaID into Group
                                       select new
                                       {
                                           //AreaID = Group.Key,
                                           name = dc.tMember_Area.Where(a => a.fAreaID == Group.Key).Select(a => a.fAreaName).FirstOrDefault(),
                                           y = Group.Count()
                                       };

            List<ResAreasReport> areas_out = new List<ResAreasReport>();
            foreach (var area in res_areas_taipeicity)
            {
                ResAreasReport rap = new ResAreasReport();
                rap.name = area.name;
                rap.y = area.y;
                areas_out.Add(rap);
            }
            //double areas_counts_total = 0;
            //foreach (var area_counts in res_areas_taipeicity)
            //{
            //    areas_counts_total += area_counts.y;
            //}

            //List<string> areas_name = new List<string>();
            //List<double> areas_portion = new List<double>();

            //foreach (var area in res_areas_taipeicity)
            //{
            //    areas_name.Add(area.AreaName);
            //    areas_portion.Add(area.AreaRes_Member_Counts / areas_counts_total);
            //}

            //data.restaurants_areas_taipeicity = restaurants_citys;
            //data.restaurants_areas_taipeicity_portion = restaurants_citys_portion;

            return Json(areas_out);
        }



        [HttpPost]
        public ActionResult GetMemAreas(int cityID)
        {

            var areas = dc.tMember_Area.Where(a => a.tMemeber.Where(m => m.fAreaID == a.fAreaID).Where(m => m.fRoleID == 3).Count() > 0).Where(a => a.fCityID == cityID).Select(a => a);

            var res_areas_taipeicity = from a in areas.ToList()
                                       join m in dc.tMemeber.Where(m => m.fRoleID == 3).ToList()
                                       on a.fAreaID equals m.fAreaID
                                       group m by m.fAreaID into Group
                                       select new
                                       {
                                           //AreaID = Group.Key,
                                           name = dc.tMember_Area.Where(a => a.fAreaID == Group.Key).Select(a => a.fAreaName).FirstOrDefault(),
                                           y = Group.Count()
                                       };

            List<ResAreasReport> areas_out = new List<ResAreasReport>();
            foreach (var area in res_areas_taipeicity)
            {
                ResAreasReport rap = new ResAreasReport();
                rap.name = area.name;
                rap.y = area.y;
                areas_out.Add(rap);
            }
            return Json(areas_out);
        }


        //載入管理員報表
           public ActionResult GetReport(int id=0)
        {
            ViewBag.citys = dc.tMember_City.ToList();
            
            return PartialView();
        }


        public ActionResult Mem_Mange(int? id, int? page)
        {

            Member_Mange mem_mange = new Member_Mange();
            mem_mange.mem = dc.tMemeber.Where(m => m.fRoleID == 3).ToList();

            List<tMemeber> members = new List<tMemeber>();

            var sortby="";
            if(id == 1)
            {
                sortby = Request.Cookies["MemberNumber"].Value;
            }
            if (id == 2)
            {
                sortby = Request.Cookies["MemberName"].Value;
            }
            if (id == 3)
            {
                sortby = Request.Cookies["Gender"].Value;
            }
            if (id == 4)
            {
                sortby = Request.Cookies["CityName"].Value;
            }
            if (id == 5)
            {
                sortby = Request.Cookies["Birth"].Value;
            }
            if (id == 6)
            {
                sortby = Request.Cookies["JoinDate"].Value;
            }
            if (id == 7)
            {
                sortby = Request.Cookies["WalletTotalMoney"].Value;
            }
            if (id == 8)
            {
                sortby = Request.Cookies["SortBy"].Value;
            }
            Response.Cookies["MemberNumber"].Value = string.IsNullOrEmpty(sortby) ? "MemberNumber desc" : "";
            Response.Cookies["MemberName"].Value = sortby == "MemberName" ? "MemberName desc" : "MemberName";
            Response.Cookies["Gender"].Value = sortby == "Gender" ? "Gender desc" : "Gender";
            Response.Cookies["CityName"].Value = sortby == "CityName" ? "CityName desc" : "CityName";
            Response.Cookies["Birth"].Value = sortby == "Birth" ? "Birth desc" : "Birth";
            Response.Cookies["JoinDate"].Value = sortby == "JoinDate" ? "JoinDate desc" : "JoinDate";
            Response.Cookies["WalletTotalMoney"].Value = sortby == "WalletTotalMoney" ? "WalletTotalMoney desc" : "WalletTotalMoney";
            switch (sortby)
            {
                case "MemberNumber desc":
                    Response.Cookies["SortBy"].Value = "MemberNumber desc";
                    members = mem_mange.mem.OrderByDescending(m =>m.fMemberID).ToList();
                    break;

                case "MemberName":
                    Response.Cookies["SortBy"].Value = "MemberName";
                    members = mem_mange.mem.OrderBy(m => m.fMemeberName).ToList();
                    break;
                case "MemberName desc":
                    Response.Cookies["SortBy"].Value = "MemberName desc";
                    members = mem_mange.mem.OrderByDescending(m => m.fMemeberName).ToList();
                    break;
                case "Gender":
                    Response.Cookies["SortBy"].Value = "Gender";
                    members = mem_mange.mem.OrderBy(m => m.fGender).ToList();
                    break;
                case "Gender desc":
                    Response.Cookies["SortBy"].Value = "Gender desc";
                    members = mem_mange.mem.OrderByDescending(m => m.fGender).ToList();
                    break;
                case "CityName":
                    Response.Cookies["SortBy"].Value = "CityName";
                    members = mem_mange.mem.OrderBy(m => m.tMember_Area.tMember_City.fCityName).ToList();
                    break;
                case "CityName desc":
                    Response.Cookies["SortBy"].Value = "CityName desc" ;
                    members = mem_mange.mem.OrderByDescending(m => m.tMember_Area.tMember_City.fCityName).ToList();
                    break;
                case "Birth":
                    Response.Cookies["SortBy"].Value = "Birth";
                    members = mem_mange.mem.OrderBy(m => m.fBirth).ToList();
                    break;
                case "Birth desc":
                    Response.Cookies["SortBy"].Value = "Birth desc";
                    members = mem_mange.mem.OrderByDescending(m => m.fBirth).ToList();
                    break;
                case "JoinDate":
                    Response.Cookies["SortBy"].Value = "JoinDate";
                    members = mem_mange.mem.OrderBy(m => m.fJoinDate).ToList();
                    break;
                case "JoinDate desc":
                    Response.Cookies["SortBy"].Value = "JoinDate desc";
                    members = mem_mange.mem.OrderByDescending(m => m.fJoinDate).ToList();
                    break;
                case "WalletTotalMoney":
                    Response.Cookies["SortBy"].Value = "WalletTotalMoney";
                    members = mem_mange.mem.ToList().OrderBy(m => Convert.ToInt32(m.fWalletTotalMoney)).ToList();
                    break;
                case "WalletTotalMoney desc":
                    Response.Cookies["SortBy"].Value = "WalletTotalMoney desc";
                    members = mem_mange.mem.ToList().OrderByDescending(m => Convert.ToInt32(m.fWalletTotalMoney)).ToList();
                    break;
                default:
                    Response.Cookies["SortBy"].Value = "MemberNumber";
                    members = mem_mange.mem.OrderBy(m => m.fMemberID).ToList();
                    break;
            }


            mem_mange.mem = members.ToList();
            mem_mange.mem_area = dc.tMember_Area.ToList();
            mem_mange.mem_city = dc.tMember_City.ToList();


            return PartialView(mem_mange.mem.ToList().ToPagedList(page??1,5));
        }





        public ActionResult Res_Mange(int? id, int? page)
        {
            Member_Mange res_mange = new Member_Mange();
            //res_mange.mem = dc.tMemeber.Where(m => m.fRoleID == 2).ToList();
            res_mange.res = dc.tRestaurant.ToList();
            //List<tMemeber> members = new List<tMemeber>();
            ViewBag.id = id;
            List<tRestaurant> restaurants = new List<tRestaurant>();
            var sortby = "";
            if (id == 1)
            {
                sortby = Request.Cookies["ResMemberNumber"].Value;
                
            }
            if (id == 2)
            {
                sortby = Request.Cookies["ResMemberName"].Value;
                
            }
            if (id == 3)
            {
                sortby = Request.Cookies["ResCityName"].Value;
             
            }
            if (id == 4)
            {
                sortby = Request.Cookies["ResBirth"].Value;
          
            }
            if (id == 5)
            {
                sortby = Request.Cookies["ResJoinDate"].Value;
               
            }

            if(id==6)
            {
                sortby = Request.Cookies["SortBy"].Value;
            }
            Response.Cookies["ResMemberNumber"].Value = string.IsNullOrEmpty(sortby) ? "MemberNumber desc" : "";
            Response.Cookies["ResMemberName"].Value = sortby == "MemberName" ? "MemberName desc" : "MemberName";
            Response.Cookies["ResCityName"].Value = sortby == "CityName" ? "CityName desc" : "CityName";
            Response.Cookies["ResBirth"].Value = sortby == "Birth" ? "Birth desc" : "Birth";
            Response.Cookies["ResJoinDate"].Value = sortby == "JoinDate" ? "JoinDate desc" : "JoinDate";

            switch (sortby)
            {
                case "MemberNumber desc":
                    Response.Cookies["SortBy"].Value = "MemberNumber desc";
                    restaurants = res_mange.res.OrderByDescending(r=>r.tMemeber.fMemberID).ToList();
                    break;
                case "MemberName":
                    Response.Cookies["SortBy"].Value = "MemberName";
                    restaurants = res_mange.res.OrderBy(r => r.tMemeber.fMemeberName).ToList();
                    break;
                case "MemberName desc":
                    Response.Cookies["SortBy"].Value = "MemberName desc";
                    restaurants = res_mange.res.OrderByDescending(r => r.tMemeber.fMemeberName).ToList();
                    break;
                case "CityName":
                    Response.Cookies["SortBy"].Value = "CityName";
                    restaurants = res_mange.res.OrderBy(r => r.tMemeber.tMember_Area.tMember_City.fCityName).ToList();
                    break;
                case "CityName desc":
                    Response.Cookies["SortBy"].Value = "CityName desc";
                    restaurants = res_mange.res.OrderByDescending(r => r.tMemeber.tMember_Area.tMember_City.fCityName).ToList();
                    break;
                case "Birth":
                    Response.Cookies["SortBy"].Value = "Birth";
                    restaurants = res_mange.res.OrderBy(r => r.tMemeber.fBirth).ToList();
                    break;
                case "Birth desc":
                    Response.Cookies["SortBy"].Value = "Birth desc";
                    restaurants = res_mange.res.OrderByDescending(r => r.tMemeber.fBirth).ToList();
                    break;
                case "JoinDate":
                    Response.Cookies["SortBy"].Value = "JoinDate";
                    restaurants = res_mange.res.OrderBy(r => r.tMemeber.fJoinDate).ToList();
                    break;
                case "JoinDate desc":
                    Response.Cookies["SortBy"].Value = "JoinDate desc";
                    restaurants = res_mange.res.OrderByDescending(r => r.tMemeber.fJoinDate).ToList();
                    break;
                default:
                    Response.Cookies["SortBy"].Value = "MemberNumber";
                    restaurants = res_mange.res.OrderBy(r => r.tMemeber.fMemberID).ToList();
                    break;
            }

            //res_mange.mem = members.ToList() ;
            res_mange.res = restaurants;
            res_mange.mem_area = dc.tMember_Area.ToList();
            res_mange.mem_city = dc.tMember_City.ToList();

            return PartialView(res_mange.res.ToList().ToPagedList(page ??1,7));

        }


        public class Member_Mange
        {
            public  IEnumerable<tMemeber> mem { get; set; }
            public IEnumerable<tRestaurant> res { get; set; }
            public IEnumerable<tMember_Area> mem_area { get; set; }
            public IEnumerable<tMember_City> mem_city { get; set; }
        }

        [HttpPost]
        public string Remove_Member(int id)
        {
            var delete_member = dc.tMemeber.Find(id);
            dc.tMemeber.Remove(delete_member);
            dc.SaveChanges();

            //return RedirectToAction("Mem_Mange", "Manage",new { Area="yuwang",id=id});
            return "刪除完成";
        }



        //public class member_all_data
        //{
        //    public tMemeber member { get; set; }
            
        //}

        //public ActionResult Member_Pesonal_Edit(int memberID)
        //{
        //    FoodDeliveryEntities dc = new FoodDeliveryEntities();
        //    //member_all_data mem_all_data = new member_all_data();
        //    tMemeber mem= dc.tMemeber.Where(m => m.fMemberID == memberID).Select(m => m).Single();
        //    //mem_all_data.member = mem;

        //    return PartialView(mem);
        //}

            public class city_area
        {
            public IEnumerable<tMember_City>  citys { get; set; }
            public IEnumerable<tMember_Area>  areas { get; set; }
        }
       
 

        public ActionResult City_Area_Manage(int ? page)
        {
            FoodDeliveryEntities dc = new FoodDeliveryEntities();
            //city_area ca = new city_area();
            //ca.areas = dc.tMember_Area;
            //ca.citys = dc.tMember_City;

            return PartialView(dc.tMember_City.ToList().ToPagedList(page??1,5));
        }

        public string City_Edit(string city_name, int cityID)
        {
            tMember_City cityedit = dc.tMember_City.Find(cityID);
            cityedit.fCityName = city_name;
            dc.Entry(cityedit).State = System.Data.Entity.EntityState.Modified;
            dc.SaveChanges();
            return "已修改";
        }

        public string Area_Edit(string area_name, int areaID)
        {
            tMember_Area areaedit = dc.tMember_Area.Find(areaID);
            areaedit.fAreaName = area_name;
            dc.Entry(areaedit).State = System.Data.Entity.EntityState.Modified;
            dc.SaveChanges();
            return "已修改";
        }
        public string City_Delete( int cityID)
        {
            tMember_City citydelete = dc.tMember_City.Find(cityID);

            dc.tMember_City.Remove(citydelete);
            dc.SaveChanges();
            return "已刪除";
        }

        public string Area_Delete(int areaID)
        {
            tMember_Area areadelete = dc.tMember_Area.Find(areaID);

            dc.tMember_Area.Remove(areadelete);
            dc.SaveChanges();
            return "已刪除";
        }
        public string City_Add(string city_name)
        {
            tMember_City cityadd = new tMember_City();
            cityadd.fCityName = city_name;
            dc.tMember_City.Add(cityadd);
            dc.SaveChanges();
            return "已新增";
        }
        public string Area_Add(string area_name, int cityID)
        {
            tMember_Area areaadd = new tMember_Area();
            areaadd.fAreaName = area_name;
            areaadd.fCityID = cityID;
            dc.tMember_Area.Add(areaadd);
            dc.SaveChanges();
            return "已新增";
        }
        public int LastPageInt()
        {
            int counts = dc.tMember_City.ToList().Count;
            int lastpage = (counts / 5);
            return lastpage;
        }
        [HttpPost]
        public ActionResult Find_Areas(int cityID)
        {
            var areas= dc.tMember_Area.Where(a => a.fCityID == cityID);
            return Json(areas.ToList());
        }






        //----------會員的修改表單
        [HttpGet]
        public ActionResult MemberModifyForm(int id)
        {

            MemberPhotoViewModel info = new MemberPhotoViewModel();

            tPhoto photo = dc.tPhoto.Where(p => p.fMemberID == id).Single();

            info.fMemberID = photo.tMemeber.fMemberID;
            info.fMemeberName = photo.tMemeber.fMemeberName;
            info.fEmail = photo.tMemeber.fEmail;
            info.fPassword = photo.tMemeber.fPassword;
            info.fGender = photo.tMemeber.fGender;
            info.fBirth = photo.tMemeber.fBirth;
            info.fPhone = photo.tMemeber.fPhone;
            info.fTel = photo.tMemeber.fTel;
            info.fAddress = photo.tMemeber.fAddress;
            info.fAreaID = photo.tMemeber.tMember_Area.fAreaID;
            info.fCityID = photo.tMemeber.tMember_Area.tMember_City.fCityID;

            //讓下拉式選單有東西
            ViewBag.datas = dc.tMember_City.ToList();
            ViewBag.area = dc.tMember_Area.Where(c => c.fCityID == photo.tMemeber.tMember_Area.tMember_City.fCityID).ToList();

            return View(info);
        }
        [HttpPost]
        public ActionResult MemberModifyForm(int id, MemberPhotoViewModel info, HttpPostedFileBase memberPhoto)
        {
            tMemeber update = dc.tMemeber.Find(id);

            update.fMemeberName = info.fMemeberName;
            update.fEmail = info.fEmail;
            update.fPassword = info.fPassword;
            update.fPasswordConfirm = info.fPassword;
            update.fGender = info.fGender;
            update.fPhone = info.fPhone;
            update.fTel = info.fTel;
            update.fAreaID = info.fAreaID;
            update.fAddress = info.fAddress;

            dc.Entry(update).State = System.Data.Entity.EntityState.Modified;
            dc.SaveChanges();

            var photoID = dc.tPhoto.Where(p => p.fMemberID == id).Select(p => p.fPhotoID).Single();
            tPhoto changePhoto = dc.tPhoto.Find(photoID);
            //將上傳的圖轉成二進位

            if (memberPhoto != null)
            {
                var imgSize = memberPhoto.ContentLength;
                byte[] imgByte = new byte[imgSize];
                memberPhoto.InputStream.Read(imgByte, 0, imgSize);

                changePhoto.fBytesImage = imgByte;
                changePhoto.fDateTime = DateTime.Today;

                dc.Entry(changePhoto).State = System.Data.Entity.EntityState.Modified;
                dc.SaveChanges();
            }

            return RedirectToAction("Index", "Home", new { Area = "administrator" });
        }



    }
}