﻿using System.Web;
using System.Web.Mvc;

namespace NorthernLights.CRM
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
