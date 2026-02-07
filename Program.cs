using System;
using System.Collections.Generic;

class Program
{
    static List<string> tasks = new List<string>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n1. Add Task");
            Console.WriteLine("2. View Tasks");
            Console.WriteLine("3. Exit");
            Console.Write("Choose option: ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Enter task: ");
                string task = Console.ReadLine();
                tasks.Add(task);
                Console.WriteLine("Task added!");
            }
            else if (choice == "2")
            {
                Console.WriteLine("\nTasks:");
                for (int i = 0; i < tasks.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {tasks[i]}");
                }
            }
            else if (choice == "3")
            {
                break;
            }
        }
    }
}
