using System;
using System.Data;
using System.Linq;

namespace QLKTX.BS
{
    public partial class BS_layer
    {
        public bool Insert(string MSSV, string MaLop, string DienSV, string HoTen, bool Phai,
                            DateTime NgSinh, string CMND, string Email, string SDT, string BHYT,
                            string QueQuan, string AnhChanDung, ref string error)
        {
            
            try
            {
                SINHVIEN sv = new SINHVIEN()
                {
                    MSSV = MSSV,
                    MaLop = MaLop,
                    DienSV = DienSV,
                    HoTen = HoTen,
                    Phai = Phai,
                    NgSinh = NgSinh,
                    CMND = CMND,
                    Email = Email,
                    SDT = SDT,
                    BHYT = BHYT,
                    QueQuan = QueQuan,
                    AnhChanDung = AnhChanDung
                };

                entities.SINHVIENs.Add(sv);
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


        private bool Delete_SinhVien(string MSSV, ref string error)
        {
            
            try
            {
                SINHVIEN sv = new SINHVIEN() { MSSV = MSSV };
                entities.SINHVIENs.Attach(sv);
                entities.SINHVIENs.Remove(sv);
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

        public bool Update(string MSSV, string MaLop, string DienSV, string HoTen, bool Phai,
                            DateTime NgSinh, string CMND, string Email, string SDT, string BHYT,
                            string QueQuan, string AnhChanDung, ref string error)
        {
            
            try
            {
                var q = (from sv in entities.SINHVIENs
                         where sv.MSSV == MSSV
                         select sv).SingleOrDefault();
                if (q != null)
                {
                    q.MaLop = MaLop;
                    q.DienSV = DienSV;
                    q.HoTen = HoTen;
                    q.Phai = Phai;
                    q.NgSinh = NgSinh;
                    q.CMND = CMND;
                    q.Email = Email;
                    q.SDT = SDT;
                    q.BHYT = BHYT;
                    q.QueQuan = QueQuan;
                    q.AnhChanDung = AnhChanDung;

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

        private DataTable Select_SinhVien(EnumConst.SinhVien type, string key, ref string error)
        {
            try
            {
                DataTable dt = new DataTable();
                IQueryable<SINHVIEN> danhSach;

                switch (type)
                {
                    case EnumConst.SinhVien.MSSV:
                        danhSach = from sv in entities.SINHVIENs
                                   where sv.MSSV.Contains(key)
                                   select sv;
                        break;
                    case EnumConst.SinhVien.MaLop:
                        danhSach = from sv in entities.SINHVIENs
                                   where sv.MaLop.Contains(key)
                                   select sv;
                        break;
                    case EnumConst.SinhVien.HoTen:
                        danhSach = from sv in entities.SINHVIENs
                                   where sv.HoTen.Contains(key)
                                   select sv;
                        break;
                    case EnumConst.SinhVien.CMND:
                        danhSach = from sv in entities.SINHVIENs
                                   where sv.CMND.Contains(key)
                                   select sv;
                        break;
                    case EnumConst.SinhVien.Email:
                        danhSach = from sv in entities.SINHVIENs
                                   where sv.Email.Contains(key)
                                   select sv;
                        break;
                    case EnumConst.SinhVien.SDT:
                        danhSach = from sv in entities.SINHVIENs
                                   where sv.SDT.Contains(key)
                                   select sv;
                        break;
                    case EnumConst.SinhVien.QueQuan:
                        danhSach = from sv in entities.SINHVIENs
                                   where sv.QueQuan.Contains(key)
                                   select sv;
                        break;
                    default:
                    case EnumConst.SinhVien.All:
                        danhSach = from sv in entities.SINHVIENs
                                   select sv;
                        break;
                }

                #region Đổ dữ liệu lên dt
                dt.Columns.Add("MSSV");
                dt.Columns.Add("MaLop");
                dt.Columns.Add("DienSV");
                dt.Columns.Add("HoTen");
                dt.Columns.Add("Phai");
                dt.Columns.Add("NgSinh");
                dt.Columns.Add("CMND");
                dt.Columns.Add("Email");
                dt.Columns.Add("SDT");
                dt.Columns.Add("BHYT");
                dt.Columns.Add("QueQuan");
                dt.Columns.Add("AnhChanDung");

                foreach (var item in danhSach)
                    dt.Rows.Add(item.MSSV, item.MaLop, item.DienSV, item.HoTen, item.Phai, item.NgSinh, item.CMND, 
                        item.Email, item.SDT, item.BHYT, item.QueQuan, item.AnhChanDung);

                #endregion

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