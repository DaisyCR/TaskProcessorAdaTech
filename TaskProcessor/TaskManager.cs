using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProcessor;

namespace TaskProcessor_Core
{
    public static class TaskManager
    {
        public static List<SystemTask> taskList = new List<SystemTask>();

        public static void CreateNewTask()
        {
            taskList.Add(new SystemTask());
        }

        public static void CancelTask(int taskId) {
            var currentTask = taskList.FirstOrDefault(x => x.Id == taskId);
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

        public static List<SystemTask> GetActiveTasks()
        {
            var activeStatusList = new List<SystemTask>();
            taskList.ForEach(task =>
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
