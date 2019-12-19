using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcSample.MongoDB
{
    public class ServerConfig
    {
        public MongoDBConfig MongoDB { get; set; } = new MongoDBConfig();
    }
}
