using ClinicaMedica2025.Datos;
using ClinicaMedica2025.Models;
using ClinicaMedica2025.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Xml.Linq;


namespace WebAppCorreo.Controllers
{
    public class InicioController : Controller
    {
        private Doctor doctor = null;
 
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string correo, string clave)
        {
            UsuarioDTO usuario = DBUsuario.Validar(correo, UtilidadServicio.ConvertirSHA256(clave));

            if (usuario != null)
            {
                if (!usuario.Confirmado)
                {
                    ViewBag.Mensaje = $"Falta confirmar su cuenta. Se le envió un correo a {correo}";
                }
                else if (usuario.Restablecer)
                {
                    ViewBag.Mensaje = $"Se ha solicitado restablecer su cuenta, favor revise su bandeja del correo {correo}";
                }
                else if (usuario.Rol.Trim().StartsWith("Med", StringComparison.OrdinalIgnoreCase))
                {
                    Session["cuil"] = usuario.Cuil;
                    Session["nombre"] = usuario.Nombre;
                    Session["apellido"] = usuario.Apellido;
                    Session["email"] = usuario.Email;
                    Session["matricula"] = usuario.MatriculaId;
                    Session["rol"] = usuario.Rol;
                    Session["clave"] = usuario.Clave;
                    return RedirectToAction("Index", "Doctor");
                }
                else if(usuario.Rol.Trim().StartsWith("Enfer", StringComparison.OrdinalIgnoreCase))
                {
                    Session["cuil"] = usuario.Cuil;
                    Session["nombre"] = usuario.Nombre;
                    Session["apellido"] = usuario.Apellido;
                    Session["email"] = usuario.Email;
                    Session["matricula"] = usuario.MatriculaId;
                    Session["rol"] = usuario.Rol;
                    Session["clave"] = usuario.Clave;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Mensaje = "Rol no reconocido";
                }
            }
            else
            {
                ViewBag.Mensaje = "No se encontraron coincidencias";
            }
            return View();
        }

        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(UsuarioDTO usuario)
        {
            Persona persona = null;

            if (usuario.Clave != usuario.ConfirmarClave)
            {
                ViewBag.Mensaje = "Las contraseñas no coinciden";
                return View();
            }
            
            if (DBUsuario.Obtener(usuario.Email) == null)
            {
                if (usuario.Rol == "Medico")
                {
                    usuario.Clave = UtilidadServicio.ConvertirSHA256(usuario.Clave);
                    usuario.Token = UtilidadServicio.GenerarToken();
                    usuario.Restablecer = false;
                    usuario.Confirmado = false;
                    bool respuesta = DBUsuario.Registrar(usuario);

                    if (respuesta)
                    {
                        string path = HttpContext.Server.MapPath("~/Plantilla/Confirmar.html");
                        string content = System.IO.File.ReadAllText(path);
                        string url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Headers["host"], "/Inicio/Confirmar?token=" + usuario.Token);

                        string htmlBody = string.Format(content, usuario.Nombre.ToUpper(), url);

                        CorreoDTO correoDTO = new CorreoDTO()
                        {
                            Para = usuario.Email,
                            Asunto = "Correo confirmación",
                            Contenido = htmlBody
                        };

                        bool enviado = CorreoServicio.Enviar(correoDTO);
                        ViewBag.Creado = true;
                        ViewBag.Mensaje = $"Su cuenta ha sido creada. Hemos enviado un mensaje al correo {usuario.Email} para confirmar su cuenta";
                    }
                    else
                    {
                        ViewBag.Mensaje = "No se pudo crear su cuenta";
                    }

                    return View();
                }
                else if (usuario.Rol == "Enfermero")
                {
                    usuario.Clave = UtilidadServicio.ConvertirSHA256(usuario.Clave);
                    usuario.Token = UtilidadServicio.GenerarToken();
                    usuario.Restablecer = false;
                    usuario.Confirmado = false;
                    bool respuesta = DBUsuario.Registrar(usuario);
                    if (respuesta)
                    {
                        string path = HttpContext.Server.MapPath("~/Plantilla/Confirmar.html");
                        string content = System.IO.File.ReadAllText(path);
                        string url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Headers["host"], "/Inicio/Confirmar?token=" + usuario.Token);

                        string htmlBody = string.Format(content, usuario.Nombre.ToUpper(), url);

                        CorreoDTO correoDTO = new CorreoDTO()
                        {
                            Para = usuario.Email,
                            Asunto = "Correo confirmación",
                            Contenido = htmlBody
                        };

                        bool enviado = CorreoServicio.Enviar(correoDTO);
                        ViewBag.Creado = true;
                        ViewBag.Mensaje = $"Su cuenta ha sido creada. Hemos enviado un mensaje al correo {usuario.Email} para confirmar su cuenta";
                    }
                    else
                    {
                        ViewBag.Mensaje = "No se pudo crear su cuenta";
                    }

                    return View();
                }
                else
                {
                    ViewBag.Mensaje = "Rol no válido";
                    return View();
                }
            }
            else
            {
                ViewBag.Mensaje = "Ya existe un usuario con ese correo";
                return View();
            }
        }

        public ActionResult Confirmar(string token)
        {
            ViewBag.Respuesta = DBUsuario.Confirmar(token);
            return View();
        }

        public ActionResult Restablecer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Restablecer(string correo)
        {
            UsuarioDTO usuario = DBUsuario.Obtener(correo);
            ViewBag.Correo = correo;
            if (usuario != null)
            {
                bool respuesta = DBUsuario.RestablecerActualizar(1, usuario.Clave, usuario.Token);

                if (respuesta)
                {
                    string path = HttpContext.Server.MapPath("~/Plantilla/Restablecer.html");
                    string content = System.IO.File.ReadAllText(path);
                    string url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Headers["host"], "/Inicio/Actualizar?token=" + usuario.Token);

                    string htmlBody = string.Format(content, usuario.Nombre, url);

                    CorreoDTO correoDTO = new CorreoDTO()
                    {
                        Para = correo,
                        Asunto = "Restablecer cuenta",
                        Contenido = htmlBody
                    };

                    bool enviado = CorreoServicio.Enviar(correoDTO);
                    ViewBag.Restablecido = true;
                }
                else
                {
                    ViewBag.Mensaje = "No se pudo restablecer la cuenta";
                }

            }
            else
            {
                ViewBag.Mensaje = "No se encontraron coincidencias con el correo";
            }

            return View();
        }

        public ActionResult Actualizar(string token)
        {
            ViewBag.Token = token;
            return View();
        }

        [HttpPost]
        public ActionResult Actualizar(string token, string clave, string confirmarClave)
        {
            ViewBag.Token = token;
            if (clave != confirmarClave)
            {
                ViewBag.Mensaje = "Las contraseñas no coinciden";
                return View();
            }

            bool respuesta = DBUsuario.RestablecerActualizar(0, UtilidadServicio.ConvertirSHA256(clave), token);

            if (respuesta)
                ViewBag.Restablecido = true;
            else
                ViewBag.Mensaje = "No se pudo actualizar";

            return View();
        }

        public ActionResult Logoff()
        {
            Session.Clear();
            return RedirectToAction("Login", "Inicio");
        }
    }
}