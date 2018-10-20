using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.EFModels;

namespace PhanMemVeSo.Areas.Admin.Controllers
{
    public class PhieuThusController : Controller
    {
        private PhanPhoiVeSoEntities db = new PhanPhoiVeSoEntities();

        // GET: Admin/PhieuThus
        public ActionResult Index()
        {
            var phieuThus = db.PhieuThus.Include(p => p.DaiLy);
            return View(phieuThus.ToList());
        }

        // GET: Admin/PhieuThus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuThu phieuThu = db.PhieuThus.Find(id);
            if (phieuThu == null)
            {
                return HttpNotFound();
            }
            return View(phieuThu);
        }

        // GET: Admin/PhieuThus/Create
        public ActionResult Create()
        {
            ViewBag.DaiLyId = new SelectList(db.DaiLies, "DaiLyId", "TenDaiLy");
            ViewBag.DateNow = System.DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }

        // POST: Admin/PhieuThus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PhieuThuId,DaiLyId,NgayThu,TienThu")] PhieuThu phieuThu)
        {
            if (ModelState.IsValid)
            {
                db.PhieuThus.Add(phieuThu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DaiLyId = new SelectList(db.DaiLies, "DaiLyId", "TenDaiLy", phieuThu.DaiLyId);
            return View(phieuThu);
        }

        // GET: Admin/PhieuThus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuThu phieuThu = db.PhieuThus.Find(id);
            if (phieuThu == null)
            {
                return HttpNotFound();
            }
            ViewBag.DaiLyId = new SelectList(db.DaiLies, "DaiLyId", "TenDaiLy", phieuThu.DaiLyId);
            return View(phieuThu);
        }

        // POST: Admin/PhieuThus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PhieuThuId,DaiLyId,NgayThu,TienThu")] PhieuThu phieuThu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phieuThu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DaiLyId = new SelectList(db.DaiLies, "DaiLyId", "TenDaiLy", phieuThu.DaiLyId);
            return View(phieuThu);
        }

        // GET: Admin/PhieuThus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuThu phieuThu = db.PhieuThus.Find(id);
            if (phieuThu == null)
            {
                return HttpNotFound();
            }
            return View(phieuThu);
        }

        // POST: Admin/PhieuThus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhieuThu phieuThu = db.PhieuThus.Find(id);
            db.PhieuThus.Remove(phieuThu);
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
