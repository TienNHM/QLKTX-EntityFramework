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
        private DataTable Select_Phong(EnumConst.Phong type, string key, ref string error)
        {
            try
            {
                DataTable dt = new DataTable();
                IQueryable<PHONG> danhSach;
                switch (type)
                {
                    case EnumConst.Phong.Khu:
                        danhSach = from p in entities.PHONGs
                                   where p.Khu == key
                                   select p;
                        break;
                    case EnumConst.Phong.LoaiPhong:
                        danhSach = from p in entities.PHONGs
                                   where p.LoaiPhong == key
                                   select p;
                        break;
                    case EnumConst.Phong.All:
                    default:
                        danhSach = from p in entities.PHONGs
                                   select p;
                        break;
                }

                dt.Columns.Add("Khu");
                dt.Columns.Add("MaPhong");
                dt.Columns.Add("LoaiPhong");

                foreach (var item in danhSach)
                    dt.Rows.Add(item.Khu, item.MaPhong, item.LoaiPhong);
                error = "";
                return dt;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }

        public DataTable Select_DSMaPhong(ref string error, string Khu)
        {
            try
            {
                var q = from phong in entities.PHONGs
                        where phong.Khu == Khu
                        select phong.MaPhong;
                DataTable dt = new DataTable();
                dt.Columns.Add("MaPhong");
                foreach (var item in q)
                    dt.Rows.Add(item);
                error = "";
                return dt;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }

        public DataTable Select_DSKhu(ref string error)
        {
            try
            {
                var q = (from phong in entities.PHONGs
                         select phong.Khu).Distinct();
                DataTable dt = new DataTable();
                dt.Columns.Add("Khu");
                foreach (var item in q)
                    dt.Rows.Add(item);
                error = "";
                return dt;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }
        
        private DataTable Select_Phong(string Khu, string MaPhong, ref string error)
        {
            try
            {
                var q = from phong in entities.PHONGs
                        where phong.Khu == Khu && phong.MaPhong == MaPhong
                        select phong;
                
                DataTable dt = new DataTable();
                dt.Columns.Add("Khu");
                dt.Columns.Add("MaPhong");
                dt.Columns.Add("LoaiPhong");
                foreach (var item in q)
                    dt.Rows.Add(item.Khu, item.MaPhong, item.LoaiPhong);
                error = "";
                return dt;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }

        private DataTable Select_LoaiPhong(EnumConst.LoaiPhong type, string key, ref string error)
        {
            try
            {
                DataTable dt = new DataTable();
                IQueryable<LOAIPHONG> q;
                switch (type)
                {
                    case EnumConst.LoaiPhong.MaLoaiPhong:
                        q = from lp in entities.LOAIPHONGs
                            where lp.MaLoaiPhong == key
                            select lp;
                        break;
                    case EnumConst.LoaiPhong.All:
                    default:
                        q = from lp in entities.LOAIPHONGs
                            select lp;
                        break;
                }
                dt.Columns.Add("MaLoaiPhong");
                dt.Columns.Add("SoSV");
                dt.Columns.Add("DienTich");
                dt.Columns.Add("DonGia");

                foreach (var item in q)
                    dt.Rows.Add(item.MaLoaiPhong, item.SoSV, item.DienTich, item.DonGia);

                error = "";
                return dt;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }

        public bool Insert(string MaLoaiPhong, int SoSV, float DienTich, int DonGia, ref string error)
        {
            
            try
            {
                LOAIPHONG lp = new LOAIPHONG()
                {
                    MaLoaiPhong = MaLoaiPhong,
                    SoSV = (byte)SoSV,
                    DienTich = Convert.ToDecimal(DienTich),
                    DonGia = DonGia
                };
                entities.LOAIPHONGs.Add(lp);
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

        public bool Update(string MaLoaiPhong, int SoSV, float DienTich, int DonGia, ref string error)
        {
            
            try
            {
                var q = (from lp in entities.LOAIPHONGs
                         where lp.MaLoaiPhong == MaLoaiPhong
                         select lp).SingleOrDefault();
                if (q != null)
                {
                    q.SoSV = (byte)SoSV;
                    q.DienTich = Convert.ToDecimal(DienTich);
                    q.DonGia = DonGia;

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

        public DataTable LayThongTinLoaiPhong(ref string error, string MaLoaiPhong)
        {
            
            try
            {
                var q = from lp in entities.LOAIPHONGs
                        join phong in entities.PHONGs
                        on lp.MaLoaiPhong equals phong.LoaiPhong
                        where lp.MaLoaiPhong == MaLoaiPhong
                        select new
                        {
                            lp.MaLoaiPhong,
                            lp.SoSV,
                            lp.DienTich,
                            lp.DonGia,
                            phong.Khu,
                            phong.MaPhong,
                        };
                DataTable dt = new DataTable();
                dt.Columns.Add("MaLoaiPhong");
                dt.Columns.Add("SoSV");
                dt.Columns.Add("DienTich");
                dt.Columns.Add("DonGia");
                dt.Columns.Add("Khu");
                dt.Columns.Add("MaPhong");
                foreach (var item in q)
                    dt.Rows.Add(item.MaLoaiPhong, item.SoSV, item.DienTich, item.DonGia, item.Khu, item.MaPhong);
                error = "";
                return dt;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }

        private bool Delete_Phong(string MaPhong, string Khu, ref string error)
        {
            try
            {
                PHONG phong = new PHONG() { MaPhong = MaPhong, Khu = Khu };
                entities.PHONGs.Attach(phong);
                entities.PHONGs.Remove(phong);
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
