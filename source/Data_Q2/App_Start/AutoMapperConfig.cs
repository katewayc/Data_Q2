using AutoMapper;
using Data_Q2.Models;
using Data_Q2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Data_Q2.App_Start
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