using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Comercios.Models
{
    public class Usuario
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [StringLength(80)]
        [Required(ErrorMessage = "Debe ingresar un nombre")]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [StringLength(20)]
        // [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,20}$", ErrorMessage = "La contraseña debe tener al menos 6 caracteres, una mayuscula, una minuscula y un digito.")]
        [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*).{6,})", ErrorMessage = "La contraseña debe tener al menos 6 caracteres, una mayuscula, una minuscula y un digito.")]       
        [Required(ErrorMessage = "Debe ingresar una contraseña")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string contrasena { get; set; }

        [StringLength(80)]
        [Index(IsUnique = true)]
        [Required(ErrorMessage = "Debe ingresar un email")]
        [Display(Name = "Email")]
        public string email { get; set; }

        //TODO: Agregar las data anotations de rol
        [Display(Name = "Rol")]
        public string rol { get; set; }

        //TODO: Ver si hay que agregar data anotations de fechaRegistro
        [Display(Name = "Fecha Registro")]
        public DateTime fechaRegistro { get; set; }
    }
}