using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace VuDaiDuong_8627_DoAnCoSo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Application["visitor"] = 0;
        }
      protected void Session_Start()
        {
            Application.Lock();
            Application["visitor"] = (int)Application["visitor"] + 1;
            Application.UnLock();
        }
    }
}
