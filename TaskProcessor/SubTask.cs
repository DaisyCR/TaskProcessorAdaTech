using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskProcessor_Core
{
    public class SubTask
    {
        public TimeSpan duration { get; set; }
        public Boolean isCompleted { get; set; }

        public SubTask()
        {
            var randomValue = new Random().Next(3, 60);
            duration = TimeSpan.FromSeconds(randomValue);
        }
    }

}
