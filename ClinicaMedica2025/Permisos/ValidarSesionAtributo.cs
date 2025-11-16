using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClinicaMedica2025.Permisos
{
    public class ValidarSesionAtributo : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sesion = filterContext.HttpContext.Session;
            if (sesion["email"] == null && sesion["clave"] == null)
            {
                filterContext.Result = new RedirectResult("~/Inicio/Login");
            }
            base.OnActionExecuting(filterContext);
        }
    } 
}