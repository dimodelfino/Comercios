using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    class Pedido
    {
        public int Id { get; set; }
        public List<Producto> productos { get; set; }
        public DateTime fechaRealizacion { get; set; }
        public double total { get; set; }
        public int cantidadProductos { get; set; }

    }
}
