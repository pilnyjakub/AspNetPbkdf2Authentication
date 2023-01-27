using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetPbkdf2Authentication.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.SessionUsername = HttpContext.Session.Keys.Contains("SessionUsername") ? HttpContext.Session.GetString("SessionUsername") : null;
            base.OnActionExecuting(context);
        }
    }
}
