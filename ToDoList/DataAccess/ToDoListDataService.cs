using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Model;

namespace ToDoList.DataAccess
{
    public class ToDoListDataService
    {
        public void Add(ToDoListModel model)
        {
            using (var db = new ToDoListDbContext())
            {
                db.ToDoTasks.Add(model);
                db.SaveChanges();
            }
        }

        public IEnumerable<ToDoListModel> Read()
        {
            using (var db = new ToDoListDbContext())
            {
                return db.ToDoTasks.AsNoTracking().ToList();
            }
        }

        public void Update(ToDoListModel model)
        {
            using (var db = new ToDoListDbContext())
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void Delete(ToDoListModel model)
        {
            using (var db = new ToDoListDbContext())
            {
                var entry = db.Entry(model);
                if (entry.State == EntityState.Detached)
                    db.ToDoTasks.Attach(model);
                db.ToDoTasks.Remove(model);
                db.SaveChanges();
            }
        }
    }
}
