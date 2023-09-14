using Microsoft.EntityFrameworkCore;
using TodoWebService.Data;
using TodoWebService.Models.DTOs.Pagination;
using TodoWebService.Models.DTOs.Todo;
using TodoWebService.Models.Entities;

namespace TodoWebService.Services
{
    public class TodoService : ITodoService
    {

        private readonly TodoDbContext _context;

        public TodoService(TodoDbContext context)
        {
            _context = context;
        }

        public async Task<TodoItemDto?> ChangeTodoItemStatus(string userId, int id, bool isCompleted)
        {
            var item = await _context.TodoItems.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

            if (item is null)
                return null;

            item.IsCompleted = isCompleted;
            item.UpdatedTime = DateTimeOffset.UtcNow;

            await _context.SaveChangesAsync();
            return new TodoItemDto
            {
                Id = item.Id,
                Text = item.Text,
                CreatedAt = item.CreatedTime,
                IsComleted = item.IsCompleted
            };
        }

        public async Task<TodoItemDto?> CreateTodo(string userId, CreateTodoItemRequest request)
        {
            var user = await _context.Users.FindAsync(userId) ?? throw new KeyNotFoundException();

            var now = DateTimeOffset.UtcNow;

            var item = new TodoItem
            {
                Text = request.Text,
                CreatedTime = now,
                UpdatedTime = now,
                IsCompleted = false,
                UserId = userId
            };
            item = _context.TodoItems.Add(item).Entity;
            await _context.SaveChangesAsync();

            return new TodoItemDto
            {
                Id = item.Id,
                Text = item.Text,
                CreatedAt = item.CreatedTime,
                IsComleted = item.IsCompleted
            };
        }

        public async Task<bool> DeleteTodo(string userId, int id)
        {
            var todoItem = await _context.TodoItems.FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
            if (todoItem is null)
                return false;

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TodoItemDto?> GetTodoItem(string userId, int id)
        {
            var todoItem = await _context.TodoItems.FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

            return todoItem is not null 
                ? new TodoItemDto
                {
                    Id = todoItem.Id,
                    Text = todoItem.Text,
                    CreatedAt = todoItem.CreatedTime,
                    IsComleted = todoItem.IsCompleted
                } : null;
        }

        public async Task<PaginatedListDto<TodoItemDto>> GetTodoItems(string userId, int page, int pageSize, string? search, bool? isCompleted)
        {
            IQueryable<TodoItem> query = _context.TodoItems.Where(e => e.UserId == userId);

            if(!string.IsNullOrWhiteSpace(search))
                query = query.Where(e => e.Text.Contains(search));


            if(isCompleted.HasValue)
                query = query.Where(e => e.IsCompleted == isCompleted);

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedListDto<TodoItemDto>(
                    items.Select(e => new TodoItemDto
                    {
                        Id = e.Id,
                        Text = e.Text,
                        CreatedAt = e.CreatedTime,
                        IsComleted = e.IsCompleted
                    }),
                    new PaginationMeta(page, pageSize, totalCount)
                );
        }
    }
}
