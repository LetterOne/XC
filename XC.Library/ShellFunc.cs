using System;
using System.Collections.Generic;
using System.Text;

namespace XC.Library
{
    class ShellFunc
    {
        public static int SSHLogin(string strHost, string strUser, string strPwd, ref string strPush)
        {

            try
            {
                ShellHelper shell = new ShellHelper();
                if (shell.OpenShell(strHost, strUser, strPwd))
                {
                    strPush = "SSH Connect OK!";
                    shell.Close();//关闭连接 
                }
            }
            catch (Exception ex)
            {
                strPush = ex.ToString();
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// 传入Shell命令脚本
        /// </summary>
        /// <param name="List">Shell命令集</param>
        /// <param name="strHost">主机地址</param>
        /// <param name="strUser">用户名</param>
        /// <param name="strPwd">密码</param>
        /// <param name="strPush">返回信息</param>
        /// <returns></returns>
       public static int SSHCommands(List<string> List, string strHost, string strUser, string strPwd, ref string strPush)
        {
            try
            {
                strPush = "";
                ShellHelper shell = new ShellHelper();
                //连接linux成功 
                if (shell.OpenShell(strHost, strUser, strPwd))
                {
                    foreach (string list in List)
                    {
                        shell.Shell(list);//执行获取命令  
                        System.Threading.Thread.Sleep(200);
                    }
                    strPush = shell.GetAllString();//获取返回结果 
                    shell.Close();//关闭连接 
                }
            }
            catch (Exception ex)
            {
                strPush = ex.ToString();
                return -1;
            }
            return 0;
        }
    }
}
