using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace Comercios.Models
{
    public class Pedido
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public List<Item> items { get; set; }
        
        public Usuario usuario { get; set; }

        [Required]
        [Display(Name = "Fecha Realización")]
        public DateTime fechaRealizacion { get; set; }

        [Required]
        [Display(Name = "Total")]
        public double total { get; set; }
        
    }
}