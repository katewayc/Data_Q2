using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using data_statistic_Q2.Models;
using data_statistic_Q2.ViewModels;
using Newtonsoft.Json;

namespace data_statistic_Q2.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            Random r = new Random();
            int i = r.Next(1, 4);
            string FileName = "data" + i + ".json";

            List<TicketData> ticketDatas = new List<TicketData>();

            using (StreamReader sr = new StreamReader(Server.MapPath("~/App_Data/" + FileName)))
            {
                string json = sr.ReadToEnd();
                List<TicketData> items = JsonConvert.DeserializeObject<List<TicketData>>(json);
                ticketDatas = items;
            }

            DashboardViewModel dbvm = new DashboardViewModel();
            List<DashboardTicketData> dashboardTicketDatas = new List<DashboardTicketData>();

            var mapping_result = AutoMapper.Mapper.Map<List<TicketData>, List<DashboardTicketData>>(ticketDatas);

            dashboardTicketDatas = mapping_result;



            dbvm.FileName = FileName;
            dbvm.AvgRspsTime = Get_AvgRspsTime(Get_AvgRspsMinute(dashboardTicketDatas));
            dbvm.ticketDatas = dashboardTicketDatas;

            ViewBag.filename = dbvm.FileName;
            ViewBag.avgrspstime = dbvm.AvgRspsTime;
            ViewData["data"] = dashboardTicketDatas;
            return View(dashboardTicketDatas);
            //return View(dbvm);
        }

        private int Get_AvgRspsMinute(List<DashboardTicketData> dashboardTicketDatas)
        {
            int TotalRspsMinute = 0;

            for (int i = 0; i < dashboardTicketDatas.Count; i++)
            {
                TotalRspsMinute += dashboardTicketDatas[i].ResponseMinutes;
            }

            int AvgRspsMinute = TotalRspsMinute / dashboardTicketDatas.Count;

            return AvgRspsMinute;
        }

        private string Get_AvgRspsTime(int AvgResponseMinutes)
        {
            string AvgRspsTime = "";

            if (AvgResponseMinutes < 60) // 1小時內
            {
                AvgRspsTime = AvgResponseMinutes.ToString() + " minutes ";
            }
            else if (AvgResponseMinutes >= 60 && AvgResponseMinutes < 1440) // 1小時 - 1天內
            {
                AvgRspsTime = Get_AvgRspsHour(AvgResponseMinutes) + " Hours "; ;
            }
            else  // 1天以上
            {
                AvgRspsTime = Get_AvgRspsDay(AvgResponseMinutes) + " Days "; ;
            }

            return AvgRspsTime;
        }

        private string Get_AvgRspsDay(int AvgResponseMinutes)
        {
            TimeSpan ts = TimeSpan.FromMinutes(AvgResponseMinutes);
            string AvgRspsDay = Math.Round(ts.TotalDays).ToString(); // 取整數
            return AvgRspsDay;
        }

        private string Get_AvgRspsHour(int AvgResponseMinutes)
        {
            TimeSpan ts = TimeSpan.FromMinutes(AvgResponseMinutes);
            string AvgRspsHour = Math.Round(ts.TotalHours, 2).ToString();
            return AvgRspsHour;
        }
    }
}