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
        Waiting,
        InProgress,
        Finished,
        Canceled,
    }

    public class SystemTask
    {
        private static int _idCount = 0;
        public int Id { get; set; }
        public Status CurrentStatus { get; set; }
        public SubTaskRepository WaitingSubTasks { get; set; }
        public int CompletedSubTasks { get; set; }
        public int Progression { get; set; }
        public int TotalSubTasks;

        public SystemTask()
        {
            Id = _idCount++;
            CurrentStatus = Status.Waiting;
            WaitingSubTasks = new SubTaskRepository();
            CompletedSubTasks = 0;
            Progression = 0;
            var randomValue = new Random().Next(10,100);
            for(int i = 0; i < randomValue; i++)
            {
                WaitingSubTasks.Add(new SubTask());
            }
            TotalSubTasks = WaitingSubTasks.GetAll().Count();
        }

        public async Task Start()
        {
            WaitingSubTasks.GetAll().ToList().ForEach(async subtask =>
            {
                await subtask.Start();
                if (subtask.isCompleted)
                {
                    CompletedSubTasks++;
                    WaitingSubTasks.Delete(subtask);
                }

                if (CompletedSubTasks == TotalSubTasks)
                {
                    CurrentStatus = Status.Finished;
                }
            });

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
