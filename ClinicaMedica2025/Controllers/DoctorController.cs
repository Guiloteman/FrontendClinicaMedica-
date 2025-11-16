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
    public class DoctorController : Controller
    {
        // GET: Doctor
        public ActionResult Index()
        {
            var cuil = Session["cuil"];
            var nombre = Session["nombre"];
            var apellido = Session["apellido"];
            var email = Session["email"];
            var matricula = Session["matricula"];
            var rol = Session["rol"];
            ViewBag.Mensaje = $"¡Bienvenida/o Doctor/a {apellido}, {nombre}!";
            return View();
        }

        public ActionResult AtencionAPacienteEnCola()
        {
            

            ViewBag.Mensaje = $"¡Atender a pacientes en cola de espera!";
            return View();
        }

        public ActionResult RegistrarAtencion()
        {
            ViewBag.Mensaje = $"¡Registrar atención!";
            return View();
        }
    }
}