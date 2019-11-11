using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using XC.Win.Base;

namespace XC.Win
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (new FrmLogin().ShowDialog() != DialogResult.OK) return;
            Application.Run(new FrmMain());
        }

        //根据登录验证方式不同，可以选择SAP验证、域账号验证(公司内网)、用户名密码验证等返回用户信息。
       // public static ADUser ADUser = null;用户信息维护

         /// <summary>
         /// 保存验证连接(一般通过WebService或者WebAPI等返回)
         /// </summary>
        public static string ConnectionString = null;

        /// <summary>
        /// 保存验证方案
        /// </summary>
        public static NetworkCredential Cred = null;
    }
}
