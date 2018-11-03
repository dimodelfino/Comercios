using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    class Producto
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }       
        public double costo { get; set; }
        public double precioSugerido { get; set; }
        public Pedido pedido { get; set; }
        //FABRICADO
        public int tiempoPrevisto { get; set; }
        //IMPORRTADOS
        public string paisOrigen { get; set; }
        public int cantidadMinima { get; set; }
    }
}
