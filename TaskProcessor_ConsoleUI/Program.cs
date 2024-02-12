using System.Diagnostics;
using System.Security.Principal;
using TaskProcessor;
using TaskProcessor_Core;

namespace TaskProcessor_ConsoleUI
{
    internal class Program
    {
        private static TaskManager manager = new TaskManager();
        static void Main(string[] args)
        {
            
            PrintMenu();
            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.D1:
                        var newTask = manager.CreateNewTask();
                        Console.WriteLine($"Task #{newTask.Id} created succefully");
                        break;
                    case ConsoleKey.D2:
                        PrintTasks(manager.GetActiveTasks());
                        break;
                    case ConsoleKey.D3:
                        PrintTasks(manager.GetInactiveTasks());
                        break;
                    case ConsoleKey.D4:
                        manager.GetActiveTasks().ForEach(async task =>
                        {
                            await task.Start();
                        });
                        while (manager.GetActiveTasks().Count() > 0)
                        {
                            Thread.Sleep(500);
                            Console.Clear();
                            PrintTasks(manager.GetActiveTasks());
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

        private static void PrintTasks(List<SystemTask> tasks)
        {
            tasks.ForEach(task =>
            {
                Console.WriteLine($"Task #{task.Id}\n" +
                    $"Status: {task.CurrentStatus}\n" +
                    $"Progress: {task.Progress} / {task.TotalSubTasks}\n");
            });

         }
    }
}
