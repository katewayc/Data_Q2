using AutoMapper;
using data_statistic_Q2.Models;
using data_statistic_Q2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace data_statistic_Q2.App_Start
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            AutoMapper.Mapper.Initialize(cfg => {
                cfg.CreateMap<TicketData, DashboardTicketData>();

            });
        }
    }
}