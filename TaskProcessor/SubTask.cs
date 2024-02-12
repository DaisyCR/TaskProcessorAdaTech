using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TaskProcessor_Core
{
    public class SubTask
    {
        public TimeSpan duration { get; set; }
        public Boolean isCompleted = false;

        public SubTask()
        {
            var randomValue = new Random().Next(1, 2);
            duration = TimeSpan.FromSeconds(randomValue);
            Task<int> result = Start();
        }

        public async Task<int> Start()
        {
            await Task.Delay(duration);
            isCompleted = true;
            return 0;
        }
    }

}
