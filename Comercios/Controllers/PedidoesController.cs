using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Comercios.Models;
using Comercios.ViewModel;

namespace Comercios.Controllers
{
    public class PedidoesController : Controller
    {
        private ComercioContext db = new ComercioContext();

        // GET: Pedidoes
        public ActionResult Index()
        {
            if (Session["Rol"] != null)
            {
                int idUsuario = Convert.ToInt32(Session["idUsuario"]);
                List<Pedido> pedidos = db.pedidos.Where(p => p.usuario.Id == idUsuario).ToList();
                if (pedidos.Count != 0)
                {
                    return View(pedidos);
                }
                else
                {
                    ViewBag.message = "No hay pedidos para mostrar!";
                    return View(pedidos);
                }
            }
            return RedirectToAction("Login", "Usuarios");
        }

        // GET: Pedidoes
        public ActionResult ProductosPedido(int id)
        {
            if (Session["Rol"] != null && Session["Rol"].Equals("cliente"))
            {
                CarritoViewModel c = new CarritoViewModel();
                c.pedido = db.pedidos.Where(p => p.Id == id).Include(p => p.items.Select(i => i.producto)).SingleOrDefault();
                return View(c);
            }
            return RedirectToAction("Login", "Usuarios");
        }

        // GET: Pedidoes
        public ActionResult IndexCarrito()
        {
            if (Session["Rol"] != null && Session["Rol"].Equals("cliente"))
            {
                CarritoViewModel p = new CarritoViewModel();
                p.pedido = (Pedido)Session["Pedido"];
                return View(p);
            }
            return RedirectToAction("Login", "Usuarios");
        }

        // GET: Pedidoes/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Rol"] != null && Session["Rol"].Equals("cliente"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Pedido pedido = db.pedidos.Find(id);
                if (pedido == null)
                {
                    return HttpNotFound();
                }
                return View(pedido);
            }
            return RedirectToAction("Login", "Usuarios");
        }

        // GET: Pedidoes/Create
        public ActionResult Create()
        {
            if (Session["Rol"] != null && Session["Rol"].Equals("Admin"))
            {
                return View();
            }
            return RedirectToAction("Login", "Usuarios");
        }

        // POST: Pedidoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,fechaRealizacion,total,cantidadProductos")] Pedido pedido)
        {
            if (Session["Rol"] != null && Session["Rol"].Equals("Admin"))
            {
                if (ModelState.IsValid)
                {
                    db.pedidos.Add(pedido);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(pedido);
            }
            return RedirectToAction("Login", "Usuarios");
        }

        // GET: Pedidoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Rol"] != null && Session["Rol"].Equals("Admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Pedido pedido = db.pedidos.Find(id);
                if (pedido == null)
                {
                    return HttpNotFound();
                }
                return View(pedido);
            }
            return RedirectToAction("Login", "Usuarios");
        }

        // POST: Pedidoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,fechaRealizacion,total,cantidadProductos")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pedido);
        }

        // GET: Pedidoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Rol"] != null && Session["Rol"].Equals("Admin"))
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Pedido pedido = db.pedidos.Find(id);
                if (pedido == null)
                {
                    return HttpNotFound();
                }
                return View(pedido);
            }
            return RedirectToAction("Login", "Usuarios");
        }

        // POST: Pedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pedido pedido = db.pedidos.Find(id);
            db.pedidos.Remove(pedido);
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

        public ActionResult confirmarPedido()
        {
            if (Session["Rol"] != null && Session["Rol"].Equals("cliente"))
            {
                int idUsuario = Convert.ToInt32(Session["idUsuario"]);
                Usuario usu = db.usuarios.Find(idUsuario);
                Pedido pedido = (Pedido)Session["Pedido"];
                pedido.usuario = usu;
                pedido.fechaRealizacion = DateTime.Now;

                foreach (Item it in pedido.items)
                {
                    pedido.total += it.costoItem;
                }

                db.pedidos.Add(pedido);

                foreach (Item it in pedido.items)
                {
                    db.Entry(it.producto).State = EntityState.Unchanged;
                }

                db.SaveChanges();

                Session["Pedido"] = null;
                return RedirectToAction("IndexCarrito");
            }
            return RedirectToAction("Login", "Usuarios");
        }


        public ActionResult SumarUnProducto(int id)
        {
            Pedido p = (Pedido)Session["Pedido"];
            Item it = p.items.Find(i => i.producto.Id == id);

            it.cantidad++;
            it.costoItem = it.producto.costo * it.cantidad;
            var index = p.items.IndexOf(it);

            if (index != -1)
            {
                p.items[index] = it;
            }

            Session["Pedido"] = p;
            return RedirectToAction("IndexCarrito");
        }

        public ActionResult RestarUnProducto(int id)
        {
            Pedido p = (Pedido)Session["Pedido"];
            Item it = p.items.Find(i => i.producto.Id == id);

            if (it.cantidad - 1 == 0)
            {
                p.items.Remove(it);
            }
            else
            {
                it.cantidad--;
                it.costoItem = it.producto.costo * it.cantidad;
                var index = p.items.IndexOf(it);

                if (index != -1)
                    p.items[index] = it;
            }

            Session["Pedido"] = p;
            return RedirectToAction("IndexCarrito");
        }

        public ActionResult EliminarProductoCarrito(int id)
        {
            Pedido p = (Pedido)Session["Pedido"];
            Item it = p.items.Find(i => i.producto.Id == id);
            p.items.Remove(it);
            Session["Pedido"] = p;
            return RedirectToAction("IndexCarrito");
        }

    }
}
