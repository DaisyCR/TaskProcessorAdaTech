using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskProcessor_Core
{
    public class SubTaskRepository : IRepository<SubTask>
    {
        private List<SubTask> subTasks = new List<SubTask>();
        public void Add(SubTask entity)
        {
            subTasks.Add(entity);
        }

        public void Delete(SubTask entity)
        {
            subTasks.Remove(entity);
        }

        public IEnumerable<SubTask> GetAll()
        {
            return subTasks;
        }

        public SubTask GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(SubTask entity)
        {
            throw new NotImplementedException();
        }
    }
}
