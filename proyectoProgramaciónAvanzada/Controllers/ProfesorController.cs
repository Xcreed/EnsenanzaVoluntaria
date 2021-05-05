using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using proyectoProgramaciónAvanzada.Models;

namespace proyectoProgramaciónAvanzada.Controllers
{
    public class ProfesorController : Controller
    {
        private EnseñanzaVoluntariaEntities db = new EnseñanzaVoluntariaEntities();

        // GET: Profesor
        [Authorize(Roles = "admin, profesor, estudiante")]
        public ActionResult Inicio()
        {
            if (Roles.IsUserInRole(User.Identity.Name, "estudiante"))
            {
                return RedirectToAction("Index", "Estudiante");
            }
            else if (Roles.IsUserInRole(User.Identity.Name, "profesor"))
            {
                return RedirectToAction("RegistrarEstudiante", "Profesor");
            }
            else if (Roles.IsUserInRole(User.Identity.Name, "admin"))
            {
                return RedirectToAction("RegistrarEstudiante", "Profesor");
            }else
            {
                return RedirectToAction("Login", "Account");
            }

        }

        [Authorize(Roles = "admin, profesor")]
        public ActionResult ActualizarEstudiante(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            ViewBag.institucion = db.INSTITUCION.ToList();

            var estudiante = db.USUARIOS.Find(id);

            if (estudiante == null)
            {
                return HttpNotFound();
            }

            return View(estudiante);
        }

        [HttpPost]
        [Authorize(Roles = "admin, profesor")]
        public ActionResult ActualizarEstudiante(USUARIOS estudiante)
        {
            USUARIOS estudianteActualizado = db.USUARIOS.Find(estudiante.ID_USUARIO);

            if (!ModelState.IsValid)
            {
                ViewBag.institucion = db.INSTITUCION.ToList();
                return View(estudiante); // returns the view with errors
            }

            ViewBag.institucion = db.INSTITUCION.ToList();

            estudianteActualizado.APELLIDO1 = estudiante.APELLIDO1;
            estudianteActualizado.APELLIDO2 = estudiante.APELLIDO2;
            estudianteActualizado.NOMBRE = estudiante.NOMBRE;
            estudianteActualizado.TELEFONO = estudiante.TELEFONO;
            estudianteActualizado.ID_INSTITUCION = estudiante.ID_INSTITUCION;

            db.Entry(estudianteActualizado).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("RegistrarEstudiante");
            
        }


        [Authorize(Roles = "admin, profesor")]
        public ActionResult EliminarEstudiante(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var estudiante = db.USUARIOS.Find(id);

            if (estudiante == null)
            {
                return HttpNotFound();
            }

            return View(estudiante);
        }

        [HttpPost]

        [Authorize(Roles = "admin, profesor")]
        public ActionResult EliminarEstudiante(USUARIOS estudiante)
        {
            if (estudiante == null)
            {
                return HttpNotFound();
            }

            USUARIOS estudianteABorrar = db.USUARIOS.Find(estudiante.ID_USUARIO);

            db.USUARIOS.Remove(estudianteABorrar);
            db.SaveChanges();

            return RedirectToAction("RegistrarEstudiante");

        }

        internal bool getUser(string id)
        {

            var usuario = db.USUARIOS.Where(u => u.Id == id).FirstOrDefault();

            if (usuario != null)
            {
                return true;
            }

            return false;
        }


        [Authorize(Roles = "admin, profesor, estudiante")]
        public ActionResult InsertarEstudiante()

        {
            
            ViewBag.institucion = db.INSTITUCION.ToList();
            ViewBag.loginID = TempData["Id"];
            //ViewBag.rolID = TempData["rol"];



            return View();
        }


        [Authorize(Roles = "admin, profesor, estudiante")]
        [HttpPost]
        public ActionResult InsertarEstudiante(USUARIOS estudiante,FormCollection values)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.institucion = db.INSTITUCION.ToList();
                ViewBag.loginID = values["loginID"];
                return View(estudiante); // returns the view with errors
            }

            var loginId = values["loginID"];
            AccountController ac = new AccountController();
            var rolId = ac.getRole(loginId);
            estudiante.Id = loginId;
            estudiante.IdRol = rolId;

            db.USUARIOS.Add(estudiante);
            db.SaveChanges();

            if (Roles.IsUserInRole(User.Identity.Name, "estudiante"))
            {
                return RedirectToAction("Index", "Estudiante");
            }
            else if (Roles.IsUserInRole(User.Identity.Name, "profesor"))
            {
                return RedirectToAction("RegistrarEstudiante", "Profesor");
            }
            else
            {
                return RedirectToAction("RegistrarEstudiante", "Profesor");
            }
        }


        [Authorize(Roles = "admin, profesor")]
        public ActionResult Tutorias()
        {

            var oTutoria = db.TUTORIA_CURSOS.ToList();

            return View(oTutoria);
        }


        [Authorize(Roles = "admin, profesor")]
        public ActionResult RegistrarEstudiante()
        {

            var usuarios = db.USUARIOS.Where(u => u.AspNetRoles.Name == "estudiante").ToList();

            return View(usuarios);

        }


        [Authorize(Roles = "admin, profesor")]
        public ActionResult Detalles(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var estudiante = db.USUARIOS.Find(id);

            if (estudiante == null)
            {
                return HttpNotFound();
            }

            return View(estudiante);
        }


        [Authorize(Roles = "admin, profesor")]
        public ActionResult AsignarTutoria()
        {

            ViewBag.cursos = db.CURSOS.ToList();
            ViewBag.Estudiantes = db.USUARIOS.Where(u => u.AspNetRoles.Name == "estudiante").ToList();
            ViewBag.Instituciones = db.INSTITUCION.ToList();

            return View();
        }

        [HttpPost]

        [Authorize(Roles = "admin, profesor")]
        public ActionResult AsignarTutoria(FormCollection values)
        {

            var id_curso = values["ID_CURSO"];
            var id_usuario = values["ID_USUARIO"];
            var id_institucion = values["ID_INSTITUCION"];
            var descripcion = values["DESCRIPCION"];
            var fecha = values["FECHA_HORA"];
            var hora = values["Hora"];

            var fecha_hora = fecha + " " + hora;

            USUARIOS estudiante = db.USUARIOS.Find(int.Parse(id_usuario));
            CURSOS curso = db.CURSOS.Find(int.Parse(id_curso));
            INSTITUCION institucion = db.INSTITUCION.Find(int.Parse(id_institucion));

            TUTORIA_CURSOS tc = new TUTORIA_CURSOS
            {
                ID_INSTITUCION = institucion.ID_INSTITUCION,
                ID_USUARIO = estudiante.ID_USUARIO,
                ID_CURSO = curso.ID_CURSO,
                DESCRIPCION = descripcion,
                FECHA_HORA = fecha_hora
                
            };

            if (!ModelState.IsValid)
            {
                return View(tc);
            }

            db.TUTORIA_CURSOS.Add(tc);
            db.SaveChanges();

            return RedirectToAction("RegistrarEstudiante");
        }

        [Authorize(Roles = "admin, profesor")]
        public ActionResult verProvincias()
        {
            return View(db.PROVINCIA.ToList());
        }

        [Authorize(Roles = "admin, profesor")]
        public ActionResult crearProvincia()
        {
            ViewBag.paises = new SelectList(db.PAIS, "ID_PAIS", "DES_PAIS");
            return View();
        }

        [Authorize(Roles = "admin, profesor")]
        [HttpPost]
        public ActionResult crearProvincia(PROVINCIA provincia)
        {
            db.PROVINCIA.Add(provincia);
            db.SaveChanges();
            return View("verProvincias");
        }


        [Authorize(Roles = "admin, profesor")]
        public ActionResult crearArchivo()
        {
            ViewBag.tutorias = new SelectList(db.TUTORIA_CURSOS, "ID_TUTORIA_CURSOS", "DESCRIPCION");
            return View();
        }

        [HttpPost]

        [Authorize(Roles = "admin, profesor")]
        public ActionResult crearArchivo(MATERIAL_TUTORIA archivo)
        {
            ViewBag.tutorias = new SelectList(db.TUTORIA_CURSOS, "ID_TUTORIA_CURSOS", "DESCRIPCION");
            var id_tutoria = db.TUTORIA_CURSOS.Where(t => t.ID_PROFESOR == null).FirstOrDefault().ID_TUTORIA_CURSOS;
            archivo.ID_TUTORIA_CURSOS = id_tutoria;
            db.MATERIAL_TUTORIA.Add(archivo);
            db.SaveChanges();

            return View("subirArchivo");
        }



        [Authorize(Roles = "admin, profesor")]
        public ActionResult subirArchivo()
        {
            return View();
        }

        [HttpPost]

        [Authorize(Roles = "admin, profesor")]
        public ActionResult subirArchivo(HttpPostedFileBase file)
        {
            List<MATERIAL_TUTORIA> tutoria = db.MATERIAL_TUTORIA.ToList();
            String ruta = tutoria.Last().DIRECCION_ARCHIVO + file.FileName;
            subidaArchivo subir = new subidaArchivo();
            String rutaSubir = Server.MapPath("~/Files/");
            rutaSubir += file.FileName;

            subir.subirArchivo(rutaSubir, file);

            

            MATERIAL_TUTORIA tutoriaModificar = tutoria.Last();
            tutoriaModificar.DIRECCION_ARCHIVO = ruta;
            
            db.MATERIAL_TUTORIA.Add(tutoriaModificar);
            db.Entry(tutoriaModificar).State = EntityState.Modified;
            db.SaveChanges();

            ViewBag.tutorias = new SelectList(db.TUTORIA_CURSOS, "ID_TUTORIA_CURSOS", "DESCRIPCION");
            ViewBag.correcto = subir.confirmacion;
            ViewBag.error = subir.error;
            if (subir.error != null)
            {
                db.MATERIAL_TUTORIA.Remove(tutoriaModificar);
                db.SaveChanges();
            } 

            return View("crearArchivo");
        }



        [Authorize(Roles = "admin, profesor")]
        public ActionResult Instituciones()
        {
            var institucion = db.DETALLE_DIRECCION__INSTITUCION.ToList();
            return View(institucion);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Profesores()
        {
            var usuarios = db.USUARIOS.Where(u => u.AspNetRoles.Name == "profesor").ToList();

            return View(usuarios);
        }

        [Authorize(Roles = "profesor")]
        public ActionResult Calificaciones()
        {
            var profesorID = User.Identity.GetUserId();
            var calificaciones = db.CALIFICACION_TUTORIA.Where(c => c.ID_USUARIO == db.USUARIOS.Where(u => u.AspNetUsers.Id == profesorID ).FirstOrDefault().ID_USUARIO).ToList(); 

            decimal promedioCalificacion = 0;

            foreach (var calificacion in calificaciones)
            {
                promedioCalificacion += calificacion.CALIFICACION.Value;
            }
            
            if (calificaciones.Count != 0)
            {
                promedioCalificacion /= calificaciones.Count;
                promedioCalificacion = Math.Round(promedioCalificacion, 2, MidpointRounding.ToEven);
                ViewBag.promedio = promedioCalificacion;
            }
            else
            {
                ViewBag.promedio = "No cuenta con calificaciones";
            }

           

            return View();
        }
    }
}