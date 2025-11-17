using ClinicaMedica2025.Datos;
using ClinicaMedica2025.Models;
using ClinicaMedica2025.Permisos;
using Org.BouncyCastle.Asn1.Esf;
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
        private static ColaAtencion colaGlobal = new ColaAtencion();
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

        // --- MANEJA LA VISUALIZACIÓN DE LA PÁGINA (GET) ---
        public ActionResult CargarPacientesEnColaDeEspera(Paciente paciente, Ingreso ingreso, Enfermera enfermera)
        {
            var nivelesDisponibles = Nivel.ObtenerNiveles();

            var nivelSeleccionado = nivelesDisponibles.Values
                .FirstOrDefault(n => n.NivelId == ingreso.NivelIdRecibido);

            if (nivelSeleccionado == null)
            {
                ViewBag.Mensaje = "❌ Error: El nivel de triage seleccionado no es válido.";
                return View();
            }

            ingreso.nivel = nivelSeleccionado;

            // 1. Asignar Paciente (ya bindeado por el Model Binder)
            // 2. Asignar fecha
            ingreso.fechaIngreso = DateTime.Now;

            // 3. Inicializar Enfermera si es nulo y asignar nombre
            if (ingreso.enfermera == null)
            {
                ingreso.enfermera = new Enfermera();
            }
            ingreso.enfermera.Nombre = enfermera.Nombre; // Asignamos el nombre de la enfermera bindeado

            // 4. Asignar el Paciente bindeado al Ingreso
            ingreso.paciente = paciente;

            // 5. Encolar en la cola estática
            colaGlobal.Encolar(ingreso);

            // 6. Preparar datos para la vista
            ViewBag.Mensaje = $"✅ Paciente {paciente.Nombre} {paciente.Apellido} registrado y encolado correctamente.";
            ViewBag.Creado = true;

            // Usamos la cola global para mostrar su estado actual
            ViewBag.ColaAtencion = colaGlobal.ToString(); // Asume que ColaAtencion tiene un buen ToString o se itera en la vista

            return View();
        }
    }
}