using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLKTX.BS;

namespace QLKTX.UI
{
    public partial class FrmDangNhap : Form
    {
        public static bool exit = false;
        private string error = "";
        BL_DangNhap dangNhap = new BL_DangNhap();
        BL_DangNhap.AccountType accountType = BL_DangNhap.AccountType.Employee;
        public FrmDangNhap()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string MaNV = "";
            bool result = dangNhap.Select(txtUserName.Text.Trim(), txtPassword.Text.Trim(), ref MaNV, ref accountType, ref error);

            if (result)
            {
                FrmMain main = new FrmMain(MaNV, accountType);
                this.Hide();
                main.ShowDialog();
                if (exit == false)
                {
                    this.Show();
                    txtUserName.Clear();
                    txtPassword.Clear();
                    txtUserName.Focus();
                }
            }
            else
            {
                MessageBox.Show("Thông tin đăng nhập không chính xác. Mời nhập lại!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserName.Clear();
                txtPassword.Clear();
                txtUserName.Focus();
            }    
        }
    }
}
