using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace XC.Library.Utils
{
    public sealed class AES
    {
        /// <summary>
        /// AES加密偏移量，必须是>=8位长的字符串
        /// </summary>
        public static string IV { get; set; } = "1234567890123456";

        /// <summary>
        /// AES加密的私钥，必须是8位长的字符串
        /// </summary>
        public static string Key { get; set; } = "12345678901234567890123456789012";

        #region 加密
        /// <summary>
        /// 加密流
        /// </summary>
        /// <param name="ms"></param>
        public static void Encrypt(byte[] inData, Stream outStream)
        {
            byte[] btKey = Encoding.Default.GetBytes(Key);
            byte[] btIV = Encoding.Default.GetBytes(IV);
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            RC2CryptoServiceProvider ff = new RC2CryptoServiceProvider();
            CryptoStream cs = new CryptoStream(outStream, aes.CreateEncryptor(btKey, btIV), CryptoStreamMode.Write);
            cs.Write(inData, 0, inData.Length);
            cs.FlushFinalBlock();
        }


        /// <summary>
        /// 对字符串进行DES加密
        /// </summary>
        /// <param name="sourceString">待加密的字符串</param>
        /// <returns>加密后的BASE64编码的字符串</returns>
        public static string EncryptString(string sourceString)
        {
            byte[] inData = Encoding.Default.GetBytes(sourceString);
            MemoryStream ms = new MemoryStream();
            try
            {
                Encrypt(inData, ms);
                string str = Convert.ToBase64String(ms.ToArray());
                ms.Close();
                return str;
            }
            catch
            {
                ms.Close();
                return "";
            }
        }

        /// <summary>
        /// 对文件内容进行DES加密
        /// </summary>
        /// <param name="sourceFile">待加密的文件绝对路径</param>
        /// <param name="destFile">加密后的文件保存的绝对路径</param>
        public static void EncryptFile(string sourceFile, string destFile)
        {
            if (!File.Exists(sourceFile)) throw new FileNotFoundException("指定的文件路径不存在！", sourceFile);
            byte[] inData = File.ReadAllBytes(sourceFile);
            FileStream fs = new FileStream(destFile, FileMode.Create, FileAccess.Write);
            try
            {
                Encrypt(inData, fs);
            }
            finally
            {
                fs.Close();
            }
        }

        /// <summary>
        /// 对文件内容进行AES加密，加密后覆盖掉原来的文件
        /// </summary>
        /// <param name="sourceFile">待加密的文件的绝对路径</param>
        public static void EncryptFile(string sourceFile)
        {
            EncryptFile(sourceFile, sourceFile);
        }
        #endregion

        #region 解密
        /// <summary>
        /// 解密流
        /// </summary>
        /// <param name="ms"></param>
        public static void Decrypt(byte[] inData, Stream outStream)
        {
            byte[] btKey = Encoding.Default.GetBytes(Key);
            byte[] btIV = Encoding.Default.GetBytes(IV);
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();

            Stream ms = outStream;
            CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(btKey, btIV), CryptoStreamMode.Write);
            cs.Write(inData, 0, inData.Length);
            cs.FlushFinalBlock();
        }

        /// <summary>
        /// 对DES加密后的字符串进行解密
        /// </summary>
        /// <param name="encryptedString">待解密的字符串</param>
        /// <returns>解密后的字符串</returns>
        public static string DecryptString(string encryptedString)
        {
            byte[] inData = Convert.FromBase64String(encryptedString);
            MemoryStream ms = new MemoryStream();
            try
            {
                Decrypt(inData, ms);
                string str = Encoding.Default.GetString(ms.ToArray());
                ms.Close();
                return str;
            }
            catch (Exception)
            {
                ms.Close();
                return "";
            }

        }

        /// <summary>
        /// 对文件内容进行DES解密
        /// </summary>
        /// <param name="sourceFile">待解密的文件绝对路径</param>
        /// <param name="destFile">解密后的文件保存的绝对路径</param>
        public static void DecryptFile(string sourceFile, string destFile)
        {
            if (!File.Exists(sourceFile)) throw new FileNotFoundException("指定的文件路径不存在！", sourceFile);
            byte[] inData = File.ReadAllBytes(sourceFile);
            FileStream fs = new FileStream(destFile, FileMode.Create, FileAccess.Write);
            try
            {
                Decrypt(inData, fs);
            }
            catch (Exception)
            {
            }
            fs.Close();
        }

        /// <summary>
        /// 对文件内容进行DES解密，加密后覆盖掉原来的文件
        /// </summary>
        /// <param name="sourceFile">待解密的文件的绝对路径</param>
        public static void DecryptFile(string sourceFile)
        {
            DecryptFile(sourceFile, sourceFile);
        }
        #endregion
    }
}
