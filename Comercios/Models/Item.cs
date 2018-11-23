using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Comercios.Models
{
    public class Item
    {
        public int id { get; set; }

        public int idProducto { get; set;}

        public int idPedido { get; set; }

        public int cantidad { get; set; }

    }
}