using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FoodDeliveryTemplete.Models;

namespace FoodDeliveryTemplete.Areas.yuwang.Controllers
{
    public class RestaurantOrderController : Controller
    {
        FoodDeliveryEntities dc = new FoodDeliveryEntities();
        // GET: yuwang/RestaurantOrder
        public ActionResult Index(int resID)
        {
            ViewBag.resID = resID;
            //var res_orders = dc.tRestaurantOrders.Where(o => o.fRestaurantID == resID&&o.fIsHistory==null).Select(o => o).ToList();
            return View();
        }

        public ActionResult Res_Orders(int resID,string orderby)
        {
                ViewBag.resID = resID;
                var res_orders = dc.tRestaurantOrders.Where(o => o.fRestaurantID == resID && (o.fIsHistory == null || o.fIsHistory == false)).Select(o => o).ToList();
                res_orders = res_orders.OrderBy(o => o.fOrderDatetime).ToList();
                //if(Request.Cookies["orders_auto_in"]==null)
                //{
                //    Response.Cookies["orders_auto_in"].Value = "1";
                //}
                //if(Request.Cookies["orders_auto_in"].Value=="1")
                //{
                //    Response.Cookies["orders_orderby"].Value = "0";
                //}
                //if (Request.Cookies["orders_auto_in"].Value == "1")
                //{
                //    Response.Cookies["orders_orderby"].Value = "0";
                //}

                if (orderby == "1")
                {
                    Response.Cookies["orders_orderby"].Value = "1";
                    var res_orders_1 = res_orders.Where(o => o.fOrderStatus == 1);
                    var res_orders_2 = res_orders.Where(o => o.fOrderStatus == 2);
                    var res_orders_3 = res_orders.Where(o => o.fOrderStatus == 3);
                    List<tRestaurantOrders> res = new List<tRestaurantOrders>();
                    foreach (var r in res_orders_1)
                    {
                        res.Add(r);
                    }
                    foreach (var r in res_orders_3)
                    {
                        res.Add(r);
                    }
                    foreach (var r in res_orders_2)
                    {
                        res.Add(r);
                    }
                    return PartialView(res);
                }

                if (orderby == "2")
                {
                    Response.Cookies["orders_orderby"].Value = "2";
                    var res_orders_1 = res_orders.Where(o => o.fOrderStatus == 1);
                    var res_orders_2 = res_orders.Where(o => o.fOrderStatus == 2);
                    var res_orders_3 = res_orders.Where(o => o.fOrderStatus == 3);
                    List<tRestaurantOrders> res = new List<tRestaurantOrders>();
                    foreach (var r in res_orders_2)
                    {
                        res.Add(r);
                    }
                    foreach (var r in res_orders_1)
                    {
                        res.Add(r);
                    }
                    foreach (var r in res_orders_3)
                    {
                        res.Add(r);
                    }
                    return PartialView(res);
                }

            if (orderby == "3")
            {
                Response.Cookies["orders_orderby"].Value = "3";
                var res_orders_1 = res_orders.Where(o => o.fOrderStatus == 1);
                var res_orders_2 = res_orders.Where(o => o.fOrderStatus == 2);
                var res_orders_3 = res_orders.Where(o => o.fOrderStatus == 3);
                List<tRestaurantOrders> res = new List<tRestaurantOrders>();
                foreach (var r in res_orders_3)
                {
                    res.Add(r);
                }
                foreach (var r in res_orders_2)
                {
                    res.Add(r);
                }
                foreach (var r in res_orders_1)
                {
                    res.Add(r);
                }
                return PartialView(res);
            }


            return PartialView(res_orders);
        }

        //public class order_details
        //{
        //  public  IEnumerable<tRestaurantOrder_Details> details {get;set;}
        //  public   IEnumerable<tFood_CustomizedOption> options { get; set; }

        //}
        public class order_detail
        {
            public tRestaurantOrder_Details detail { get; set; }
            public tFood_CustomizedOption option { get; set; }

        }
        public ActionResult Res_Orders_Details(int orderID)
        {
            var details = dc.tRestaurantOrder_Details.Where(d => d.fOrderID == orderID).ToList();
            List<order_detail> order_details = new List<order_detail>();

            foreach(var detail in details)
            {
                order_detail od = new order_detail();
                od.detail = detail;
                if(dc.tFood_CustomizedOption.Where(o => o.fCustomizedOptionID == detail.fCustomizedOptionID).Count()>0)
                {
                    od.option = dc.tFood_CustomizedOption.Where(o => o.fCustomizedOptionID == detail.fCustomizedOptionID).Select(o => o).Single();
                    order_details.Add(od);
                }
                else
                {
                    od.option = null;
                    order_details.Add(od);
                }
                
            }

            ViewBag.orderID = orderID;
            return PartialView(order_details);
        }
        public class res_order_data
        {
            //IEnumerable<tRestaurantOrders> res_orders { get; set; }
            //IEnumerable<tRestaurantOrder_Details> res_order_details { get; set; }
            //IEnumerable<> res_orders { get; set; }
        }


        public string PutOrderIntoHistory(int orderID)
        {
            var order_finished = dc.tRestaurantOrders.Find(orderID);
            order_finished.fIsHistory = true;
            order_finished.fOrderStatus = 4;
            dc.Entry(order_finished).State = System.Data.Entity.EntityState.Modified;
            dc.SaveChanges();

            return "已完成";
        }



        public ActionResult History_Res_Orders(int resID)
        {
            ViewBag.resID = resID;
            var res_orders = dc.tRestaurantOrders.Where(o => o.fRestaurantID == resID && o.fIsHistory == true).Select(o => o).ToList();
            return PartialView(res_orders);
        }



        public string EditOrderStatus(int orderID, int order_status)
        {
            var order_status_edit = dc.tRestaurantOrders.Find(orderID);
            order_status_edit.fOrderStatus = order_status;
            dc.Entry(order_status_edit).State = System.Data.Entity.EntityState.Modified;
            dc.SaveChanges();

            return "已修改";
        }




        public string SetDelayOrders(int orderID)
        {
            var delay_order = dc.tRestaurantOrders.Find(orderID);
            delay_order.fIsDelay = true;
            dc.Entry(delay_order).State = System.Data.Entity.EntityState.Modified;
            dc.SaveChanges();

            return "已寫入";
        }


        public string DeleteHistoryOrders(int orderID)
        {
            var delete_order_details = dc.tRestaurantOrder_Details.Where(o => o.fOrderID == orderID).ToList();
            foreach(var detail in delete_order_details)
            {
                dc.tRestaurantOrder_Details.Remove(detail);
                dc.SaveChanges();
            }

            var delete_order = dc.tRestaurantOrders.Find(orderID);
            dc.tRestaurantOrders.Remove(delete_order);
            dc.SaveChanges();
            return "已刪除";
        }
        //public ActionResult Order_Status_Edit(int orderID, int order_status)
        //{

        //}
        //public ActionResult History_Res_Orders_Details(int orderID)
        //{
        //    ViewBag.orderID = orderID;
        //    var res_order_details = dc.tRestaurantOrder_Details.Where(d => d.fOrderID == orderID).ToList();

        //    return PartialView(res_order_details);
        //}
    }
}