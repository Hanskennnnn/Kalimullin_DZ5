using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace BabuliHospital
{
    struct Student
    {


        public string LastName;
        public string FirstName;
        public DateTime BirthDate;
        public string Exam;
        public int Score;

        public Student(string lastName, string firstName, DateTime birthDate, string exam, int score)
        {
            LastName = lastName;
            FirstName = firstName;
            BirthDate = birthDate;
            Exam = exam;
            Score = score;
        }
    }
    struct Babulia
    {
        public string Name;
        public DateTime BirthDate;
        public List<string> Illnesses;

        public Babulia(string name, DateTime birthDate, List<string> illnesses)
        {
            Name = name;
            BirthDate = birthDate;
            Illnesses = illnesses;
        }

        public string PrintInfo()
        {
            var today = DateTime.Today;

            // Calculate the age
            var age = today.Year - BirthDate.Year;

            // Adjust for leap years
            if (BirthDate.Date > today.AddYears(-age)) age--;
            string illnesses = Illnesses.Count > 0 ? string.Join(", ", Illnesses) : "Здорова (только спросить)";
            return $"Бабуля {Name}, {age} лет лечит болезни: {illnesses}";
        }
    }

    struct Hospital
    {
        public string Name;
        public List<string> TreatedIllnesses;
        public int Capacity;
        public List<Babulia> Patients;

        public Hospital(string name, List<string> treatedIllnesses, int capacity)
        {
            Name = name;
            TreatedIllnesses = treatedIllnesses;
            Capacity = capacity;
            Patients = new List<Babulia>();
        }

        public bool IsFull => Patients.Count >= Capacity;

        public double OccupancyRate => (double)Patients.Count / Capacity * 100;

        public void PrintPatients()
        {
            if (Patients.Count > 0)
            {
                foreach (var patient in Patients)
                {
                    Console.WriteLine($"    {patient.PrintInfo()}");
                }
            }
            else
            {
                Console.WriteLine("  Пациентов нет.");
            }
        }

        public override string ToString()
        {
            string illnesses = TreatedIllnesses.Count > 0 ? string.Join(", ", TreatedIllnesses) : "Нет специализаций";
            return $"{Name} (лечат: {illnesses}, вместимость: {Capacity}, занято: {Patients.Count} ({OccupancyRate:F1}%))";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Задание 1
            Task1();
            //Задание 2
            Task2();
            // Задание 3
            Task3();
        }
        static void Task1()
        {
            Console.WriteLine("\n\nЗадание 1");
            List<string> imagesList = new List<string>();

            for (int i = 1; i <= 32; i++)
            {
                string imageName = $"image{i}";
                imagesList.Add(imageName);
                imagesList.Add(imageName);
            }

            Console.WriteLine("Изначальный список:");
            PrintList(imagesList);

            MixingRandom(imagesList);

            Console.WriteLine("\nПеремешанный список:");
            PrintList(imagesList);
        }

        static void MixingRandom(List<string> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                string value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        static void PrintList(List<string> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {list[i]}");
            }
        }


        static void Task2()
        {
            Console.WriteLine("\n\n Задание 2");
            Dictionary<string, Student> students = new Dictionary<string, Student>(StringComparer.OrdinalIgnoreCase);
            const string filePath = "students.txt";

            void LoadStudentsFromFile()
            {
                if (File.Exists(filePath))
                {
                    var lines = File.ReadAllLines(filePath);
                    foreach (var line in lines)
                    {
                        var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length == 5) 
                        {
                            if (int.TryParse(parts[2], out int birthYear))
                            {
                                string exam = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(parts[3].ToLower());
                                int score = int.Parse(parts[4]);
                                string key = $"{parts[0]} {parts[1]}";

                                students[key] = new Student(
                                    parts[0],              
                                    parts[1],                    
                                    new DateTime(birthYear, 1, 1),
                                    exam,                         
                                    score                         
                                );
                            }
                            else
                            {
                                Console.WriteLine($"Ошибка: Неверный формат года рождения для студента {parts[0]} {parts[1]}.");
                            }
                        }
                    }
                    Console.WriteLine("Данные студентов загружены из файла.");
                }
                else
                {
                    Console.WriteLine("Файл не найден. Список студентов пуст.");
                }
            }

            void SaveStudentsToFile()
            {
                var lines = students.Select(s => $"{s.Value.LastName} {s.Value.FirstName} {s.Value.BirthDate:yyyy} {s.Value.Exam} {s.Value.Score}");
                File.WriteAllLines(filePath, lines);
                Console.WriteLine("Данные студентов сохранены в файл.");
            }

            void AddNewStudent()
            {
                Console.Write("Введите фамилию: ");
                string lastName = Console.ReadLine();
                Console.Write("Введите имя: ");
                string firstName = Console.ReadLine();

                DateTime birthDate = DateTime.Now;
                bool validBirthDate = false;
                while (!validBirthDate)
                {
                    Console.Write("Введите год рождения (только год): ");
                    validBirthDate = int.TryParse(Console.ReadLine(), out int year);
                    if (validBirthDate)
                    {
                        birthDate = new DateTime(year, 1, 1);
                    }
                    else
                    {
                        Console.WriteLine("Неверный формат года. Попробуйте снова.");
                    }
                }

                Console.Write("Введите экзамен: ");
                string exam = Console.ReadLine();

                int score = 0;
                bool validScore = false;
                while (!validScore)
                {
                    Console.Write("Введите баллы: ");
                    validScore = int.TryParse(Console.ReadLine(), out score);
                    if (!validScore)
                    {
                        Console.WriteLine("Неверный формат баллов. Пожалуйста, введите число.");
                    }
                }

                string key = $"{lastName} {firstName}".ToLower();
                if (students.ContainsKey(key))
                {
                    Console.WriteLine("Студент с таким именем уже существует.");
                }
                else
                {
                    students[key] = new Student(lastName, firstName, birthDate, exam, score);
                    Console.WriteLine("Студент добавлен.");
                }
            }

            void DeleteStudent()
            {
                Console.Write("Введите фамилию студента: ");
                string lastName = Console.ReadLine().ToLower();
                Console.Write("Введите имя студента: ");
                string firstName = Console.ReadLine().ToLower();

                string key = $"{lastName} {firstName}".ToLower();
                if (students.Remove(key))
                {
                    Console.WriteLine("Студент удалён.");
                }
                else
                {
                    Console.WriteLine("Студент не найден.");
                }
            }

            void SortStudents()
            {
                var sortedStudents = students.Values.OrderBy(s => s.Score).ToList();
                Console.WriteLine("Список студентов отсортирован по баллам:");

                foreach (var student in sortedStudents)
                {
                    Console.WriteLine($"{student.LastName} {student.FirstName} " +
                                      $"{student.BirthDate:yyyy} {student.Exam} {student.Score}");
                }
            }

            LoadStudentsFromFile();

            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Новый студент");
                Console.WriteLine("2. Удалить");
                Console.WriteLine("3. Сортировать");
                Console.WriteLine("4. Выход");
                Console.Write("Выберите действие: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddNewStudent();
                        break;
                    case "2":
                        DeleteStudent();
                        break;
                    case "3":
                        SortStudents();
                        break;
                    case "4":
                        SaveStudentsToFile();
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор! Попробуйте снова.");
                        break;
                }
            }
        }

        static void Task3()
        {
            Console.WriteLine("\n Задание 3");
            Queue<Babulia> babulias = new Queue<Babulia>();
            List<Hospital> hospitals = new List<Hospital>
            {
                new Hospital("Больница №1", new List<string> { "Грипп", "Кашель", "Ангина" }, 5),
                new Hospital("Больница №2", new List<string> { "Сахарный диабет", "Гипертония" }, 3),
                new Hospital("Больница №3", new List<string> { "Астма", "Грипп" }, 4)
            };

            // Ввод бабуль
            Console.WriteLine("Введите информацию о бабулях (каждая бабуля на отдельной строке).");
            Console.WriteLine("Формат: Имя, Дата рождения (гггг.мм.дд), Болезнь1, Болезнь2, ...");
            Console.WriteLine("Для завершения ввода оставьте строку пустой.\n");

            while (true)
            {
                Console.Write("Введите данные о бабуле через пробел: ");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) break;

                string[] parts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToArray();
                if (parts.Length >= 2)
                {
                    string name = parts[0];
                    if (DateTime.TryParse(parts[1], out DateTime birthDate))
                    {
                        List<string> illnesses = parts.Length > 2 ? parts.Skip(2).Select(x => x.ToLowerInvariant()).ToList() : new List<string>();
                        babulias.Enqueue(new Babulia(name, birthDate, illnesses));

                        Console.WriteLine("\nСостояние больниц после ввода бабули:");
                        foreach (var hospital in hospitals)
                        {
                            Console.WriteLine(hospital);
                            hospital.PrintPatients();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Некорректный формат даты рождения. Пожалуйста, используйте формат гггг.мм.дд.");
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный ввод. Убедитесь, что вы ввели имя и дату рождения.");
                }
            }


            while (babulias.Count > 0)
            {
                Babulia currentBabulia = babulias.Dequeue();
                bool admitted = false;

                foreach (var hospital in hospitals)
                {
                    if (!hospital.IsFull && hospital.TreatedIllnesses.Any(illness => currentBabulia.Illnesses.Contains(illness)))
                    {
                        hospital.Patients.Add(currentBabulia);
                        admitted = true;
                        Console.WriteLine($"Бабуля {currentBabulia.Name} была принята в {hospital.Name}.");
                        break; 
                    }
                }

                if (!admitted)
                {
                    Console.WriteLine($"Бабуля {currentBabulia.Name} не была принята ни в одну больницу из-за отсутствия мест или неподходящих болезней.");
                }

                Console.WriteLine("\nСостояние больниц после приема бабули:");
                foreach (var hospital in hospitals)
                {
                    Console.WriteLine(hospital);
                    hospital.PrintPatients();
                }
            }

          
        }
    }
}