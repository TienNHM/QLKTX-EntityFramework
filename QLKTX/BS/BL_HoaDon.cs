using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace QLKTX.BS
{
    public partial class BS_layer
    {   
        private DataTable GetData(IQueryable<HOADON> q)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaHD");
            dt.Columns.Add("Nam");
            dt.Columns.Add("Thang");
            dt.Columns.Add("NgayHD");
            dt.Columns.Add("MaNV");
            dt.Columns.Add("MaPhong");
            dt.Columns.Add("Khu");
            foreach (var item in q)
                dt.Rows.Add(item.MaHD, item.Nam, item.Thang, item.NgayHD, item.MaNV, item.MaPhong, item.Khu);
            return dt;
        }

        /// <summary>
        /// Tìm danh sách các hóa đơn của khu phòng
        /// </summary>
        /// <param name="error">Chứa thông báo lỗi</param>
        /// <returns></returns>
        public DataTable Select_HoaDon(string Khu, string MaPhong, ref string error)
        {
            try
            {
                var q = from hd in entities.HOADONs
                        where hd.Khu == Khu && hd.MaPhong == MaPhong
                        select hd;
            
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
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }

        private DataTable Select_HoaDon(EnumConst.HoaDon type, string key, ref string error)
        {
             try
             {
                DataTable dt = new DataTable();
                IQueryable<HOADON> q;

                switch (type)
                {
                    case EnumConst.HoaDon.MaHD:
                        int hd = Convert.ToInt32(key);
                        q = from nv in entities.HOADONs
                                   where nv.MaHD == hd
                                   select nv;
                        break;
                    case EnumConst.HoaDon.All:
                    default:
                        q = from nv in entities.HOADONs
                                   select nv;
                        break;
                }

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
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }

        public bool Insert(ref string error, string Nam, string Thang, DateTime NgayHD, string MaNV, string MaPhong, string Khu, ref int identity)
        {
            
            try
            {
                HOADON hoaDon = new HOADON()
                {
                    Nam = Nam,
                    Thang = Thang,
                    NgayHD = NgayHD,
                    MaNV = MaNV,
                    MaPhong = MaPhong,
                    Khu = Khu
                };
                entities.HOADONs.Add(hoaDon);
                entities.SaveChanges();
                identity = hoaDon.MaHD;
                error = "";
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool Update(ref string error, string MaHD, string Nam, string Thang, DateTime NgayHD, string MaNV, string MaPhong, string Khu)
        {
            
            try
            {
                int id = Convert.ToInt32(MaHD);
                var q = (from hd in entities.HOADONs
                         where hd.MaHD == id
                         select hd).SingleOrDefault();
                if (q != null)
                {
                    q.Nam = Nam;
                    q.Thang = Thang;
                    q.NgayHD = NgayHD;
                    q.MaNV = MaNV;
                    q.MaPhong = MaPhong;
                    q.Khu = Khu;
                    
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

        private bool Delete_HoaDon(string MaHD, ref string error)
        {
            try
            {
                HOADON nv = new HOADON() { MaHD = Convert.ToInt32(MaHD) };
                entities.HOADONs.Attach(nv);
                entities.HOADONs.Remove(nv);
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
