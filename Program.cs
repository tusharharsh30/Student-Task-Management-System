using System;
using System.Collections.Generic;

class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Priority { get; set; } // Lower number = higher priority
    public bool IsCompleted { get; set; }

    public override string ToString()
    {
        string status = IsCompleted ? "Completed" : "Pending";
        return $"[{Id}] {Title} | Priority: {Priority} | Status: {status}";
    }
}

class Program
{
    static PriorityQueue<TaskItem, int> taskQueue = new();
    static List<TaskItem> allTasks = new();
    static int taskIdCounter = 1;

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n==== Student Task Manager ====");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. View Tasks by Priority");
            Console.WriteLine("3. Mark Task as Completed");
            Console.WriteLine("4. Delete Completed Tasks");
            Console.WriteLine("5. Exit");
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
                    DeleteCompletedTasks();
                    break;
                case "5":
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

        Console.Write("Enter priority (1 = High, 5 = Low): ");
        int priority = int.Parse(Console.ReadLine());

        TaskItem task = new TaskItem
        {
            Id = taskIdCounter++,
            Title = title,
            Priority = priority,
            IsCompleted = false
        };

        allTasks.Add(task);
        taskQueue.Enqueue(task, priority);

        Console.WriteLine("Task added successfully!");
    }

    static void ViewTasks()
    {
        if (taskQueue.Count == 0)
        {
            Console.WriteLine("No tasks available.");
            return;
        }

        Console.WriteLine("\n--- Tasks (High Priority First) ---");

        // Temporary queue to preserve original queue
        var tempQueue = new PriorityQueue<TaskItem, int>(taskQueue.UnorderedItems);

        while (tempQueue.Count > 0)
        {
            var task = tempQueue.Dequeue();
            Console.WriteLine(task);
        }
    }

    static void CompleteTask()
    {
        Console.Write("Enter task ID to mark as completed: ");
        int id = int.Parse(Console.ReadLine());

        var task = allTasks.Find(t => t.Id == id);

        if (task == null)
        {
            Console.WriteLine("Task not found!");
            return;
        }

        task.IsCompleted = true;
        Console.WriteLine("Task marked as completed.");
    }

    static void DeleteCompletedTasks()
    {
        allTasks.RemoveAll(t => t.IsCompleted);

        taskQueue.Clear();
        foreach (var task in allTasks)
        {
            taskQueue.Enqueue(task, task.Priority);
        }

        Console.WriteLine("Completed tasks deleted.");
    }
}
