using Grpc.Core;
using GrpcSample.Protos;
using GrpcSample.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcSample.Services
{
    public class TodoService : Todo.TodoBase
    {
        private readonly ITodoRepository _toporepository;
        public TodoService(ITodoRepository todoRepository)
        {
            _toporepository = todoRepository;
        }

        public override Task<GetTodoByNameReply> GetTodoByName(GetTodoName request, ServerCallContext context)
        {
            return base.GetTodoByName(request, context);
        }
    }
}
