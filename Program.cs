using System;
using System.Collections.Generic;

class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime Deadline { get; set; }
    public bool IsCompleted { get; set; }

    public override string ToString()
    {
        string status = IsCompleted ? "Completed" : "Pending";
        return $"[{Id}] {Title} | Deadline: {Deadline:dd-MM-yyyy} | Status: {status}";
    }
}


class Program
{
    static PriorityQueue<TaskItem, DateTime> taskQueue = new();
    static List<TaskItem> allTasks = new();
    static int taskIdCounter = 1;

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n==== Student Task Manager ====");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. View Tasks by Priority");
            Console.WriteLine("3. Complete Task (Auto Delete)");
            Console.WriteLine("4. Exit");
            Console.Write("Choose option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTask();
                    break;
                case "2":
                    ViewTasks();
                    break;
                case "3":
                    CompleteTask();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }
    }

    static void AddTask()
    {

        Console.Write("Enter task title: ");
        string title = Console.ReadLine();

        Console.Write("Enter deadline (yyyy-mm-dd): ");
        DateTime deadline = DateTime.Parse(Console.ReadLine());

        TaskItem task = new TaskItem
        {
            Id = taskIdCounter++,
            Title = title,
            Deadline = deadline,
            IsCompleted = false
        };

        allTasks.Add(task);
        taskQueue.Enqueue(task, deadline);

        Console.WriteLine("Task added with deadline.");
    }


    static void ViewTasks()
    {
        if (taskQueue.Count == 0)
        {
            Console.WriteLine("No tasks available.");
            return;
        }

        Console.WriteLine("\n--- Tasks Ordered by Deadline ---");

        var tempQueue = new PriorityQueue<TaskItem, DateTime>(taskQueue.UnorderedItems);

        while (tempQueue.Count > 0)
        {
            var task = tempQueue.Dequeue();
            Console.WriteLine(task);
        }
    }


    static void CompleteTask()
    {
        Console.Write("Enter task ID to complete: ");
        int id = int.Parse(Console.ReadLine());

        var task = allTasks.Find(t => t.Id == id);

        if (task == null)
        {
            Console.WriteLine("Task not found.");
            return;
        }

        // Remove from list
        allTasks.Remove(task);

        // Rebuild priority queue
        taskQueue.Clear();
        foreach (var t in allTasks)
        {
            taskQueue.Enqueue(t, t.Deadline);
        }

        Console.WriteLine("Task completed and removed.");
    }


}
