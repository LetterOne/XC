using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Tamir.SharpSsh.jsch;

namespace XC.Library
{
    /// <summary>
    /// Shell脚本输入
    /// </summary>
    class ShellHelper
    {

        System.IO.MemoryStream outputstream = new MemoryStream();
        Tamir.SharpSsh.SshStream inputstream = null;
        //Channel channel = null;
        //Session session = null;
        /// <summary> 
        /// 命令等待标识 
        /// </summary> 
        string waitMark = "]#";

        /// <summary> 
        /// 打开连接 
        /// </summary> 
        /// <paramname="host"></param> 
        /// <paramname="username"></param> 
        /// <paramname="pwd"></param> 
        /// <returns></returns> 
        public bool OpenShell(string host, string username, string pwd)
        {
            try
            {
                ////Redirect standard I/O to the SSHchannel 
                inputstream = new Tamir.SharpSsh.SshStream(host, username, pwd);
                ///我手动加进去的方法。。为了读取输出信息 
                //inputstream.se(outputstream);
                return inputstream != null;
            }
            catch { throw; }

        }

        /// <summary> 
        /// 执行命令 
        /// </summary> 
        /// <paramname="cmd"></param> 
        public bool Shell(string cmd)
        {
            if (inputstream == null) return false;
            string initinfo = GetAllString();
            inputstream.Write(cmd);
            inputstream.Flush();
            string currentinfo = GetAllString();
            while (currentinfo == initinfo)
            {
                System.Threading.Thread.Sleep(100);
                currentinfo = GetAllString();
            }
            return true;
        }

        /// <summary> 
        /// 获取输出信息 
        /// </summary> 
        /// <returns></returns> 
        public string GetAllString()
        {
            string outinfo = Encoding.UTF8.GetString(outputstream.ToArray());
            //等待命令结束字符 
            while (!outinfo.Trim().EndsWith(waitMark))
            {
                System.Threading.Thread.Sleep(200);
                outinfo = Encoding.UTF8.GetString(outputstream.ToArray());
            }
            outputstream.Flush();
            return outinfo.ToString();
        }

        /// <summary> 
        /// 关闭连接 
        /// </summary> 
        public void Close()
        {
            if (inputstream != null) inputstream.Close();
        }
    }
}
