using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Comercios.Models
{
    public class ComercioContext: DbContext
    {
        public DbSet<Producto> productos { get; set; }
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Pedido> pedidos { get; set; }
        public ComercioContext() : base("Conexion_ThinkPad") { }

    }
}