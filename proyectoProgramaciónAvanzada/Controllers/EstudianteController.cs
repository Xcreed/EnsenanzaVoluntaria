using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using proyectoProgramaciónAvanzada.Models;

namespace proyectoProgramaciónAvanzada.Controllers
{
    public class EstudianteController : Controller
    {
        private EnseñanzaVoluntariaEntities db = new EnseñanzaVoluntariaEntities();

        // GET: Estudiante
        [Authorize(Roles = "admin, estudiante, profesor")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "admin, estudiante, profesor")]
        public ActionResult Continentes()
        {
            return View("Estudios Sociales/Continentes");
        }

        [Authorize(Roles = "admin, estudiante, profesor")]
        public ActionResult Tectonismo()
        {
            return View("Estudios Sociales/Tectonismo");
        }

        [Authorize(Roles = "admin, estudiante, profesor")]
        public ActionResult HistoriaCR()
        {
            return View("Estudios Sociales/HistoriaCostaRica");
        }

        [Authorize(Roles = "admin, estudiante, profesor")]
        public ActionResult EstructuraOracion()
        {
            return View("Espaniol/EstructuraOracion");
        }

        [Authorize(Roles = "admin, estudiante, profesor")]
        public ActionResult UsoVyB()
        {
            return View("Espaniol/UsoVyB");
        }

        [Authorize(Roles = "admin, estudiante, profesor")]
        public ActionResult UsoSignosPuntuacion()
        {
            return View("Espaniol/UsoSignosPuntuacion");
        }

        [Authorize(Roles = "admin, estudiante, profesor")]
        public ActionResult Esqueleto()
        {
            return View("Ciencias/Esqueleto");
        }

        [Authorize(Roles = "admin, estudiante, profesor")]
        public ActionResult Celula()
        {
            return View("Ciencias/Celula");
        }

        [Authorize(Roles = "admin, estudiante, profesor")]
        public ActionResult SistemaDigestivo()
        {
            return View("Ciencias/SistemaDigestivo");
        }

        [Authorize(Roles = "admin, estudiante, profesor")]
        public ActionResult Multiplicacion()
        {
            return View("Matemáticas/Multiplicacion");
        }

        [Authorize(Roles = "admin, estudiante, profesor")]
        public ActionResult Resta()
        {
            return View("Matemáticas/Resta");
        }

        [Authorize(Roles = "admin, estudiante, profesor")]
        public ActionResult Suma()
        {
            return View("Matemáticas/Suma");
        }

        [Authorize(Roles = "admin, estudiante, profesor")]
        public ActionResult Archivos()
        {
            return View(db.MATERIAL_TUTORIA.ToList());
        }

        [Authorize(Roles = "admin, estudiante, profesor")]
        [HttpPost]
        public ActionResult Descargar(FormCollection valores)
        {
            var rutaArchivo = valores["RUTA"];

            var ruta = Server.MapPath(rutaArchivo);

            return File(ruta,"application/", rutaArchivo);
        }

        [Authorize(Roles = "admin, estudiante")]
        public ActionResult CalificaProfe()
        {
            ViewBag.Profesores = db.USUARIOS.Where(u => u.AspNetRoles.Name == "profesor").ToList();

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin, estudiante")]
        public ActionResult CalificarProfesor(FormCollection valores)
        {
            var ID_PROFE = valores["ID_USUARIO"];
            var CALIFICACIONDADA = valores["CALIFICACION"];
            var ID_TUTORIA_CURSOS_PARAM = db.TUTORIA_CURSOS.Where(t => t.ID_PROFESOR == null).FirstOrDefault().ID_TUTORIA_CURSOS;
            ViewBag.Profesores = db.USUARIOS.Where(u => u.AspNetRoles.Name == "profesor").ToList();

            CALIFICACION_TUTORIA calif = new CALIFICACION_TUTORIA()
            {
                CALIFICACION = decimal.Parse(CALIFICACIONDADA),
                ID_TUTORIA_CURSOS = ID_TUTORIA_CURSOS_PARAM,
                ID_USUARIO = int.Parse(ID_PROFE)
            };

            try
            {
                db.CALIFICACION_TUTORIA.Add(calif);
                db.SaveChanges();
                ViewBag.exito = "Calificación enviada exitosamente.";
            }
            catch 
            {
                ViewBag.error = "Error al enviar la calificación.";
            }

            return View("CalificaProfe");
        }
    }
}