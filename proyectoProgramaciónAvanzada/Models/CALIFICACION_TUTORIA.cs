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
    
    public partial class CALIFICACION_TUTORIA
    {
        public int ID_CALIFICACION_TUTORIA { get; set; }
        public Nullable<decimal> CALIFICACION { get; set; }
        public Nullable<int> ID_TUTORIA_CURSOS { get; set; }
        public Nullable<int> ID_USUARIO { get; set; }
    
        public virtual TUTORIA_CURSOS TUTORIA_CURSOS { get; set; }
        public virtual USUARIOS USUARIOS { get; set; }
    }
}
