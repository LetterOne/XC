using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XC.Home.Models
{
    public class SqlContext:DbContext
    {
        public SqlContext(DbContextOptions<SqlContext> Options) : base(Options)
        {
        }
        public DbSet<Note> notes { get; set; } //在数据库中生成数据表A
    }
}
