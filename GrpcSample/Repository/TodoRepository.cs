using GrpcSample.Model;
using GrpcSample.MongoDB;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcSample.Repository
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ITodoContext _context;
        public TodoRepository(ITodoContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Todo>> GetAllTodos()
        {
            return await _context
                            .Todos
                            .Find(_ => true)
                            .ToListAsync();
        }
        public async Task<IEnumerable<Todo>> GetTodo(string name)
        {
            FilterDefinition<Todo> filter = Builders<Todo>.Filter.Eq(m => m.Title, name);
            return await _context
                    .Todos
                    .Find(filter)
                    .ToListAsync();
        }
        public async Task Create(Todo todo)
        {
            await _context.Todos.InsertOneAsync(todo);
        }
        public async Task<bool> Update(Todo todo)
        {
            ReplaceOneResult updateResult =
                await _context
                        .Todos
                        .ReplaceOneAsync(
                            filter: g => g.Id == todo.Id,
                            replacement: todo);
            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
        public async Task<bool> Delete(Guid id)
        {
            FilterDefinition<Todo> filter = Builders<Todo>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = await _context
                                                .Todos
                                              .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
    }
}
