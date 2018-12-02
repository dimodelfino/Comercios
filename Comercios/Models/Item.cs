using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comercios.Models
{
    public class Item
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }

        [Display(Name = "Producto")]
        public Producto producto { get; set;}       

        [Display(Name = "Unidades Pedidas")]
        public int cantidad { get; set; }

        [Display(Name = "Costo Total")]
        public double costoItem { get; set; } 

        //[Display(Name = "Fecha de Compra del Producto")]
        //public DateTime fechaProductoAgregado { get; set; }

    }
}