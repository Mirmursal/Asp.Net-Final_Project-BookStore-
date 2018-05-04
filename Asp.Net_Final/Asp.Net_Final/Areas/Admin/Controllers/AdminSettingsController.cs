using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Asp.Net_Final.Models;

namespace Asp.Net_Final.Areas.Admin.Controllers
{
    public class AdminSettingsController : Controller
    {
        private Entities db = new Entities();

        // GET: Admin/AdminSettings
        public ActionResult Index()
        {
            return View(db.AdminSettings.ToList());
        }

        // GET: Admin/AdminSettings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminSetting adminSetting = db.AdminSettings.Find(id);
            if (adminSetting == null)
            {
                return HttpNotFound();
            }
            return View(adminSetting);
        }

        // GET: Admin/AdminSettings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminSettings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name,Lastname,Email,Password,Image,Status")] AdminSetting adminSetting)
        {
            if (ModelState.IsValid)
            {
                db.AdminSettings.Add(adminSetting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(adminSetting);
        }

        // GET: Admin/AdminSettings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminSetting adminSetting = db.AdminSettings.Find(id);
            if (adminSetting == null)
            {
                return HttpNotFound();
            }
            return View(adminSetting);
        }

        // POST: Admin/AdminSettings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name,Lastname,Email,Password,Image,Status")] AdminSetting adminSetting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adminSetting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(adminSetting);
        }

        // GET: Admin/AdminSettings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdminSetting adminSetting = db.AdminSettings.Find(id);
            if (adminSetting == null)
            {
                return HttpNotFound();
            }
            return View(adminSetting);
        }

        // POST: Admin/AdminSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AdminSetting adminSetting = db.AdminSettings.Find(id);
            db.AdminSettings.Remove(adminSetting);
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
