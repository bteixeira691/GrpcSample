using GrpcSample.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcSample.MongoDB
{
    public class TodoContext : ITodoContext
    {
        private readonly IMongoDatabase _db;
        public TodoContext(MongoDBConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            _db = client.GetDatabase(config.Database);
        }
        public IMongoCollection<Todo> Todos => _db.GetCollection<Todo>("Todos");
    }
}
