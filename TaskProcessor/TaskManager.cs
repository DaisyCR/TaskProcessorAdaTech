using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using TaskProcessor;

namespace TaskProcessor_Core
{
    public static class TaskManager
    {
        private static IRepository<SystemTask> Tasks = new TaskRepository();

        public static Task<SystemTask> CreateNewTask()
        {
            Task<SystemTask> creatingNewTask = Task.Run(() =>
            {
                var newTask = new SystemTask();
                Tasks.Add(newTask);
                return newTask;
            });

            return creatingNewTask;
        }

        public static Task<SystemTask> GetTaskById(int id)
        {
            Task<SystemTask> gettingTaskById = Task.Run(() =>
            {
                return Tasks.GetById(id);
            });

            return gettingTaskById;
            
        }
        public static Task CancelTask(int taskId)
        {
            Task cancellingTask = Task.Run(() => 
            {
                var currentTask = Tasks.GetById(taskId);
                if (currentTask == null)
                {
                    throw new InvalidOperationException();
                }
                if (currentTask.isNotActive())
                {
                    throw new InvalidOperationException();
                }
                currentTask.CurrentStatus = Status.Canceled;
            });

            return Task.CompletedTask;
        }

        public static async Task<IEnumerable<SystemTask>> GetActiveTasks()
        {
            var activeTasksList = new List<SystemTask>();
            var gettingActiveTasks = Task.Run(() =>
            {
                foreach (var task in Tasks.GetAll())
                {
                    if (task.isActive())
                    {
                        activeTasksList.Add(task);
                    }
                }

                return activeTasksList;
            });

            return await gettingActiveTasks;
            
        }

        public static async Task<IEnumerable<SystemTask>> GetInactiveTasks()
        {
            var inactiveStatusList = new List<SystemTask>();
            var gettingInactiveTasks = Task.Run(() =>
            {
                foreach (var task in Tasks.GetAll())
                {
                    if (task.isNotActive())
                    {
                        inactiveStatusList.Add(task);
                    }
                }
                return inactiveStatusList;
            });

            return await gettingInactiveTasks;
        }


    }
}
