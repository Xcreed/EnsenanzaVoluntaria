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

    public partial class USUARIOS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USUARIOS()
        {
            this.CALIFICACION_TUTORIA = new HashSet<CALIFICACION_TUTORIA>();
            this.DETALLE_DIRECCION__USUARIO = new HashSet<DETALLE_DIRECCION__USUARIO>();
            this.TUTORIA_CURSOS = new HashSet<TUTORIA_CURSOS>();
        }
    
        public int ID_USUARIO { get; set; }
        [Display(Name = "Cédula")]
        [Required(ErrorMessage = "{0} es requerido")]
        [MaxLength(9,ErrorMessage = "{0} debe ser de máximo 9 caracteres")]
        [Phone(ErrorMessage = "{0} solo acepta valores numéricos")]
        public string CEDULA { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "{0} es requerido")]
        public string NOMBRE { get; set; }
        [Display(Name = "Primer Apellido")]
        [Required(ErrorMessage = "{0} es requerido")]
        public string APELLIDO1 { get; set; }
        [Display(Name = "Segundo Apellido")]
        [Required(ErrorMessage = "{0} es requerido")]
        public string APELLIDO2 { get; set; }
        [Display(Name = "Teléfono")]
        [Required(ErrorMessage = "{0} es requerido")]
        [Phone(ErrorMessage = "{0} no es un formato de número de teléfono válido")]
        [MaxLength(9, ErrorMessage = "{0} debe ser de máximo 8 caracteres")]
        public string TELEFONO { get; set; }
        
        public string CORREO { get; set; }
       
        public Nullable<int> ID_ROLES { get; set; }
        public Nullable<int> ID_INSTITUCION { get; set; }

        public string CONTRASENA { get; set; }
        public string Id { get; set; }
        public string IdRol { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CALIFICACION_TUTORIA> CALIFICACION_TUTORIA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETALLE_DIRECCION__USUARIO> DETALLE_DIRECCION__USUARIO { get; set; }
        public virtual ROLES ROLES { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TUTORIA_CURSOS> TUTORIA_CURSOS { get; set; }
        public virtual INSTITUCION INSTITUCION { get; set; }
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual AspNetRoles AspNetRoles { get; set; }
    }
}
