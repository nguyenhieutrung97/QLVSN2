using Model.EFModels;
using Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhanMemVeSo.Areas.Admin.Controllers
{
    public class QuanLySoTrungController : Controller
    {
        private PhanPhoiVeSoEntities db = new PhanPhoiVeSoEntities();
        // GET: Admin/QuanLySoTrung
        public ActionResult Index(int? loaiVeSoId,System.DateTime? ngayDoSo)
        {
            System.DateTime ngayDo = ngayDoSo.GetValueOrDefault(System.DateTime.Now).Date;
            loaiVeSoId = loaiVeSoId ?? 1;
            ViewBag.NgayDo = ngayDo.ToString("yyyy-MM-dd");
            ViewBag.LoaiVeSo = db.LoaiVeSoes.Where(m => m.LoaiVeSoId == loaiVeSoId).Select(m=>m.TenTinh).SingleOrDefault();
            var listKetQua = db.KetQuaXoSoes.Where(m => m.LoaiVeSoId == loaiVeSoId & System.DateTime.Compare(m.NgayXoSo, ngayDo) == 0).OrderBy(m=>m.GiaiId);
            return View(listKetQua.ToList());
        }

        public ActionResult findDateandTypeVeSo()
        {
            ViewBag.LoaiVeSoId = new SelectList(db.LoaiVeSoes, "LoaiVeSoId", "TenTinh");
            ViewBag.DateNow = System.DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }

        [HttpPost]
        public ActionResult findDateandTypeVeSo(int loaiVeSoId,System.DateTime ngayXoSo)
        {
            return RedirectToAction("Create", "QuanLySoTrung",new { loaiVeSoId=loaiVeSoId,ngayXoSo=ngayXoSo });
        }
        public ActionResult Create(int loaiVeSoId, System.DateTime ngayXoSo)
        {
            ViewBag.LoaiVeSoId = loaiVeSoId;
            ViewBag.NgayXoSo = ngayXoSo;
            ViewBag.LoaiVeSo = db.LoaiVeSoes.Where(m => m.LoaiVeSoId == loaiVeSoId).Select(m => m.TenTinh).SingleOrDefault();
            ViewBag.NgayXoSoShow = ngayXoSo.ToString("dd-MM-yyyy");
            var listGiai = db.Giais.OrderByDescending(m => m.TienThuong);
            List<int> listGiaiId = new List<int>();
            foreach(var item in listGiai)
            {
                for(int i = 1; i <= item.SLSoTrung; i++)
                {
                    listGiaiId.Add(item.GiaiId);
                }
            }
            List<KetQuaXoSoVM> listKQXS = new List<KetQuaXoSoVM>();
            foreach(var item in listGiaiId)
            {
                KetQuaXoSoVM kqxs = new KetQuaXoSoVM();
                kqxs.GiaiId = item;
                kqxs.TenGiai = db.Giais.Where(m => m.GiaiId == item).Select(m => m.TenGiai).FirstOrDefault();
                listKQXS.Add(kqxs);
            }
            return View(listKQXS);
        }
        [HttpPost]
        public ActionResult Create(List<KetQuaXoSoVM> listKQXS, int loaiVeSoId, System.DateTime ngayXoSo)
        {
            if (ModelState.IsValid)
            {
                foreach(var item in listKQXS)
                {
                    int giaiId = db.Giais.Where(m => m.TenGiai == item.TenGiai).Select(m => m.GiaiId).FirstOrDefault();
                    KetQuaXoSo kqxs = new KetQuaXoSo() { GiaiId = giaiId, SoTrung = item.SoTrung, LoaiVeSoId = loaiVeSoId, NgayXoSo = ngayXoSo };
                    db.KetQuaXoSoes.Add(kqxs);
                }
                db.SaveChanges();
                return RedirectToAction("Index", "QuanLySoTrung");
            }
            return View(listKQXS);
            
        }
    }
}