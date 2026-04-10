using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static List<string> tasks = new List<string>();
    static List<bool> completed = new List<bool>();
    static string filePath = "tasks.txt";

    static void Main()
    {
        LoadTasks();

        while (true)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("==== TO-DO LIST ====\n");
            Console.ResetColor();

            DisplayTasks();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nOptions:");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. Remove Task");
            Console.WriteLine("3. Mark Completed");
            Console.WriteLine("4. Exit");
            Console.ResetColor();

            Console.Write("\nChoose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTask();
                    break;
                case "2":
                    RemoveTask();
                    break;
                case "3":
                    MarkCompleted();
                    break;
                case "4":
                    SaveTasks();
                    return;
                default:
                    Console.WriteLine("Invalid choice!");
                    Console.ReadLine();
                    break;
            }
        }
    }

    static void DisplayTasks()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks yet!");
        }
        else
        {
            for (int i = 0; i < tasks.Count; i++)
            {
                string status = completed[i] ? "[✔]" : "[ ]";
                Console.WriteLine($"{i + 1}. {status} {tasks[i]}");
            }
        }
    }

    static void AddTask()
    {
        Console.Write("Enter task: ");
        string task = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(task))
        {
            tasks.Add(task);
            completed.Add(false);
            SaveTasks();
            Console.WriteLine("Task added!");
        }
        else
        {
            Console.WriteLine("Task cannot be empty!");
        }

        Console.ReadLine();
    }

    static void RemoveTask()
    {
        Console.Write("Enter task number: ");
        if (int.TryParse(Console.ReadLine(), out int num))
        {
            if (num >= 1 && num <= tasks.Count)
            {
                tasks.RemoveAt(num - 1);
                completed.RemoveAt(num - 1);
                SaveTasks();
                Console.WriteLine("Task removed!");
            }
            else
            {
                Console.WriteLine("Invalid number!");
            }
        }
        else
        {
            Console.WriteLine("Invalid input!");
        }

        Console.ReadLine();
    }

    static void MarkCompleted()
    {
        Console.Write("Enter task number: ");
        if (int.TryParse(Console.ReadLine(), out int num))
        {
            if (num >= 1 && num <= tasks.Count)
            {
                completed[num - 1] = true;
                SaveTasks();
                Console.WriteLine("Task marked completed!");
            }
            else
            {
                Console.WriteLine("Invalid number!");
            }
        }
        else
        {
            Console.WriteLine("Invalid input!");
        }

        Console.ReadLine();
    }

    static void SaveTasks()
    {
        List<string> lines = new List<string>();
        for (int i = 0; i < tasks.Count; i++)
        {
            lines.Add(tasks[i] + "|" + completed[i]);
        }
        File.WriteAllLines(filePath, lines);
    }

    static void LoadTasks()
    {
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split('|');
                tasks.Add(parts[0]);
                completed.Add(bool.Parse(parts[1]));
            }
        }
    }
}