using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace XC.Library
{
   public class CharToCode
    {
        #region 字符转码函数

        /// <summary>
        /// Hexbin：字符的ASCII的二进制数据表示形式， 例 字符'1'的hex是0x31表示为hexbin是 '3''1' ，GetBytes为0x：33 31即00110011,00110001 
        /// Hex：字符的直接二进制数据（例，‘1’，byte为00110001）</summary>
        /// 实际是这样的：stirng s="0102",发送时会发送30313032 ，转换后发送01 02即以字节方式发送信息 <param name="bHexbin"></param>
        /// string转byte  
        /// <param name="bHex"></param>
        /// <param name="nLen"></param>
        public static void Hexbin2Hex(byte[] bHexbin, byte[] bHex, int nLen)
        {
            for (int i = 0; i < nLen / 2; i++)
            {
                if (bHexbin[2 * i] < 0x41)
                {
                    bHex[i] = Convert.ToByte(((bHexbin[2 * i] - 0x30) << 4) & 0xf0);
                }
                else
                {
                    bHex[i] = Convert.ToByte(((bHexbin[2 * i] - 0x37) << 4) & 0xf0);
                }

                if (bHexbin[2 * i + 1] < 0x41)
                {
                    bHex[i] |= Convert.ToByte((bHexbin[2 * i + 1] - 0x30) & 0x0f);
                }
                else
                {
                    bHex[i] |= Convert.ToByte((bHexbin[2 * i + 1] - 0x37) & 0x0f);
                }
            }
        }

        /// <summary>
        /// 上一函数的逆过程
        /// byte转string
        /// </summary> 
        public static void Hex2Hexbin(byte[] bHex, byte[] bHexbin, int nLen)
        {
            byte c;
            for (int i = 0; i < nLen; i++)
            {
                c = Convert.ToByte((bHex[i] >> 4) & 0x0f);
                if (c < 0x0a)
                {
                    bHexbin[2 * i] = Convert.ToByte(c + 0x30);
                }
                else
                {
                    bHexbin[2 * i] = Convert.ToByte(c + 0x37);
                }
                c = Convert.ToByte(bHex[i] & 0x0f);
                if (c < 0x0a)
                {
                    bHexbin[2 * i + 1] = Convert.ToByte(c + 0x30);
                }
                else
                {
                    bHexbin[2 * i + 1] = Convert.ToByte(c + 0x37);
                }
            }
        }

        /// <summary>
        /// 字符串反序
        /// </summary>
        /// <param name="str"></param>
        public static String String_Reorder(String str)
        {
            int strnum;
            string newstr = "";
            strnum = str.Length;
            for (int i = strnum / 2; i > 0; i--)
            {
                newstr += str.Substring((i - 1) * 2, 2);
            }
            return newstr;
        }

        /// 汉字/英文字符转16进制
        /// </summary>
        /// <param name="Str">string字符串</param>
        /// <param name="Charset">编码：如UTP-8，GB2312</param>
        /// <param name="Divide">是否每字符用逗号分隔</param>
        /// <returns></returns>
        public static string ChnToHex(string Str, string Charset, bool Divide)
        {
            //if ((Str.Length % 2) != 0)
            //{
            //    Str += " ";//空格
            //}
            System.Text.Encoding chs = System.Text.Encoding.GetEncoding(Charset);
            byte[] bytes = chs.GetBytes(Str);
            string str = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                str += string.Format("{0:X}", bytes[i]);
                if (Divide && (i != bytes.Length - 1))
                {
                    str += string.Format("{0}", ",");
                }
            }
            return str.ToUpper();
        }


        /// <summary>
        /// 生成文件的MD5校验码
        /// </summary>
        /// <param name="FileContent">文件内容byte[]型</param>
        /// <returns></returns>
        public static string GetMD5HashFromByte(byte[] FileContent)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(FileContent);
            ASCIIEncoding Enc = new ASCIIEncoding();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("X2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 生成MD5校验码
        /// </summary>
        /// <param name="FileContent">生成MD5的string字符串</param>
        /// <returns></returns>
        public static string GetMD5HashFromByte(string FileContent)
        {
            byte[] FileData = Encoding.ASCII.GetBytes(FileContent);
            int ByteLength = FileData.Length;
            if (ByteLength % 2 != 0) return null;
            byte[] bHex = new byte[ByteLength / 2];
            //将ASCII码字节数组（bs）转为直接的二进制字节数组（bHex）:[43][45] -> [ce] 
            Hexbin2Hex(FileData, bHex, ByteLength);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(bHex);
            ASCIIEncoding Enc = new ASCIIEncoding();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("X2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 生成文件的MD5校验码
        /// </summary>
        /// <param name="FileContent">文件内容byte[]型</param>
        /// <returns></returns>
        public static string GetMD5HashFromStr(string FileContent)
        {
            byte[] FileData = Encoding.ASCII.GetBytes(FileContent);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(FileData);
            ASCIIEncoding Enc = new ASCIIEncoding();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("X2"));
            }
            return sb.ToString();
        }


        /// <summary>
        /// IP地址转为Int型
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <returns></returns>
        public static int IpToInt(string ip)
        {
            char[] separator = new char[] { '.' };
            string[] items = ip.Split(separator);
            return int.Parse(items[0]) << 24
                    | int.Parse(items[1]) << 16
                    | int.Parse(items[2]) << 8
                    | int.Parse(items[3]);
        }
        #endregion
}
}
