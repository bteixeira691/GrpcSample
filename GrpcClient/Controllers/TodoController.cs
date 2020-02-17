using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GrpcClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        [HttpGet]
        public async Task<IList<Model.Todo>> GetAsync()
        {
            List<GrpcClient.Model.Todo> listTodo = new List<GrpcClient.Model.Todo>();
            using (var result = client().GetTodos(new VoidRequest()))
            {
                while (await result.ResponseStream.MoveNext())
                {
                    var todo = new GrpcClient.Model.Todo
                    {
                        Category = result.ResponseStream.Current.Category,
                        Content = result.ResponseStream.Current.Content,
                        Title = result.ResponseStream.Current.Title
                    };
                    listTodo.Add(todo);
                }
            };

            return listTodo;
        }

        // GET: api/Todo/5
        [HttpGet("{name}", Name = "Get")]
        public async Task<Model.Todo> GetSingleTodo(string name)
        {

            var result = await client().SingleTodoAsync(new GetTodoName { Name = name });

            var todo = new Model.Todo
            {
                Category = result.Category,
                Content = result.Content,
                Title = result.Title
            };
            return todo;
        }

        // POST: api/Todo
        [HttpPost]
        public async Task<bool> Post(Model.Todo todo)
        {
            var result = await client().CreateTodoAsync(new TodoReturn
            {
                Category = todo.Category,
                Content = todo.Content,
                Title = todo.Title
            });

            return result.Bool;
        }

        [HttpDelete("{name}", Name = "Delete")]
        public async Task<bool> Delete(string name)
        {
            var result = await client().DeleteTodoAsync(new GetTodoName { Name = name });

            return result.Bool;
        }

        private Todo.TodoClient client()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            return new Todo.TodoClient(channel);
        }

    }
}
