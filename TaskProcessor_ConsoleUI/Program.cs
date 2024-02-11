using System.Diagnostics;
using System.Security.Principal;
using TaskProcessor_Core;

namespace TaskProcessor_ConsoleUI
{
    internal class Program
    {
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
                        TaskManager.CreateNewTask();
                        Console.WriteLine($"Task #{TaskManager.taskList.Last().Id} created succefully");
                        break;
                    case ConsoleKey.D2:
                        PrintActiveTasks();
                        break;
                }
                PrintMenu();
                
            } while (true);
        }

        private static void PrintMenu()
        {
            Thread.Sleep(500);
            Console.Clear();
            Console.WriteLine("Hello, World!");
            Console.WriteLine("Press 1 to create a new task");
            Console.WriteLine("Press 2 to show active tasks");
            Console.WriteLine("---------------------------------------------------");
        }

        private static void PrintActiveTasks()
        {
            TaskManager.GetActiveTasks().ForEach(task =>
            {
                Console.WriteLine($"Task #{task.Id} {task.WaitingSubTasks.Count}/ {task.GetProgression()}%");
            });
        }
    }
}
