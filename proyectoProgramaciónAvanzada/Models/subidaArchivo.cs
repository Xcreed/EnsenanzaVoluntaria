using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace proyectoProgramaciónAvanzada.Models
{
    public class subidaArchivo
    {
        public string confirmacion { get; set; }
        public Exception error { get; set; }

        public void subirArchivo(String ruta, HttpPostedFileBase file) 
        {
            try 
            {
                file.SaveAs(ruta);
                confirmacion = "Archivo Guardado Exitosamente";
            }
            catch (Exception err)
            {
                this.error = err;
            }
        }
    }
}