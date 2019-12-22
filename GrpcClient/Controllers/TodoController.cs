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
        // GET: api/Todo
        [HttpGet]
        public async Task<IList<GrpcClient.Model.Todo>> GetAsync()
        {
            List < GrpcClient.Model.Todo > listTodo = new List<GrpcClient.Model.Todo>();
            using(var result = client().GetTodoByName(new GetTodoName { Name = "all" }))
            {
                while(await result.ResponseStream.MoveNext())
                {
                    var todo = new GrpcClient.Model.Todo
                    {
                        Category= result.ResponseStream.Current.Category,
                        Content=result.ResponseStream.Current.Content,
                        Title=result.ResponseStream.Current.Title
                    };
                    listTodo.Add(todo);
                }
            };

            return listTodo;
        }

        // GET: api/Todo/5
        [HttpGet("{name}", Name = "Get")]
        public async Task<IList<GrpcClient.Model.Todo>> GetAsync(string name)
        {
            List<GrpcClient.Model.Todo> listTodo = new List<GrpcClient.Model.Todo>();
            using (var result = client().GetTodoByName(new GetTodoName { Name = name }))
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

        // POST: api/Todo
        [HttpPost]
        public bool PostAsync(GrpcClient.Model.Todo todo)
        {
            var result = client().CreateTodo(new TodoReturn
            {
                Category = todo.Category,
                Content = todo.Content,
                Title = todo.Title
            });

            if (result.Bool)
                return true;
            return false;
        }

             

       

      private Todo.TodoClient client()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            return new Todo.TodoClient(channel);
        }
    }
}
