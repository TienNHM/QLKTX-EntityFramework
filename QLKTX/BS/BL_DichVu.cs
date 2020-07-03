using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLKTX.BS
{
    public partial class BS_layer
    {
        public bool Insert(string MaDV, string TenDV, int GiaDV, string DonViTinh, ref string error)
        {
            
            try
            {
                DICHVU dv = new DICHVU()
                {
                    MaDV = Convert.ToInt32(MaDV),
                    TenDV = TenDV,
                    GiaDV = GiaDV,
                    DonViTinh = DonViTinh
                };
                entities.DICHVUs.Add(dv);
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

        public bool Update(string MaDV, string TenDV, int GiaDV, string DonViTinh, ref string error)
        {
            
            try
            {
                int madv = Convert.ToInt32(MaDV);
                var q = (from dv in entities.DICHVUs
                         where dv.MaDV == madv
                         select dv).SingleOrDefault();
                if (q != null)
                {
                    q.TenDV = TenDV;
                    q.GiaDV = GiaDV;
                    q.DonViTinh = DonViTinh;
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

        private DataTable Select_DichVu(EnumConst.DichVu type, string key, ref string error)
        {
            try
            {
                DataTable dt = new DataTable();
                IQueryable<DICHVU> q;
                switch (type)
                {
                    case EnumConst.DichVu.MaDV:
                        int madv = Convert.ToInt32(key);
                        q = from dv in entities.DICHVUs
                            where dv.MaDV == madv
                            select dv;
                        break;
                    case EnumConst.DichVu.TenDV:
                        q = from dv in entities.DICHVUs
                            where dv.TenDV.Contains(key)
                            select dv;
                        break;
                    case EnumConst.DichVu.All:
                    default:
                        q = from dv in entities.DICHVUs
                            select dv;
                        break;
                }

                #region Đổ dữ liệu lên dt
                dt.Columns.Add("MaDV");
                dt.Columns.Add("TenDV");
                dt.Columns.Add("GiaDV");
                dt.Columns.Add("DonViTinh");

                foreach (var item in q)
                    dt.Rows.Add(item.MaDV, item.TenDV, item.GiaDV, item.DonViTinh);
                #endregion

                error = "";
                return dt;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }

        private bool Delete_DichVu(string MaDV, ref string error)
        {
            try
            {
                DICHVU dv = new DICHVU() { MaDV = Convert.ToInt32(MaDV) };
                entities.DICHVUs.Attach(dv);
                entities.DICHVUs.Remove(dv);
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
