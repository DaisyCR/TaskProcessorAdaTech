using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProcessor;

namespace TaskProcessor_Core
{
    public class TaskRepository : IRepository<SystemTask>
    {
        private List<SystemTask> tasks = new List<SystemTask>();

        public void Add(SystemTask entity)
        {
            tasks.Add(entity);
        }

        public void Delete(SystemTask entity)
        {
            tasks.Remove(entity);
        }

        public IEnumerable<SystemTask> GetAll()
        {
            return tasks;
        }

        public SystemTask GetById(int id)
        {
            return tasks.Find(task => task.Id == id);
        }

        public void Update(SystemTask entity)
        {
            throw new NotImplementedException();
        }
    }
}
