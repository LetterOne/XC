using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XC.WebAPI.Models
{
    public class MongoDBSettings : IMongoDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IMongoDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
