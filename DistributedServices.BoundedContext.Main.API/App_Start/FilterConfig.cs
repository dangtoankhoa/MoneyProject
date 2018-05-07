using System.Web;
using System.Web.Mvc;

namespace DistributedServices.BoundedContext.Main.API
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
