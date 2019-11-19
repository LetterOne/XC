using Microsoft.EntityFrameworkCore;
using XC.WebAPI.Models;

namespace XC.WebAPI.Context
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    public class XCContext:DbContext
    {
        public XCContext()
        {
        }

        public XCContext(DbContextOptions<XCContext> options)
         : base(options)
        {
        }

        /// <summary>
        /// 用户信息类
        /// </summary>
        public DbSet<Sys_User> Users { get; set; }
    }
}
