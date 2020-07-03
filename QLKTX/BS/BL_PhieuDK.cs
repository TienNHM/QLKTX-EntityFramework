using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLKTX.BS
{
    public partial class BS_layer
    {
        public bool Insert(string MSSV, string MaNV, string Khu, string MaPhong, string HocKi, string NamHoc,
            DateTime NgayGioDK, int ThoiHan, DateTime NgayBD, ref int identity, ref string error)
        {
            
            try
            {
                PHIEUDK pdk = new PHIEUDK()
                {
                    MSSV = MSSV,
                    MaNV = MaNV,
                    Khu = Khu,
                    MaPhong = MaPhong,
                    HocKi = HocKi,
                    NamHoc = NamHoc,
                    NgayGioDK = NgayGioDK,
                    ThoiHan = (byte)ThoiHan,
                    NgayBD = NgayBD,
                };
                entities.PHIEUDKs.Add(pdk);
                entities.SaveChanges();
                error = "";
                identity = pdk.MaPDK;
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool Update(int MaPDK, string MSSV, string MaNV, string Khu, string MaPhong, string HocKi, string NamHoc, 
            DateTime NgayGioDK, int ThoiHan, DateTime NgayBD, ref string error)
        {
            
            try
            {
                var q = (from pdk in entities.PHIEUDKs
                         where pdk.MaPDK == MaPDK
                         select pdk).SingleOrDefault();
                if (q != null)
                {
                    q.MSSV = MSSV;
                    q.MaNV = MaNV;
                    q.Khu = Khu;
                    q.MaPhong = MaPhong;
                    q.HocKi = HocKi;
                    q.NamHoc = NamHoc;
                    q.NgayGioDK = NgayGioDK;
                    q.ThoiHan = (byte)ThoiHan;
                    q.NgayBD = NgayBD;

                    entities.SaveChanges();
                }
                error = "";
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public DataTable LayDSPhongTrong(ref string error)
        {
            
            try
            {
                var q = entities.DS_PHONG_TRONG();
                DataTable dt = new DataTable();
                dt.Columns.Add("Khu");
                dt.Columns.Add("MaPhong");
                foreach (var item in q)
                    dt.Rows.Add(item.Khu, item.MaPhong);
                error = "";
                return dt;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }

        private DataTable GetData(IQueryable<PHIEUDK> danhSach)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaPDK");
            dt.Columns.Add("MSSV");
            dt.Columns.Add("MaNV");
            dt.Columns.Add("Khu");
            dt.Columns.Add("MaPhong");
            dt.Columns.Add("HocKi");
            dt.Columns.Add("NamHoc");
            dt.Columns.Add("NgayGioDK");
            dt.Columns.Add("ThoiHan");
            dt.Columns.Add("NgayBD");

            foreach (var item in danhSach)
                dt.Rows.Add(item.MaPDK, item.MSSV, item.MaNV, item.Khu, item.MaPhong, item.HocKi, item.NamHoc, item.NgayGioDK, item.ThoiHan, item.NgayBD);

            return dt;
        }

        private DataTable Select_PhieuDK(EnumConst.PhieuDK type, string key, ref string error)
        {
            try
            {
                IQueryable<PHIEUDK> danhSach;

                switch (type)
                {
                    case EnumConst.PhieuDK.MaPDK:
                        int pdk = Convert.ToInt32(key);
                        danhSach = from nv in entities.PHIEUDKs
                                   where nv.MaPDK == pdk
                                   select nv;
                        break;
                    case EnumConst.PhieuDK.MSSV:
                        danhSach = from nv in entities.PHIEUDKs
                                   where nv.MSSV.Contains(key)
                                   select nv;
                        break;
                    case EnumConst.PhieuDK.MaNV:
                        danhSach = from nv in entities.PHIEUDKs
                                   where nv.MaNV.Contains(key)
                                   select nv;
                        break;
                    case EnumConst.PhieuDK.HocKi:
                        danhSach = from nv in entities.PHIEUDKs
                                   where nv.HocKi.Contains(key)
                                   select nv;
                        break;
                    case EnumConst.PhieuDK.NamHoc:
                        danhSach = from nv in entities.PHIEUDKs
                                   where nv.NamHoc.Contains(key)
                                   select nv;
                        break;
                    case EnumConst.PhieuDK.All:
                    default:
                        danhSach = from nv in entities.PHIEUDKs
                                   select nv;
                        break;
                }
                try
                {
                    error = "";
                    return GetData(danhSach);
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    return null;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }

        private DataTable Select_PhieuDK(string Khu, string MaPhong, ref string error)
        {
            try
            {
                var q = from pdk in entities.PHIEUDKs
                        where pdk.Khu == Khu && pdk.MaPhong == MaPhong
                        select pdk;
                try
                {
                    error = "";
                    return GetData(q);
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    return null;
                }
            }
            catch(Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }

        private bool Delete_PhieuDK(string MaPDK, ref string error)
        {
            try
            {
                int pdk = Convert.ToInt32(MaPDK);
                PHIEUDK PhieuDK = new PHIEUDK() { MaPDK = pdk };
                entities.PHIEUDKs.Attach(PhieuDK);
                entities.PHIEUDKs.Remove(PhieuDK);
                entities.SaveChanges();
                error = "";
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}
