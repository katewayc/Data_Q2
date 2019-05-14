using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data_Q2.Models;
using Data_Q2.ViewModels;
using Newtonsoft.Json;

namespace Data_Q2.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            Random r = new Random();
            int i = r.Next(1, 4);
            string FileName = "data" + i + ".json";

            List<TicketData> ticketDataList = new List<TicketData>();

            using (StreamReader sr = new StreamReader(Server.MapPath(@"~/App_Data/" + FileName)))
            {
                string json = sr.ReadToEnd();
                List<TicketData> items = JsonConvert.DeserializeObject<List<TicketData>>(json);
                ticketDataList = items;
            }

            int ticketCount = 0;
            foreach (var item in ticketDataList)
            {
                ticketCount += item.Ticket;
            }

            DashboardViewModel model = new DashboardViewModel();
            List<DashboardTicketData> listForView = new List<DashboardTicketData>();

            var mappingResult = AutoMapper.Mapper.Map<List<TicketData>, List<DashboardTicketData>>(ticketDataList);

            listForView = mappingResult;

            model.FileName = FileName;
            model.AvgRspsTime = GetAvgRspsTime(GetAvgRspsMinute(listForView));
            model.TicketSum = ticketCount.ToString();
            model.TicketDataList = listForView;

            ViewBag.filename = model.FileName;
            ViewBag.avgrspstime = model.AvgRspsTime;
            ViewData["data"] = listForView;
            
            return View(model);  // return View(listForView);
        }

        private int GetAvgRspsMinute(List<DashboardTicketData> model)
        {
            int totalRspsMinute = 0;

            for (int i = 0; i < model.Count; i++)
            {
                totalRspsMinute += model[i].ResponseMinutes;
            }

            int avgRspsMinute = totalRspsMinute / model.Count;

            return avgRspsMinute;
        }

        private string GetAvgRspsTime(int avgResponseMinutes)
        {
            string avgRspsTime = "";

            if (avgResponseMinutes < 60) // 1小時內
            {
                avgRspsTime = avgResponseMinutes.ToString() + " minutes ";
            }
            else if (avgResponseMinutes >= 60 && avgResponseMinutes < 1440) // 1小時 - 1天內
            {
                avgRspsTime = GetAvgRspsHour(avgResponseMinutes) + " Hours "; ;
            }
            else  // 1天以上
            {
                avgRspsTime = GetAvgRspsDay(avgResponseMinutes) + " Days "; ;
            }

            return avgRspsTime;
        }

        private string GetAvgRspsDay(int avgResponseMinutes)
        {
            TimeSpan ts = TimeSpan.FromMinutes(avgResponseMinutes);
            string avgRspsDay = Math.Round(ts.TotalDays).ToString(); // 取整數
            return avgRspsDay;
        }

        private string GetAvgRspsHour(int avgResponseMinutes)
        {
            TimeSpan ts = TimeSpan.FromMinutes(avgResponseMinutes);
            string avgRspsHour = Math.Round(ts.TotalHours, 2).ToString();
            return avgRspsHour;
        }
    }
}