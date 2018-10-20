using Model.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Bus
{
    public class DaiLyBus
    {
        private PhanPhoiVeSoEntities db = new PhanPhoiVeSoEntities();
        public decimal TinhToanSLPhatTheoDaiLy(int loaiVeSoId,int daiLyId, System.DateTime ngayPhatHienTai)
        {
            decimal slDangKy = db.PhieuDangKies.OrderByDescending(m => m.NgayDangKy).Where(m => m.DaiLyId == daiLyId & m.LoaiVeSoId==loaiVeSoId &System.DateTime.Compare(m.NgayDangKy, ngayPhatHienTai) <=0).Select(m=>m.SLDangKy).FirstOrDefault();
            System.DateTime ngayDangKy= db.PhieuDangKies.OrderByDescending(m => m.NgayDangKy).Where(m => m.DaiLyId == daiLyId &m.LoaiVeSoId==loaiVeSoId& System.DateTime.Compare(m.NgayDangKy, ngayPhatHienTai) <= 0).Select(m => m.NgayDangKy).FirstOrDefault();
            var listTop3 = db.PhieuPhatHanhs.Where(m => m.DaiLyId == daiLyId & m.LoaiVeSoId==loaiVeSoId & System.DateTime.Compare(m.NgayPhat, ngayPhatHienTai) <= 0 &m.SLBanDuoc!=null).OrderByDescending(m => m.NgayPhat).Take(3);
            int count = listTop3.Count();
            if (count == 0)
            {
                return slDangKy;
            }
            else {
                decimal? sum = 0;
                foreach(var item in listTop3)
                {
                    sum += item.SLBanDuoc / item.SLPhat;
                }
                decimal? getReturn = Math.Round((decimal)sum / count * slDangKy);
                return getReturn??default(decimal);
            }
        }

        public decimal TinhToanCongNoTheoDaiLy(int daiLyId, System.DateTime ngayCanTim)
        {
            decimal tienDaTra = 0;
            var listTienDaTra = db.PhieuThus.Where(m => m.DaiLyId == daiLyId & System.DateTime.Compare(m.NgayThu, ngayCanTim) <= 0) ;
            if (listTienDaTra.Count() != 0)
            {
                tienDaTra = listTienDaTra.Select(m => m.TienThu).Sum();
            }
            var listTienConThieu = db.PhieuPhatHanhs.Where(m => m.DaiLyId == daiLyId & System.DateTime.Compare(m.NgayPhat, ngayCanTim) <= 0);
            decimal tienConThieu = 0;
            if (listTienConThieu.Count()!=0)
            {
                tienConThieu = listTienConThieu.Select(m => m.SLPhat).Sum()*10000;
            }
            decimal tienTaiKhoan = tienDaTra - tienConThieu;
            return tienTaiKhoan;
        }
    }
}
