//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace proyectoProgramaciónAvanzada.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class MATERIAL_TUTORIA
    {

        [Display(Name = "Tutoría")]
        public int ID_MATERIAL_TUTORIA { get; set; }
        [Display(Name = "Tutoría-Curso ")]
        public int ID_TUTORIA_CURSOS { get; set; }
        [Display(Name = "Dirección Archivo")]
        public string DIRECCION_ARCHIVO { get; set; }
        [Display(Name = "Título")]
        public string TITULO_COMENTARIO { get; set; }
        [Display(Name = "Comentario")]
        public string CUERPO_COMENTARIO { get; set; }
    
        public virtual TUTORIA_CURSOS TUTORIA_CURSOS { get; set; }
    }
}
