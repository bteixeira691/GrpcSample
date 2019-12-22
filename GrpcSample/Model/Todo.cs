using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcSample.Model
{
    public class Todo
    {
        [BsonId]
        public ObjectId InternalId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }

       
    }
}
