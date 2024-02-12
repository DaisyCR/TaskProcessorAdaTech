using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProcessor;

namespace TaskProcessor_Core
{
    public static class  TaskManager
    {
        private static IRepository<SystemTask> Tasks = new TaskRepository();

        public static SystemTask CreateNewTask()
        {
            var newTask = new SystemTask();
            Tasks.Add(newTask);
            return newTask;
        }

        public static SystemTask GetTaskById(int id)
        {
            return Tasks.GetById(id);
        }
        public static void CancelTask(int taskId) {
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

        public static IEnumerable<SystemTask> GetActiveTasks()
        {
            var activeStatusList = new List<SystemTask>();
            foreach(var task in Tasks.GetAll())
            {
                if (task.isActive())
                {
                    activeStatusList.Add(task);
                }
            }
            return activeStatusList;
        }

        public static IEnumerable<SystemTask> GetInactiveTasks()
        {
            var inactiveStatusList = new List<SystemTask>();
            foreach(var task in Tasks.GetAll())
            {
                if (task.isNotActive())
                {
                    inactiveStatusList.Add(task);
                }
            }
            return inactiveStatusList;
        }


    }
}
