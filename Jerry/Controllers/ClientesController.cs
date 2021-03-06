﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Jerry.Models;

namespace Jerry.Controllers
{
    public class ClientesController : Controller
    {
        private const string  BIND_FIELDS = "clienteID,nombre,apellidoP,apellidoM,email,telefono";
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Clientes
        [Authorize]
        public async Task<ActionResult> Index()
        {
            return View(await db.clientes.ToListAsync());
        }

        // GET: Clientes/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        [Authorize]
        [Authorize(Roles = ApplicationUser.UserRoles.ADMIN + "," + ApplicationUser.UserRoles.ASISTENTE)]
        public ActionResult Create()
        {
            return View("Form_Cliente", new Cliente());
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = ApplicationUser.UserRoles.ADMIN + "," + ApplicationUser.UserRoles.ASISTENTE)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = BIND_FIELDS)] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.clientes.Add(cliente);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View("Form_Cliente", cliente);
        }

        // GET: Clientes/Edit/5
        [Authorize(Roles = ApplicationUser.UserRoles.ADMIN + "," + ApplicationUser.UserRoles.ASISTENTE)]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = await db.clientes.FindAsync(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View("Form_Cliente", cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = ApplicationUser.UserRoles.ADMIN+","+ApplicationUser.UserRoles.ASISTENTE)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = BIND_FIELDS)] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = cliente.clienteID });
            }
            return View("Form_Cliente",cliente);
        }

        // GET: Clientes/Delete/5
        //[Authorize]
        [Authorize(Roles = ApplicationUser.UserRoles.ADMIN)]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = await db.clientes.FindAsync(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        //[Authorize]
        [Authorize(Roles = "Administrador")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Cliente cliente = await db.clientes.FindAsync(id);
            db.clientes.Remove(cliente);
            await db.SaveChangesAsync();
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
