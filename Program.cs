using System;
using System.Threading.Channels;

namespace TaskManager
{
    class Program
    {
        static List<TaskItem> tasks = new List<TaskItem>();

        static void Main()
        {
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
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
        }
        static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("Список задач:");
            Console.WriteLine("1. Добавить задачу");
            Console.WriteLine("2. Обозначить задачу как выполненную");
            Console.WriteLine("3. Удалить задачу");
            Console.WriteLine("4. Просмотр задач");
            Console.WriteLine("5. Выход");
            Console.WriteLine("Выберите действие...");
        }
        static void AddTask()
        {
            Console.WriteLine("Введите описание задачи: ");
            string description = Console.ReadLine();
            tasks.Add(new TaskItem { Description = description, IsCompleted = false });
            Console.WriteLine("Задача добавлена!");
            Console.ReadLine();
        }
        static void MarkTaskCompleted()
        {
            Console.WriteLine("Введите номер задачи для отметки как выполненную: ");
            int taskNumber;
            if (int.TryParse(Console.ReadLine(), out taskNumber) && taskNumber > 0 && taskNumber <= tasks.Count)
            {
                tasks[taskNumber - 1].IsCompleted = true;
                Console.WriteLine("Задача отмечена как выполненная!");
            }
            else
            {
                Console.WriteLine("Неверный номер задачи.");
            }
            Console.ReadLine();


        }
        static void DeleteTask()
        {
            Console.WriteLine("Введите номер задачи для удаления: ");
            int taskNumber;
            if (int.TryParse(Console.ReadLine(), out taskNumber) && taskNumber > 0 && taskNumber <= tasks.Count)
            {
                tasks.RemoveAt(taskNumber - 1);
                Console.WriteLine("Задача удалена!");
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
                for (int i = 0; i < tasks.Count; i++)
                {
                    string status = tasks[i].IsCompleted ? "Выполнено" : "Не выполнено";
                    Console.WriteLine($"{i + 1}.{tasks[i].Description} - {status}");
                }
            }
            Console.WriteLine("Нажмите любую кнопку для продолжения...");
            Console.ReadKey();
        }

    }
    class TaskItem
    {
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}