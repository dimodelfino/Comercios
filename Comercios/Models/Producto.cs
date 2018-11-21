using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Comercios.Models
{
    public class Producto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre")]
        [StringLength(80)]
        [Display(Name = "Producto")]
        public string nombre { get; set; }

        [StringLength(450)]
        [Required(ErrorMessage = "Debe ingresar una descripción")]
        [Display(Name = "Descripción")]
        public string descripcion { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Ingrese un valor positivo.")]
        [Required(ErrorMessage = "Debe ingresar un costo")]
        [Display(Name = "Costo")]
        public double costo { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Ingrese un valor positivo.")]
        [Required(ErrorMessage = "Debe ingresar un precio sugerido")]
        [Display(Name = "Precio sugerido")]
        public double precioSugerido { get; set; }

        public Pedido pedido { get; set; }

        [Display(Name = "Producto Fabricado")]
        public bool esFabircado { get; set; }
      //public string tipo { get; set; }

        //FABRICADO
                
        [Range(0, int.MaxValue, ErrorMessage = "Ingrese un valor positivo.")]
        [Display(Name = "Tiempo prevísto")]
        public int tiempoPrevisto { get; set; }
        //IMPORRTADOS
        
        [StringLength(80)]
        [Display(Name = "País de origen")]
        public string paisOrigen { get; set; }
        
        [Range(0, int.MaxValue, ErrorMessage = "Ingrese un valor positivo.")]
        [Display(Name = "Cantidad mínima")]
        public int cantidadMinima { get; set; }
    }
}