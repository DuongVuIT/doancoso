﻿using System.Web;
using System.Web.Mvc;

namespace VuDaiDuong_8627_DoAnCoSo
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
