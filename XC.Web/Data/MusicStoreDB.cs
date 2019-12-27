using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace XC.Web.Models
{
    public class MusicStoreDB : DbContext
    {
        public MusicStoreDB (DbContextOptions<MusicStoreDB> options)
            : base(options)
        {
        }

        public DbSet<XC.Web.Models.Album> Album { get; set; }
    }
}
