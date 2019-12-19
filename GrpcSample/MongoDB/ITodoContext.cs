using GrpcSample.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcSample.MongoDB
{
    public interface ITodoContext
    {
        IMongoCollection<Todo> Todos { get; }
    }
}
