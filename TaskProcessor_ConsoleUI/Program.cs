using System.Diagnostics;
using System.Security.Principal;
using TaskProcessor;
using TaskProcessor_Core;

namespace TaskProcessor_ConsoleUI
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            
            PrintMenu();
            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.D1:
                        var newTask = TaskManager.CreateNewTask();
                        Console.WriteLine($"Task #{newTask.Result.Id} created succefully");
                        break;
                    case ConsoleKey.D2:
                        PrintTasks(TaskManager.GetActiveTasks().Result);
                        break;
                    case ConsoleKey.D3:
                        PrintTasks(TaskManager.GetInactiveTasks().Result);
                        break;
                    case ConsoleKey.D4:
                        foreach(var task in TaskManager.GetActiveTasks().Result)
                        {
                            task.Start();
                        }
                        while (TaskManager.GetActiveTasks().Result.Count() > 0)
                        {
                            Thread.Sleep(500);
                            Console.Clear();
                            PrintTasks(TaskManager.GetActiveTasks().Result);
                        }
                        
                        break;
                }
                PrintMenu();
                
            } while (true);
        }

        private static void PrintMenu()
        {
            Thread.Sleep(100);
            Console.Clear();
            Console.WriteLine("Hello, World!");
            Console.WriteLine("Press 1 to create a new task");
            Console.WriteLine("Press 2 to show active tasks");
            Console.WriteLine("Press 3 to show inactive tasks");
            Console.WriteLine("Press 4 to run all task");
            Console.WriteLine("---------------------------------------------------");
        }

        private static void PrintTasks(IEnumerable<SystemTask> tasks)
        {
            foreach(var task in tasks)
            {
                Console.WriteLine($"Task #{task.Id}\n" +
                    $"Status: {task.CurrentStatus}\n" +
                    $"Progress: {task.Progress} / {task.TotalSubTasks}\n");
            }

         }
    }
}
