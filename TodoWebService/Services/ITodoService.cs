using TodoWebService.Models.DTOs.Pagination;
using TodoWebService.Models.DTOs.Todo;

namespace TodoWebService.Services
{
    public interface ITodoService
    {
        Task<TodoItemDto?> GetTodoItem(string userId, int id);
        Task<TodoItemDto?> ChangeTodoItemStatus(string userId, int id, bool isCompleted);
        Task<TodoItemDto?> CreateTodo(string userId, CreateTodoItemRequest request);
        Task<bool> DeleteTodo(string userId, int id);
        Task<PaginatedListDto<TodoItemDto>> GetTodoItems(string userId, int page, int pageSize, string? search, bool? isCompleted);
    }
}
