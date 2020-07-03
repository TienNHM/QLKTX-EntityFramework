using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;
using QLKTX.UI;

namespace QLKTX.BS
{
    public class BL_DangNhap
    {
        public enum AccountType
        {
            Admin,
            Employee,
        }

        public bool Select(string TenDN, string MatKhau, ref string MaNV, ref AccountType accountType, ref string error)
        {

            QUANLYKTXEntities entities = new QUANLYKTXEntities();
            try
            {
                var q = (from user in entities.DANGNHAPs
                         where user.TenDN == TenDN && user.MatKhau == MatKhau
                         select user).SingleOrDefault();
                
                if (q != null)
                {
                    MaNV = q.MaNV;
                    accountType = (q.NHANVIEN.MaNQL == "") ? AccountType.Admin : AccountType.Employee;
                    error = "";
                    return true;
                }
                else
                {
                    error = "Sai thông tin đăng nhập";
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
