using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLKTX.BS
{
    public partial class BS_layer
    {
        /// <summary>
        /// Kiểm tra hóa đơn đã có dịch vụ là MaDV chưa
        /// </summary>
        /// <param name="error"></param>
        /// <param name="MaHD"></param>
        /// <param name="MaDV"></param>
        /// <returns>true, nếu MaDV đã có trong hóa đơn</returns>
        public bool Select_SDDV(ref string error, string  MaHD, string  MaDV)
        {
            
            try
            {
                int mahd = Convert.ToInt32(MaHD);
                int madv = Convert.ToInt32(MaDV);
                var count = (from sddv in entities.SDDVs
                         where sddv.MaHD == mahd && sddv.MaDV == madv
                         select sddv).Count();
                if (count > 0)
                {
                    error = "Đã tồn tại";
                    return true;
                }                
                else
                {
                    error = "";
                    return false;
                }    
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        private DataTable Select_SDDV(ref string error, string MaHD)
        {
            try
            {
                int mahd = Convert.ToInt32(MaHD);
                var q = from sddv in entities.SDDVs
                             where sddv.MaHD == mahd
                             select sddv;
                DataTable dt = new DataTable();
                dt.Columns.Add("MaDV");
                dt.Columns.Add("TenDV");
                dt.Columns.Add("SoLuong");
                dt.Columns.Add("DonViTinh");
                foreach (var item in q)
                    dt.Rows.Add(item.MaDV, item.DICHVU.TenDV, item.SoLuong, item.DICHVU.DonViTinh);
                error = "";
                return dt;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }    

        private bool Delete_SDDV(ref string error, string MaHD, string MaDV)
        {
            try
            {
                int mahd = Convert.ToInt32(MaHD);
                int madv = Convert.ToInt32(MaDV);
                SDDV sddv = new SDDV() { MaDV = madv, MaHD = mahd };
                entities.SDDVs.Attach(sddv);
                entities.SDDVs.Remove(sddv);
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

        public bool Insert(ref string error, string  MaHD, string  MaDV, int SoLuong)
        {
            
            try
            {
                SDDV dv = new SDDV()
                {
                    MaHD = Convert.ToInt32(MaHD),
                    MaDV = Convert.ToInt32(MaDV),
                    SoLuong = (byte)SoLuong
                };
                entities.SDDVs.Add(dv);
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

        public bool Update(ref string error, string  MaHD, string MaDV, int SoLuong)
        {
            
            try
            {
                int mahd = Convert.ToInt32(MaHD);
                int madv = Convert.ToInt32(MaDV);
                var q = (from dv in entities.SDDVs
                         where dv.MaHD == mahd && dv.MaDV == madv
                         select dv).SingleOrDefault();
                if (q != null)
                {
                    q.SoLuong = (byte) SoLuong;
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
    }
}
