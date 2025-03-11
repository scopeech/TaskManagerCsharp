using System;
using System.Collections.Generic;
using System.IO;

namespace ToDoListConsoleApp
{
    class Program
    {
        static List<TaskItem> tasks = new List<TaskItem>();
        static string filePath = "tasks.txt"; // Путь к файлу, где будут храниться задачи

        static void Main(string[] args)
        {
            LoadTasksFromFile(); // Загрузка задач при старте программы

            while (true)
            {
                ShowMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddTask();
                        break;
                    case "2":
                        MarkTaskCompleted();
                        break;
                    case "3":
                        DeleteTask();
                        break;
                    case "4":
                        ShowTasks();
                        break;
                    case "5":
                        SaveTasksToFile(); // Сохранение задач перед выходом
                        Environment.Exit(0); // Выход из программы
                        break;
                    default:
                        Console.WriteLine("Неверный выбор, попробуйте снова.");
                        break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Список задач:");
            Console.WriteLine("1. Добавить задачу");
            Console.WriteLine("2. Отметить задачу как выполненную");
            Console.WriteLine("3. Удалить задачу");
            Console.WriteLine("4. Просмотр задач");
            Console.WriteLine("5. Выход");
            Console.Write("Выберите действие: ");
        }

        static void AddTask()
        {
            Console.Write("Введите описание задачи: ");
            string description = Console.ReadLine();
            tasks.Add(new TaskItem { Description = description, IsCompleted = false });
            Console.WriteLine("Задача добавлена!");
            SaveTasksToFile(); // Сохранение задач в файл после добавления
            Console.ReadLine();
        }

        static void MarkTaskCompleted()
        {
            Console.Write("Введите номер задачи для отметки как выполненную: ");
            int taskNumber;
            if (int.TryParse(Console.ReadLine(), out taskNumber) && taskNumber > 0 && taskNumber <= tasks.Count)
            {
                tasks[taskNumber - 1].IsCompleted = true;
                Console.WriteLine("Задача отмечена как выполненная!");
                SaveTasksToFile(); // Сохранение задач в файл после изменения
            }
            else
            {
                Console.WriteLine("Неверный номер задачи.");
            }
            Console.ReadLine();
        }

        static void DeleteTask()
        {
            Console.Write("Введите номер задачи для удаления: ");
            int taskNumber;
            if (int.TryParse(Console.ReadLine(), out taskNumber) && taskNumber > 0 && taskNumber <= tasks.Count)
            {
                tasks.RemoveAt(taskNumber - 1); // Удаляем задачу по индексу
                Console.WriteLine("Задача удалена!");
                SaveTasksToFile(); // Сохранение задач в файл после удаления
            }
            else
            {
                Console.WriteLine("Неверный номер задачи.");
            }
            Console.ReadLine();
        }

        static void ShowTasks()
        {
            Console.Clear();
            if (tasks.Count == 0)
            {
                Console.WriteLine("Список задач пуст.");
            }
            else
            {
                Console.WriteLine("Список задач:");
                for (int i = 0; i < tasks.Count; i++)
                {
                    string status = tasks[i].IsCompleted ? "Выполнено" : "Не выполнено";
                    Console.WriteLine($"{i + 1}. {tasks[i].Description} - {status}");
                }
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey(); // Ожидание нажатия клавиши
        }

        // Метод для сохранения задач в файл
        static void SaveTasksToFile()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var task in tasks)
                {
                    string status = task.IsCompleted ? "Выполнено" : "Не выполнено";
                    writer.WriteLine($"{task.Description} | {status}");
                }
            }
        }

        // Метод для загрузки задач из файла
        static void LoadTasksFromFile()
        {
            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var taskParts = line.Split('|');
                        if (taskParts.Length == 2)
                        {
                            tasks.Add(new TaskItem
                            {
                                Description = taskParts[0].Trim(),
                                IsCompleted = taskParts[1].Trim() == "Выполнено"
                            });
                        }
                    }
                }
            }
        }
    }

    class TaskItem
    {
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
