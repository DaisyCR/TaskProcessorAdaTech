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
        public List<SubTask> WaitingSubTasks { get; set; }
        public List<SubTask> CompletedSubTasks { get; set; }
        public int Progression { get; set; }

        public SystemTask()
        {
            Id = _idCount++;
            CurrentStatus = Status.Waiting;
            WaitingSubTasks = new List<SubTask>();
            CompletedSubTasks = new List<SubTask>();
            var randomValue = new Random().Next(1,5);
            for(int i = 0; i < randomValue; i++)
            {
                WaitingSubTasks.Add(new SubTask());
            }
        }

        public void Start()
        {
            WaitingSubTasks.ForEach(subtask =>
            {
                subtask.Start();
            });

            do
            {
                WaitingSubTasks.ForEach(subtask =>
                {
                    if (subtask.isCompleted)
                    {
                        CompletedSubTasks.Add(subtask);
                        WaitingSubTasks.Remove(subtask);
                    }
                });
            } while( WaitingSubTasks.Count > 0 );
            
        }

        public Boolean isActive()
        {
            return CurrentStatus == Status.Waiting || CurrentStatus == Status.InProgress;
        }

        public Boolean isNotActive()
        {
            return !isActive();
        }

        public int GetProgression()
        {
            var totalSubTasks = WaitingSubTasks.Count + CompletedSubTasks.Count;
            return CompletedSubTasks.Count / totalSubTasks;
        }
    }
}
