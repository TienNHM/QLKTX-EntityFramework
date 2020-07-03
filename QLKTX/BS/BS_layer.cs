using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace QLKTX.BS
{
    public partial class BS_layer
    {
        QUANLYKTXEntities entities = null;

        public BS_layer()
        {
            entities = new QUANLYKTXEntities();
        }

        public enum TableName
        {
            SinhVien,
            NhanVien,
            PhieuDK,
            HoaDon,
            Phong,
            DichVu,
            LoaiPhong,
            SDDV,
            Stay
        }

        public DataTable Select(ref string error, TableName table, string Khu, string MaPhong)
        {
            try
            {
                switch (table)
                {
                    case TableName.HoaDon:
                        return Select_HoaDon(Khu, MaPhong, ref error);
                    case TableName.PhieuDK:
                        return Select_PhieuDK(Khu, MaPhong, ref error);
                    case TableName.Phong:
                        return Select_Phong(Khu, MaPhong, ref error);
                    default:
                        error = "Lỗi Select.";
                        return null;
                }    
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }
        public DataTable Select(ref string error, TableName table, object selectType, string key = "")
        {
            
            try
            {
                DataTable dt = new DataTable();

                switch (table)
                {
                    case TableName.SinhVien:
                        return Select_SinhVien((EnumConst.SinhVien)selectType, key, ref error);
                    case TableName.NhanVien:
                        return Select_NhanVien((EnumConst.NhanVien)selectType, key, ref error);
                    case TableName.PhieuDK:
                        return Select_PhieuDK((EnumConst.PhieuDK)selectType, key, ref error);
                    case TableName.HoaDon:
                        return Select_HoaDon((EnumConst.HoaDon)selectType, key, ref error);
                    case TableName.Phong:
                        return Select_Phong((EnumConst.Phong)selectType, key, ref error);
                    case TableName.DichVu:
                        return Select_DichVu((EnumConst.DichVu)selectType, key, ref error);
                    case TableName.LoaiPhong:
                        return Select_LoaiPhong((EnumConst.LoaiPhong)selectType, key, ref error);
                    case TableName.Stay:
                        return Select_Stay(ref error, key);
                    case TableName.SDDV:
                        return Select_SDDV(ref error, key);
                    default:
                        error = "Lỗi khi Select.";
                        return null;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName">SDDV hoặc Phong</param>
        /// <param name="firstPara">MaHD hoặc Khu</param>
        /// <param name="secondPara">MaDV hoặc MaPhong</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public bool Delete(TableName tableName, string firstPara, string secondPara, ref string error)
        {
            try
            {
                switch (tableName)
                {
                    case TableName.Phong:
                        return Delete_Phong(MaPhong: secondPara, Khu: firstPara, ref error);
                    case TableName.SDDV:
                        return Delete_SDDV(ref error, MaHD: firstPara, MaDV: secondPara);
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

        public bool Delete(TableName tableName, string Key, ref string error)
        {
            try
            {
                switch (tableName)
                {
                    case TableName.SinhVien:
                        return Delete_SinhVien(Key, ref error);
                    case TableName.NhanVien:
                        return Delete_NhanVien(Key, ref error);
                    case TableName.PhieuDK:
                        return Delete_PhieuDK(Key, ref error);
                    case TableName.HoaDon:
                        return Delete_HoaDon(Key, ref error);
                    case TableName.DichVu:
                        return Delete_DichVu(Key, ref error);
                    case TableName.Stay:
                        return Delete_Stay(Key, ref error);
                    default:
                        error = "Lỗi khi Delete.";
                        return false;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}
