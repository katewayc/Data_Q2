using data_statistic_Q2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace data_statistic_Q2.ViewModels
{
    public class DashboardViewModel
    {
        public string FileName { get; set; }
        public string AvgRspsTime { get; set; }
        public IEnumerable<DashboardTicketData> ticketDatas { get; set; }
    }
    public class DashboardTicketData : TicketData
    {

    }
}