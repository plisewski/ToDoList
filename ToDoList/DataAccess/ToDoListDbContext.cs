using System.Data.Entity;
using ToDoList.Model;

namespace ToDoList.DataAccess
{
    public class ToDoListDbContext : DbContext
    {
        public DbSet<ToDoListModel> ToDoTasks { get; set; }

        public ToDoListDbContext() : base("ToDoListDB")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ToDoListDbContext>());
        }
    }
}
