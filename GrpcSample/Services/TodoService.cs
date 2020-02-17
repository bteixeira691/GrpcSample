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
            try
            {
                await _toporepository.Create(new Model.Todo
                {
                    Category = request.Category,
                    Content = request.Content,
                    Title = request.Title
                }).ConfigureAwait(false);
                var response = new returnBool() { Bool = true };
                return await Task.FromResult(response);
            }
            catch (Exception e)
            {
                var response = new returnBool() { Bool = false };
                return await Task.FromResult(response);
            }
        }

        public override async Task GetTodos(VoidRequest request, IServerStreamWriter<TodoReturn> responseStream, ServerCallContext context)
        {
            List<TodoReturn> listTodo = new List<TodoReturn>();

            var result = await _toporepository.GetAllTodos();
            foreach (var item in result)
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


        }

        public override async Task<returnBool> DeleteTodo(GetTodoName request, ServerCallContext context)
        {
            try
            {
                await _toporepository.Delete(request.Name).ConfigureAwait(false);
                var response = new returnBool() { Bool = true };
                return await Task.FromResult(response);
            }
            catch (Exception e)
            {
                var response = new returnBool() { Bool = false };
                return await Task.FromResult(response);
            }



        }
    }
}
