using QLKTX.BS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLKTX.UI
{
    public partial class FrmPhong : Form
    {
        string error = "";
        int row = -1;
        List<string> DSSV = new List<string>();

        public FrmPhong()
        {
            InitializeComponent();
        }

        public FrmPhong(string Khu, string MaPhong)
        {
            InitializeComponent();
            cmbKhu.Text = Khu.Trim();
            cmbKhu.Enabled = false;
            cmbPhong.Text = MaPhong.Trim();
            cmbPhong.Enabled = false;
            cmbLoaiPhong.Enabled = false;
            try
            {
                //Lấy DS sinh viên trong phòng
                var dt = FrmMain.bS_Layer.Select_DSSV_Stay(ref error, cmbKhu.Text.Trim(), cmbPhong.Text.Trim());
                if (error == "")
                {
                    dgv.Rows.Clear();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        //Lấy thông tin sinh viên
                        string mssv = dt.Rows[i]["MSSV"].ToString();
                        var sv = FrmMain.bS_Layer.Select(ref error, BS_layer.TableName.SinhVien, EnumConst.SinhVien.MSSV, mssv);
                        if (error != "") continue;
                        DSSV.Add(mssv);
                        DateTime DoB;
                        try
                        {
                            DoB = DateTime.ParseExact(dt.Rows[0]["NgayBD"].ToString(), "yyyy-MM-dd", CultureInfo.CurrentCulture);
                        }
                        catch 
                        {
                            DoB = DateTime.Now;
                        }
                        
                        string[] str = new string[]
                        {
                        sv.Rows[0]["MSSV"].ToString(),
                        sv.Rows[0]["HoTen"].ToString(),
                        sv.Rows[0]["MaLop"].ToString(),
                        DoB.ToString("yyyy-MM-dd"),
                        sv.Rows[0]["SDT"].ToString(),
                        sv.Rows[0]["QueQuan"].ToString()
                        };
                        dgv.Rows.Add(str);
                    }
                }
                else
                    MessageBox.Show(error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi");
            }

            try
            {
                var loaiPhong = FrmMain.bS_Layer.Select(ref error, BS_layer.TableName.Phong, Khu, MaPhong);
                if (loaiPhong != null)
                {
                    cmbLoaiPhong.Text = loaiPhong.Rows[0]["LoaiPhong"].ToString();
                    var tmp = FrmMain.bS_Layer.LayThongTinLoaiPhong(ref error, cmbLoaiPhong.Text);
                    lbMaxSV.Text = tmp.Rows[0]["SoSV"].ToString();
                }
            }
            catch { }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            cmbLoaiPhong.Enabled = true;
            cmbKhu.Enabled = true;
            cmbPhong.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (row < 0) return;
            if (dgv.Rows[row].IsNewRow) return;
            var re = MessageBox.Show("Bạn có thực sự muốn xóa sinh viên này khỏi phòng không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (re == DialogResult.Yes)
            {
                var dt = FrmMain.bS_Layer.Delete(BS_layer.TableName.Stay, DSSV[row], ref error);
                if (dt == false)
                    MessageBox.Show("Đã xảy ra lỗi trong quá trình xóa sinh viên khỏi phòng! \n" + error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                {
                    DSSV.RemoveAt(row);
                    dgv.Rows.RemoveAt(row);
                }    
            }                
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            row = e.RowIndex;
        }
    }
}
