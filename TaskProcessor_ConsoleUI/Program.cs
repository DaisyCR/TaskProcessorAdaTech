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
                        manager.CreateNewTask();
                        Console.WriteLine($"Task #{manager.Tasks.GetAll().ToList().Last().Id} created succefully");
                        break;
                    case ConsoleKey.D2:
                        PrintActiveTasks();
                        break;
                    case ConsoleKey.D3:
                        manager.Tasks.GetAll().ToList().ForEach(task =>
                        {
                            task.Start();
                        });
                        while (manager.GetActiveTasks().Count() > 0)
                        {
                            Thread.Sleep(500);
                            Console.Clear();
                            PrintActiveTasks();
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
            Console.WriteLine("Press 3 to run all task");
            Console.WriteLine("---------------------------------------------------");
        }

        private static void PrintActiveTasks()
        {
            manager.GetActiveTasks().ForEach(task =>
            {
                Console.WriteLine($"Task #{task.Id}\n" +
                    $"WaitingSubTasks: {task.WaitingSubTasks.GetAll().Count()}\n" +
                    $"CompletedSubTasks: {task.CompletedSubTasks}\n");
            });

         }
            
    }
}
