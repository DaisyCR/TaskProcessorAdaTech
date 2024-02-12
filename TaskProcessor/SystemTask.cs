using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskProcessor_Core;

namespace TaskProcessor
{
    public enum Status
    {
        Created,
        Waiting,
        InProgress,
        Finished,
        Canceled,
    }

    public class SystemTask
    {
        public int Id { get; set; }
        private static int _idCount = 1;
        public Status CurrentStatus { get; set; }
        public SubTaskRepository WaitingSubTasks { get; set; }
        public SubTaskRepository CompletedSubTasks { get; set; }
        public int TotalSubTasks;
        public int Progress;

        public SystemTask()
        {
            Id = _idCount++;
            CurrentStatus = Status.Created;
            WaitingSubTasks = new SubTaskRepository();
            CompletedSubTasks = new SubTaskRepository();
            var randomValue = new Random().Next(10,100);
            for(int i = 0; i < randomValue; i++)
            {
                WaitingSubTasks.Add(new SubTask());
            }
            TotalSubTasks = WaitingSubTasks.GetAll().Count();
            Progress = 0;

        }

        public Task Start()
        {
            CurrentStatus = Status.InProgress;
            Task startingTasks = Task.Run(() =>
            {
                foreach(var subtask in WaitingSubTasks.GetAll().ToList()) 
                {
                    if (subtask.Start().IsCompleted)
                    {
                        CompletedSubTasks.Add(subtask);
                        WaitingSubTasks.Delete(subtask);
                        Progress = CompletedSubTasks.GetAll().Count();
                    }
                }

                if (CompletedSubTasks.GetAll().Count() == TotalSubTasks)
                {
                    CurrentStatus = Status.Finished;
                }
            });

            return Task.CompletedTask;
        }

        public Boolean isActive()
        {
            return CurrentStatus == Status.Waiting || CurrentStatus == Status.InProgress;
        }

        public Boolean isNotActive()
        {
            return !isActive();
        }
    }
}
