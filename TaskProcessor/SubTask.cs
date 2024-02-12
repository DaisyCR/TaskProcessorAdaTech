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
        private int _randomValue;
       public SubTask()
        {
            _randomValue = new Random().Next(3, 60);
        }

        public Task Start()
        {
            Task.Delay(_randomValue * 1000);
            return Task.CompletedTask;
        }
    }

}
