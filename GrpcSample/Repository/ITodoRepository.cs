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
        Task<Todo> GetTodo(Guid id);
        Task Create(Todo todo);
        Task<bool> Update(Todo todo);
        Task<bool> Delete(Guid id);

    }
}
