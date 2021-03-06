﻿using System;
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
    public class UsuariosController : Controller
    {
        private ComercioContext db = new ComercioContext();

        // GET: Usuarios
        public ActionResult Index()
        {
            if (Session["idUsuario"] != null && Session["Rol"].ToString() != "Admin")
            {
                return View(db.usuarios.ToList());
            }
            return RedirectToAction("Login");
        }

        // GET: Usuarios
        public ActionResult Login()
        {
            return View();
        }

        // POST: Usuarios/Login
        [HttpPost]
        public ActionResult Login(string email, string contrasena)
        {
            try
            {
                using (db)
                {
                    var usr = db.usuarios.SingleOrDefault
                        (u => u.email.ToUpper() == email.ToUpper()
                        && u.contrasena == contrasena);
                    if (usr != null)
                    {
                        Session["Rol"] = usr.rol;
                        Session["Email"] = usr.email;
                        Session["idUsuario"] = usr.Id;
                        Session["Pedido"] = null;
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("LoginIncorrecto", "El mail o contraseña no son correctos");
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session["Rol"] = null;
            Session["Email"] = null;
            return RedirectToAction("Index", "Home");
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["idUsuario"] != null && Session["Rol"].ToString() != "Admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Usuario usuario = db.usuarios.Find(id);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                return View(usuario);
            }
            return RedirectToAction("Login");
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,nombre,contrasena,email,telefono")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.rol = "cliente";
                usuario.fechaRegistro = DateTime.Now;
                db.usuarios.Add(usuario);
                db.SaveChanges();
                Session["Rol"] = usuario.rol;
                Session["Email"] = usuario.email;
                Session["idUsuario"] = usuario.Id;
                Session["Pedido"] = null;
                return RedirectToAction("Index", "Home");
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["idUsuario"] != null && Session["Rol"].ToString() != "Admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Usuario usuario = db.usuarios.Find(id);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                return View(usuario);
            }
            return RedirectToAction("Login");
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,nombre,contrasena,email")] Usuario usuario)
        {
            if (Session["idUsuario"] != null && Session["Rol"].ToString() != "Admin")
            {
                if (ModelState.IsValid)
                {
                    db.Entry(usuario).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(usuario);
            }
            return RedirectToAction("Login");
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["idUsuario"] != null && Session["Rol"].ToString() != "Admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Usuario usuario = db.usuarios.Find(id);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                return View(usuario);
            }
            return RedirectToAction("Login");
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.usuarios.Find(id);
            db.usuarios.Remove(usuario);
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
    }
}
