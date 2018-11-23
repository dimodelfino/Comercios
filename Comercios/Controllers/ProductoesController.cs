using System;
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

                    if (Busqueda == "Nombre")
                    {
                        prod = productos.Where(s => s.nombre.Contains(SearchStringNombre));
                        return View(prod.ToList());
                    }
                    else if (Busqueda == "Codigo")
                    {
                        var searchInt = Convert.ToInt32(SearchStringCodigo);
                        prod = productos.Where(s => s.Id.Equals(searchInt));
                        return View(prod.ToList());
                    }
                    else if (Busqueda == "Descripcion")
                    {
                        prod = productos.Where(s => s.descripcion.Contains(SearchStringDescripcion));
                        return View(prod.ToList());
                    }
                    else if (Busqueda == "RangoPrecios")
                    {
                        var costoInicial = Convert.ToInt32(SearchStringCostoInicial);
                        var costoFinal = Convert.ToInt32(SearchStringCostoFinal);
                        prod = productos.Where(s => s.costo >= costoInicial && s.costo <= costoFinal);
                        return View(prod.ToList());
                    }
                    else if (Busqueda == "Tipo")
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

        // GET: Productoes/Create
        public ActionResult Create()
        {
            if (Session["Rol"].Equals("empleado"))
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
            if (Session["Rol"].Equals("empleado"))
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
        public ActionResult Edit([Bind(Include = "Id,nombre,descripcion,costo,precioSugerido,tiempoPrevisto,paisOrigen,cantidadMinima")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                if (producto.costo <= producto.precioSugerido * 1.10 && producto.costo >= producto.precioSugerido)
                {
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

        public void agregarProductoAPedido(int idPedido, int idProducto)
        {
            //TODO
            var pedido = db.pedidos.Find(idPedido);
            var producto = db.productos.Find(idProducto);
        }

    }
}
