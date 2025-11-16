using ClinicaMedica2025.Datos;
using ClinicaMedica2025.Models;
using ClinicaMedica2025.Permisos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClinicaMedica2025.Controllers
{
    [ValidarSesionAtributo]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var cuil = Session["cuil"];
            var nombre = Session["nombre"];
            var apellido = Session["apellido"];
            var email = Session["email"];
            var matricula = Session["matricula"];
            var rol = Session["rol"];

            ViewBag.Mensaje = $"¡Bienvenida/o Enfermera/o {apellido}, {nombre}!";
            return View();
        }

        public ActionResult CargarPacientesAlSistema()
        {
            ViewBag.Mensaje = $"Cargar pacientes al sistema";

            return View();
        }

        public ActionResult CargarPacientesEnColaDeEspera()
        {
            ViewBag.Mensaje = $"Cargar pacientes en cola de espera";
            return View();
        }
    }
}