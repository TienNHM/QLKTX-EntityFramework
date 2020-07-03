using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace QLKTX.BS
{
    public partial class BS_layer
    {
        public bool Insert(ref string error, string MSSV, string MaPhong, string Khu)
        {
            try
            {
                var q = (from sv in entities.SINHVIENs
                         where sv.MSSV == MSSV
                         select sv).SingleOrDefault();
                if (q != null)
                {
                    q.PHONG = new PHONG()
                    {
                        MaPhong = MaPhong,
                        Khu = Khu
                    };
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

        public  bool Update(ref string error, string MSSV, string MaPhong, string Khu)
        {
            try
            {
                var q = (from sv in entities.SINHVIENs
                         where sv.MSSV == MSSV
                         select sv).FirstOrDefault();
                if (q != null)
                {
                    q.PHONG.MaPhong = MaPhong;
                    q.PHONG.Khu = Khu;
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

        private DataTable Select_Stay(ref string error, string MSSV)
        {
            try
            {
                var q = (from sv in entities.SINHVIENs
                         where sv.MSSV == MSSV
                         select sv).SingleOrDefault();
                DataTable dt = new DataTable();
                dt.Columns.Add("MSSV");
                dt.Columns.Add("Khu");
                dt.Columns.Add("MaPhong");
                dt.Rows.Add(q.MSSV, q.PHONG.Khu, q.PHONG.MaPhong);
                error = "";
                return dt;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }

        public DataTable Select_DSSV_Stay(ref string error, string Khu, string MaPhong)
        {
            try
            {
                var q = from sv in entities.SINHVIENs
                         where sv.PHONG.Khu == Khu && sv.PHONG.MaPhong == MaPhong
                         select sv;
                DataTable dt = new DataTable();
                dt.Columns.Add("MSSV");
                dt.Columns.Add("Khu");
                dt.Columns.Add("MaPhong");
                foreach (var item in q)
                    dt.Rows.Add(item.MSSV, item.PHONG.Khu, item.PHONG.MaPhong);
                error = "";
                return dt;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }

        private bool Delete_Stay(string MSSV, ref string error)
        {
            try
            {
                var q = (from sv in entities.SINHVIENs
                         where sv.MSSV == MSSV
                         select sv).SingleOrDefault();
                if (q != null)
                {
                    q.PHONG.Khu = null;
                    q.PHONG.MaPhong = null;
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
    }
}
