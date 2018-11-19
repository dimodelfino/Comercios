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
        //FABRICADO

        [Required(ErrorMessage = "Debe ingresar un tiemmpo previsto")]
        [Range(0, int.MaxValue, ErrorMessage = "Ingrese un valor positivo.")]
        [Display(Name = "Tiempo previsto")]
        public int tiempoPrevisto { get; set; }
        //IMPORRTADOS

        [Required(ErrorMessage = "Debe ingresar un pais de origen")]
        [StringLength(80)]
        [Display(Name = "País de origen")]
        public string paisOrigen { get; set; }

        [Required(ErrorMessage = "Debe ingresar una cantidad minima")]
        [Range(0, int.MaxValue, ErrorMessage = "Ingrese un valor positivo.")]
        [Display(Name = "Cantidad minima")]
        public int cantidadMinima { get; set; }
    }
}