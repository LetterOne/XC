using Microsoft.IdentityModel.Protocols;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace XC.Library
{
    class DbBaseSQL
    {
        #region 连接数据库的方法

        /// <summary>
        /// 连接数据库的字符串
        /// </summary>
        public static string connectionString = "";// ConfigurationManager.ConnectionStrings["DataBaseConnec"].ConnectionString;
        /// <summary>
        /// 连接数据库的方法
        /// </summary>
        /// <param name="connectionString">连接数据库的字符串</param>
        /// <returns></returns>
        public static OracleConnection GetConn()
        {
            //创建一个数据库链接对象
            OracleConnection oracleCon = new OracleConnection(connectionString);
            return oracleCon;
        }
        #endregion

        #region 更新(增删改)的公共方法

        /// <summary>
        /// 更新(增删改)的公共方法
        /// </summary>
        /// <param name="szOracleString">执行的Oracle语句</param>
        /// <param name="oracleParams">传入的参数</param>
        /// <returns></returns>
        public static bool UpdateMethod(string sOracleString, OracleParameter[] oracleParams)
        {
            //链接数据库
            using (OracleConnection oracleCon = GetConn())
            {
                bool flag = false;
                //实例化一个
                OracleCommand oracleCmd = new OracleCommand(sOracleString, oracleCon);
                //判断参数是否为空
                if (oracleParams != null)
                {
                    //添加参数
                    oracleCmd.Parameters.AddRange(oracleParams);
                }
                try
                {
                    //打开数据库
                    oracleCon.Open();
                    //获取返回受影响的函数
                    int n = oracleCmd.ExecuteNonQuery();
                    if (n > 0)
                    {
                        flag = true;
                    }
                }
                catch (Exception)
                {
                    flag = false;
                }
                finally
                {
                    //关闭数据库
                    oracleCon.Close();
                }
                return flag;
            }
        }
        #endregion

        #region 查询的公共方法

        /// <summary>
        /// 查询的 公共方法（DataTable）
        /// </summary>
        /// <param name="strOracle">执行的Oracle语句</param>
        /// <param name="oracleParams">传入的参数</param>
        /// <returns></returns>
        public static DataTable SelectDtMethod(string strOracle, OracleParameter[] oracleParams)
        {
            using (OracleConnection oracleCon = GetConn())
            {
                //实例化 DataTable
                DataTable dt = new DataTable();
                oracleCon.Open();
                //实例化 SqlDataAdapter
                OracleDataAdapter oda = new OracleDataAdapter(strOracle, oracleCon);
                //判断参数是否为空
                if (oracleParams != null)
                {
                    //添加参数
                    oda.SelectCommand.Parameters.AddRange(oracleParams);
                }
                //填充
                oda.Fill(dt);
                oracleCon.Close();
                return dt;
            }
        }

        /// <summary>
        /// 查询的 公共方法，可以执行多条语句，返回多个数据表的集合（DataSet）
        /// </summary>
        /// <param name="strOracle">执行的Oracle语句</param>
        /// <param name="oracleParams"></param>
        /// <returns></returns>
        public static DataSet SelectDsMethod(string strOracle, OracleParameter[] oracleParams)
        {
            using (OracleConnection oracleCon = GetConn())
            {
                try
                {
                    //实例化 DataTable
                    DataSet ds = new DataSet();
                    oracleCon.Open();
                    //实例化 SqlDataAdapter
                    OracleDataAdapter oda = new OracleDataAdapter(strOracle, oracleCon);
                    //判断参数是否为空
                    if (oracleParams != null)
                    {
                        //添加参数
                        oda.SelectCommand.Parameters.AddRange(oracleParams);
                    }
                    //填充
                    oda.Fill(ds);
                    oracleCon.Close();
                    return ds;
                }
                catch (Exception)
                {

                    return null;
                }

            }
        }

        /// <summary>
        /// 查询的存储过程,通过掉用存储过程
        /// </summary>
        /// <param name="procName"></param>
        /// <param name="oracleParams"></param>
        /// <returns></returns>
        public static DataSet SelectProcMethod(string procName, OracleParameter[] oracleParams)
        {
            using (OracleConnection oracleCon = GetConn())
            {
                try
                {
                    //实例化 DataTable
                    DataSet ds = new DataSet();
                    oracleCon.Open();
                    //实例化 SqlDataAdapter
                    OracleCommand oracleCmd = new OracleCommand(procName, oracleCon);
                    oracleCmd.CommandType = CommandType.StoredProcedure;
                    if (oracleParams != null && oracleParams.Length > 0)
                    {
                        oracleCmd.Parameters.AddRange(oracleParams);
                    }
                    OracleDataAdapter oda = new OracleDataAdapter(oracleCmd);
                    oda.Fill(ds);
                    return ds;
                }
                catch (Exception)
                {

                    return null;
                }

            }
        }
        #endregion

        #region 读取数据的 公共方法
        /// <summary>
        /// 读取数据的 公共方法（OracleDataReader）
        /// </summary>
        /// <param name="strOracle">执行的Oracle语句</param>
        /// <param name="oracleParams">传入的参数</param>
        /// <returns></returns>
        public static OracleDataReader ReaderDataMethod(string strOracle, OracleParameter[] oracleParams)     //数据库执行SQL语句
        {
            OracleConnection oracleCon = GetConn();
            try
            {
                OracleDataReader odr = null;
                oracleCon.Open();
                //实例化 SqlDataAdapter
                OracleCommand oracleCmd = new OracleCommand(strOracle, oracleCon);
                if (oracleParams != null && oracleParams.Length > 0)
                {
                    oracleCmd.Parameters.AddRange(oracleParams);
                }
                odr = oracleCmd.ExecuteReader();
                return odr;
            }
            catch (Exception)
            {

                return null;
            }
        }
        #endregion


        /// <summary>
        /// 执行多调sql语句，带有事务
        /// </summary>
        /// <param name="sOracleStringList"></param>
        /// <returns></returns>
        public static bool UpdateTranMethod(List<string> sOracleStringList)
        {
            //链接数据库
            using (OracleConnection oracleCon = GetConn())
            {
                OracleCommand oracleCmd = new OracleCommand();
                oracleCmd.Connection = oracleCon;
                //打开数据库
                oracleCon.Open();
                OracleTransaction tran = oracleCon.BeginTransaction();
                oracleCmd.Transaction = tran;
                bool flag = false;
                try
                {
                    foreach (var strOracle in sOracleStringList)
                    {
                        oracleCmd.CommandText = strOracle;
                        //获取返回受影响的函数
                        int n = oracleCmd.ExecuteNonQuery();
                        if (n > 0)
                        {
                            flag = true;
                        }
                        else
                        {
                            tran.Rollback();
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        tran.Commit();
                    }
                }
                catch (Exception)
                {
                    tran.Rollback();
                    flag = false;
                }
                finally
                {
                    //关闭数据库
                    oracleCon.Close();
                }
                return flag;
            }
        }

        /// <summary>
        /// 执行多调sql语句，带有事务可返回0行
        /// </summary>
        /// <param name="sOracleStringList"></param>
        /// <returns></returns>
        public static bool UpdateTransMethod(List<string> sOracleStringList)
        {
            //链接数据库
            using (OracleConnection oracleCon = GetConn())
            {
                OracleCommand oracleCmd = new OracleCommand();
                oracleCmd.Connection = oracleCon;
                //打开数据库
                oracleCon.Open();
                OracleTransaction tran = oracleCon.BeginTransaction();
                oracleCmd.Transaction = tran;
                bool flag = false;
                try
                {
                    foreach (var strOracle in sOracleStringList)
                    {
                        oracleCmd.CommandText = strOracle;
                        //获取返回受影响的函数
                        int n = oracleCmd.ExecuteNonQuery();
                        if (n >= 0)
                        {
                            flag = true;
                        }
                        else
                        {
                            tran.Rollback();
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        tran.Commit();
                    }
                }
                catch (Exception)
                {
                    tran.Rollback();
                    flag = false;
                }
                finally
                {
                    //关闭数据库
                    oracleCon.Close();
                }
                return flag;
            }
        }
    }
}
