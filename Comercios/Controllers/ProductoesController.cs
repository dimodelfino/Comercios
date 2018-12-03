using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Comercios.Models;

namespace Comercios.Controllers
{
    public class ProductoesController : Controller
    {
        private ComercioContext db = new ComercioContext();

        // GET: Productoes
        public ActionResult Index(string SearchStringCodigo, string SearchStringNombre, string SearchStringDescripcion,
            string SearchStringCostoInicial, string SearchStringCostoFinal, string Tipo, string Busqueda)
        {
            if (Session["Rol"] != null)
            {
                var productos = db.productos.OrderByDescending(p => p.esFabircado).ThenBy(p => p.nombre);

                if (!String.IsNullOrEmpty(SearchStringCodigo) || !String.IsNullOrEmpty(SearchStringNombre) || !String.IsNullOrEmpty(SearchStringDescripcion)
                     || !String.IsNullOrEmpty(SearchStringCostoInicial) || !String.IsNullOrEmpty(SearchStringCostoFinal) || !String.IsNullOrEmpty(Tipo))
                {
                    var prod = from p in db.productos select p;

                    if (Busqueda == "Nombre" && !String.IsNullOrEmpty(SearchStringNombre))
                    {
                        prod = productos.Where(s => s.nombre.Contains(SearchStringNombre));
                        return View(prod.ToList());
                    }
                    else if (Busqueda == "Codigo" && !String.IsNullOrEmpty(SearchStringCodigo))
                    {
                        var searchInt = Convert.ToInt32(SearchStringCodigo);
                        prod = productos.Where(s => s.Id.Equals(searchInt));
                        return View(prod.ToList());
                    }
                    else if (Busqueda == "Descripcion" && !String.IsNullOrEmpty(SearchStringDescripcion))
                    {
                        prod = productos.Where(s => s.descripcion.Contains(SearchStringDescripcion));
                        return View(prod.ToList());
                    }
                    else if (Busqueda == "RangoPrecios" && !String.IsNullOrEmpty(SearchStringCostoInicial) && !String.IsNullOrEmpty(SearchStringCostoFinal))
                    {
                        var costoInicial = Convert.ToInt32(SearchStringCostoInicial);
                        var costoFinal = Convert.ToInt32(SearchStringCostoFinal);
                        prod = productos.Where(s => s.costo >= costoInicial && s.costo <= costoFinal);
                        return View(prod.ToList());
                    }
                    else if (Busqueda == "Tipo" && !String.IsNullOrEmpty(Tipo))
                    {
                        if (Tipo == "Importado")
                        {
                            prod = productos.Where(s => s.esFabircado.Equals(false));
                            return View(prod.ToList());
                        }
                        else
                        {
                            prod = productos.Where(s => s.esFabircado.Equals(true));
                            return View(prod.ToList());
                        }
                    }
                }
                return View(productos.ToList());
            }
            return RedirectToAction("Login", "Usuarios");
        }

        // GET: Productoes/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Rol"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Producto producto = db.productos.Find(id);
                if (producto == null)
                {
                    return HttpNotFound();
                }
                return View(producto);
            }
            return RedirectToAction("Login", "Usuarios");
        }

        // GET: Productoes/Create
        public ActionResult Create()
        {
            if (Session["Rol"].Equals("Admin"))
            {
                return View();
            }
            return RedirectToAction("Index");
        }

        // POST: Productoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,nombre,descripcion,costo,precioSugerido,tiempoPrevisto,paisOrigen,cantidadMinima,esFabricado")] Producto producto)
        {
            if (producto.esFabircado)
            {
                producto.paisOrigen = "-";
            }
            else
            {
                producto.tiempoPrevisto = 0;
            }
            if (ModelState.IsValid)
            {
                db.productos.Add(producto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(producto);
        }

        // GET: Productoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Rol"] != null && Session["Rol"].Equals("empleado"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Producto producto = db.productos.Find(id);
                if (producto == null)
                {
                    return HttpNotFound();
                }
                return View(producto);
            }
            return RedirectToAction("Index");
        }

        // POST: Productoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,nombre,descripcion,costo,precioSugerido,esFabircado,tiempoPrevisto,paisOrigen,cantidadMinima")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                if (producto.costo <= producto.precioSugerido * 1.10 && producto.costo >= producto.precioSugerido)
                {
                    //Producto p = (Producto)db.productos.Find(producto.Id);
                    //producto.esFabircado = p.esFabircado;
                    db.Entry(producto).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("costo", "El costo del producto no puede ser menor al precio sugerido ni tampoco superarlo por un 10 %");
                }
            }
            return View(producto);
        }

        // GET: Productoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Rol"].Equals("Admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Producto producto = db.productos.Find(id);
                if (producto == null)
                {
                    return HttpNotFound();
                }
                return View(producto);
            }
            return RedirectToAction("Index");
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Producto producto = db.productos.Find(id);
            db.productos.Remove(producto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult agregarProductoAPedido(int? id)
        {
            if (Session["Rol"] != null && Session["Rol"].Equals("cliente"))
            {
                var producto = db.productos.Find(id);
                int idUsuario = idUsuario = Convert.ToInt32(Session["idUsuario"]);
                if (Session["Pedido"] == null)
                {
                    Pedido ped = new Pedido()
                    {
                        items = new List<Item>()
                    };

                    Item it = new Item()
                    {
                        cantidad = 1,
                        producto = producto,
                        costoItem = producto.costo
                    };

                    ped.items.Add(it);
                    Session["Pedido"] = ped;
                }
                else
                {
                    Pedido pedido = (Pedido)Session["Pedido"];
                    bool existe = false;
                    int index = -1;
                    foreach (Item itm in pedido.items)
                    {
                        if (itm.producto.Id == producto.Id)
                        {
                            existe = true;
                            index = pedido.items.IndexOf(itm);
                        }
                    }
                    if (existe)
                    {
                        pedido.items[index].cantidad++;
                        pedido.items[index].costoItem = producto.costo * pedido.items[index].cantidad;
                    }
                    else
                    {
                        Item it = new Item()
                        {
                            cantidad = 1,
                            producto = producto,
                            costoItem = producto.costo
                        };
                        pedido.items.Add(it);
                    }
                    Session["Pedido"] = pedido;
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult CargarProductos()
        {
            if (Session["Rol"] != null && Session["Rol"].Equals("empleado"))
            {
                if (db.productos.Count() == 0)
                {
                    CargarPorductos();
                }
                else
                {
                    ViewBag.mensaje = "Los prodcutos ya fueron cargados al sistema";
                }
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }

        public void CargarPorductos()
        {
            if (Session["Rol"] != null && Session["Rol"].Equals("empleado"))
            {
                List<String> listaTemp = new List<String>();
                string ruta = HttpRuntime.AppDomainAppPath + @"Archivos\ListadoProductos.txt";
                StreamReader sr = new StreamReader(ruta);
                string linea = null;
                linea = sr.ReadLine();

                while ((linea != null))
                {
                    listaTemp.Add(linea);
                    linea = sr.ReadLine();
                }

                for (int i = 0; i < listaTemp.Count; i++)
                {
                    string cadena = listaTemp[i];
                    string[] vec1 = cadena.Split('|');
                    List<Producto> listaProductos = new List<Producto>();

                    if (vec1 != null)
                    {
                        Producto p = new Producto();

                        p.nombre = vec1[0].ToString();
                        p.descripcion = vec1[1].ToString();
                        p.costo = double.Parse(vec1[3]);
                        p.precioSugerido = double.Parse(vec1[3]);
                        p.esFabircado = bool.Parse(vec1[4]);
                        if (vec1[5] != "*") p.tiempoPrevisto = int.Parse(vec1[5]);
                        if (vec1[6] != "*") p.paisOrigen = vec1[6].ToString();
                        if (vec1[7] != "*") p.cantidadMinima = int.Parse(vec1[7]);
                        db.productos.Add(p);
                        db.SaveChanges();
                    }
                }
                ViewBag.mensaje = "Los productos se han cargado exitosamente!";
            }
        }
    }
}
