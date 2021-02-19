using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TodoList.Data.POCO;

namespace TodoList.Data
{
    public partial class TodoListDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TodoListDbContext(DbContextOptions<TodoListDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<TaskItem> TaskItems { get; set; }
    }
}
