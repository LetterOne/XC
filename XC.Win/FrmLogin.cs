using System
    ;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XC.Win.Base;

namespace XC.Win
{
    public partial class FrmLogin : FrmBase
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnOldaSyc_Click(object sender, EventArgs e)
        {
//#if DEBUG
//            this.DialogResult = DialogResult.OK;
//            return;
//#endif

            this.Enabled = false;
            btnLogin.Text = "登录中";
            Application.DoEvents();
            //此处写登录判断逻辑，可调用RPC或其他登录验证方式，这里写个简单判断，用Admin登录
            try
            {

                if (txtName.Text.Trim().ToUpper().Equals("ADMIN") && txtPWD.Text.Equals("123456"))
                {
                    btnLogin.Text = "登录成功";
                    this.DialogResult = DialogResult.OK;
                }
                else if(string.IsNullOrEmpty(txtName.Text)||string.IsNullOrEmpty(txtPWD.Text))
                {
                    MessageBox.Show("用户名或密码不能为空");
                    return;
                }
            }
            catch (Exception ex)
            {
                // 如果用户名或者密码不正确，也会抛出异常。
                MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            finally
            {
                this.Enabled = true;
                btnLogin.Text = "登录";
                Application.DoEvents();
            }

        }



        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
