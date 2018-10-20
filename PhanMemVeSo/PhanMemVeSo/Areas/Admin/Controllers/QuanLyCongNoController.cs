using Model.Bus;
using Model.EFModels;
using Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhanMemVeSo.Areas.Admin.Controllers
{
    public class QuanLyCongNoController : Controller
    {
        private PhanPhoiVeSoEntities db = new PhanPhoiVeSoEntities();
        // GET: Admin/QuanLyCongNo
        public ActionResult Index(System.DateTime? ngayCanTim)
        {
            ViewBag.DateNow = System.DateTime.Now.ToString("yyyy-MM-dd");
            System.DateTime ngayCanTimGan = ngayCanTim.GetValueOrDefault(System.DateTime.Now);
            DaiLyBus daiLyBus = new DaiLyBus();
            List<PhieuCongNo> listPhieuCongNo = new List<PhieuCongNo>();
            foreach(var item in db.DaiLies)
            {
                PhieuCongNo phieuCongNo = new PhieuCongNo();
                phieuCongNo.DaiLyId = item.DaiLyId;
                phieuCongNo.TenDaiLy = item.TenDaiLy;
                phieuCongNo.TaiKhoan = daiLyBus.TinhToanCongNoTheoDaiLy(item.DaiLyId, ngayCanTimGan);
                listPhieuCongNo.Add(phieuCongNo);
            }
            return View(listPhieuCongNo);
        }
    }
}