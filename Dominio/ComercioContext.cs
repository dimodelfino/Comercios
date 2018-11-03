using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace Dominio
{
    class ComercioContext: DbContext
    {
        public DbSet<Producto> productos { get; set; }
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Pedido> pedidos { get; set; }
        public ComercioContext() : base("Conexion_ThinkPad") {} 

    }
}
