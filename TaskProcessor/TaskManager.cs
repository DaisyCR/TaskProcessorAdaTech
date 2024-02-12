using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProcessor;

namespace TaskProcessor_Core
{
    public class TaskManager
    {
        public IRepository<SystemTask> Tasks { get; set; }

        public TaskManager()
        {
            Tasks = new TaskRepository(); ;
        }

        public void CreateNewTask()
        {
            Tasks.Add(new SystemTask());
        }

        public void CancelTask(int taskId) {
            var currentTask = Tasks.GetById(taskId);
            if(currentTask == null)
            {
                throw new InvalidOperationException();
            }
            if (currentTask.isNotActive())
            {
                throw new InvalidOperationException();
            }
            currentTask.CurrentStatus = Status.Canceled;
        }

        public List<SystemTask> GetActiveTasks()
        {
            var activeStatusList = new List<SystemTask>();
            Tasks.GetAll().ToList().ForEach(task =>
            {
                if (task.isActive())
                {
                    activeStatusList.Add(task);
                }
            });
            return activeStatusList;
        }
    }
}
