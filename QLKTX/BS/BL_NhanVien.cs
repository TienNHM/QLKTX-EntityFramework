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
        public bool Insert(string MaNV, string CMND, string HoTen, string SDT, string Email, string DiaChi, int Luong, string MaNQL, string avt, ref string error)
        {
            
            try
            {
                NHANVIEN nv = new NHANVIEN()
                {
                    MaNV = MaNV,
                    CMND = CMND,
                    HoTen = HoTen,
                    SDT = SDT,
                    Email = Email,
                    DiaChi = DiaChi,
                    Luong = Luong,
                    MaNQL = MaNQL,
                    AnhChanDung = avt
                };
                entities.NHANVIENs.Add(nv);
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

        private bool Delete_NhanVien(string MaNV, ref string error)
        {
            
            try
            {
                NHANVIEN nv = new NHANVIEN() { MaNV = MaNV };
                entities.NHANVIENs.Attach(nv);
                entities.NHANVIENs.Remove(nv);
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

        public bool Update(string MaNV, string CMND, string HoTen, string SDT, string Email, string DiaChi, int Luong, string MaNQL, string avt, ref string error)
        {
            
            try
            {
                var q = (from nv in entities.NHANVIENs
                                where nv.MaNV == MaNV
                                select nv).FirstOrDefault();
                if (q != null)
                {
                    q.CMND = CMND;
                    q.HoTen = HoTen;
                    q.SDT = SDT;
                    q.Email = Email;
                    q.DiaChi = DiaChi;
                    q.Luong = Luong;
                    q.MaNQL = MaNQL;
                    q.AnhChanDung = avt;

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

        private DataTable Select_NhanVien(EnumConst.NhanVien type, string key, ref string error)
        {
            
            try
            {
                DataTable dt = new DataTable();

                IQueryable<NHANVIEN> danhSach;

                switch (type)
                {
                    case EnumConst.NhanVien.MaNV:
                        danhSach = from nv in entities.NHANVIENs
                                   where nv.MaNV.Contains(key)
                                   select nv;
                        break;
                    case EnumConst.NhanVien.CMND:
                        danhSach = from nv in entities.NHANVIENs
                                   where nv.CMND.Contains(key)
                                   select nv;
                        break;
                    case EnumConst.NhanVien.HoTen:
                        danhSach = from nv in entities.NHANVIENs
                                   where nv.HoTen.Contains(key)
                                   select nv;
                        break;
                    case EnumConst.NhanVien.SDT:
                        danhSach = from nv in entities.NHANVIENs
                                   where nv.SDT.Contains(key)
                                   select nv;
                        break;
                    case EnumConst.NhanVien.Email:
                        danhSach = from nv in entities.NHANVIENs
                                   where nv.Email.Contains(key)
                                   select nv;
                        break;
                    case EnumConst.NhanVien.All:
                    default:
                        danhSach = from nv in entities.NHANVIENs
                                   select nv;
                        break;
                }

                #region Đổ dữ liệu lên dt
                dt.Columns.Add("MaNV");
                dt.Columns.Add("CMND");
                dt.Columns.Add("HoTen");
                dt.Columns.Add("SDT");
                dt.Columns.Add("Email");
                dt.Columns.Add("DiaChi");
                dt.Columns.Add("Luong");
                dt.Columns.Add("MaNQL");
                dt.Columns.Add("AnhChanDung");

                foreach (var item in danhSach)
                    dt.Rows.Add(item.MaNV, item.CMND, item.HoTen, item.SDT, item.Email, item.DiaChi, item.Luong, item.MaNQL, item.AnhChanDung);

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

    }
}
