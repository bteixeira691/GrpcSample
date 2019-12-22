using GrpcSample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcSample.Repository
{
    public interface ITodoRepository
    {

        Task<IEnumerable<Todo>> GetAllTodos();
        Task<IEnumerable<Todo>> GetTodo(string name);
        Task Create(Todo todo);

        Task<bool> Delete(string name);

    }
}
