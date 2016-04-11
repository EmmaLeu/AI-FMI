using BL;
using AI.Code.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace AI.Code.Base
{
    public class BaseController : Controller
    {
        private Services services;

        public Services Services
        {
            get
            {
                return services ?? (services = new Services());
            }
        }

        public new SessionWrapper Session = SessionWrapper.Instance;

        public bool CheckIfAuthorized(int id)
        {
            return (Session.CurrentUser != null && Session.CurrentUser.UserID != id);


        }

        protected override void Dispose(bool disposing)
        {
            if (services != null)
            {
                services.Dispose();
            }

            base.Dispose(disposing);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if ((System.Web.HttpContext.Current.Session["CurrentUser"] == null || !filterContext.HttpContext.Request.IsAuthenticated)
                && filterContext.ActionDescriptor.GetCustomAttributes(true).All(a => a.GetType() != typeof(AllowAnonymousAttribute))
                && filterContext.Controller.GetType().CustomAttributes.All(a => a.AttributeType != typeof(AllowAnonymousAttribute)))
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new JsonResult
                    {
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                        Data = new
                        {
                            Status = "nok",
                            Reason = "Session expired"
                        }
                    };
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                    {
                        {"Controller", "Account"},
                        {"Action", "Login"},
                        {"ReturnUrl", filterContext.HttpContext.Request.RawUrl}
                    });
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}