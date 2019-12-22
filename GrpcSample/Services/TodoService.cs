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


        public override async Task<returnBool> CreateTodo(TodoReturn request, ServerCallContext context)
        {
            await _toporepository.Create(new Model.Todo
            { 
                Category= request.Category,
                Content=request.Content,
                Title=request.Title
            }).ConfigureAwait(false);
            var response = new returnBool() { Bool= true};

            return await Task.FromResult(response);
        }

        public override async Task GetTodoByName(GetTodoName request, IServerStreamWriter<TodoReturn> responseStream, ServerCallContext context)
        {
            List<TodoReturn> listTodo = new List<TodoReturn>();
            switch (request.Name)
            {
                case "all":
                    var result = await _toporepository.GetAllTodos();
                    foreach (var item in result)
                    {
                        listTodo.Add( new TodoReturn 
                        {
                        Category= item.Category,
                        Content=item.Content,
                        Title=item.Title
                        });

                    }
                    foreach (var item in listTodo)
                    {
                        await responseStream.WriteAsync(item);
                    }
                    break;
                default:
                    var result2 = await _toporepository.GetTodo(request.Name);
                    foreach (var item in result2)
                    {
                        listTodo.Add(new TodoReturn
                        {
                            Category = item.Category,
                            Content = item.Content,
                            Title = item.Title
                        });

                    }
                    foreach (var item in listTodo)
                    {
                        await responseStream.WriteAsync(item);
                    }
                    break;

            }
        }
    }
}
