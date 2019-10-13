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
            Task task = new Task(()=> {
                if (txtName.Text.ToUpper().Equals("ADMIN")&&txtPWD.Text.Equals("123456"))
                {
                    MessageBox.Show("系统正在登录中");
                    this.Dispose();
                    //FrmMain frm = new FrmMain();
                    //frm.ShowDialog();
                }
            });
            task.Start();
            
        }



        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
