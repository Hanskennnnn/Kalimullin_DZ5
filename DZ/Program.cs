using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

namespace DZ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Упражнение 6.1
            Console.WriteLine("Задание 6.1");
            char[] textFile = null;

            if (args.Length > 0)
            {
                string fileName = args[0];
                if (File.Exists(fileName))
                {
                    try
                    {
                        textFile = File.ReadAllText(fileName).ToCharArray();
                        GetVowelsAndConsonants(textFile);
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine($"Ошибка при работе с файлом: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine($"Файл {fileName} не найден.");
                }
            }
            else
            {
                Console.WriteLine("Укажите имя файла в качестве аргумента командной строки.");
            }

            if (textFile != null)
            {
                GetVowelsAndConsonants(textFile);
            }


            //Упражнение 6.2
            Task6_2();
            //Упражнение 6.3
            Task6_3();
            //Лабораторная работа 6.2
            Task6_5();
            //Лабораторная 6.3
            Task6_6();

        }
        static void Task6_3()
        {
            Console.WriteLine("\n\nУпражнение 6.3");
            Random rand = new Random();
            float[,] temperatures = new float[12, 30];

            for (int i = 0; i < temperatures.GetLength(0); i++)
            {
                for (int j = 0; j < temperatures.GetLength(1); j++)
                {
                    temperatures[i, j] = rand.Next(-30, 30);
                }

            }
            double[] avgTemps = GetAveragesPerMonth(temperatures);
            for (int i = 0; i < avgTemps.Length; i++)
            {
                Console.WriteLine($"Номер месяца:{i + 1} - средняя температура {avgTemps[i]}.");
            }
            Array.Sort(avgTemps);

            Console.WriteLine("\nОтсортированный массив средних температур:");
            foreach (double avgTemp in avgTemps)
            {
                Console.Write($"{avgTemp:F2} °C ");
            }
            Console.WriteLine();
        }
        static double[] GetAveragesPerMonth(float[,] array)
        {
            double[] averageTemperatures = new double[12];
            float sum = 0;
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    sum += array[i, j];
                    

                }
                averageTemperatures[i] =  sum / array.GetLength(1);
            }
            return averageTemperatures;
        }
        static void Task6_2()
        {
            Console.WriteLine("\n\nЗадание 6.2");

            double[,] matrix1 = { { 1, 2, 3 }, { 4, 5, 6 } };
            double[,] matrix2 = { { 7, 8 }, { 9, 10 }, { 11, 12 } };

            Console.WriteLine("Матрица A:");
            PrintMatrix(matrix1);
            Console.WriteLine("\nМатрица B:");
            PrintMatrix(matrix2);

            try
            {
                double[,] result = MultiplyMatrices(matrix1, matrix2);
                Console.WriteLine("\nРезультат умножения (A * B):");
                PrintMatrix(result);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("\nОшибка: " + ex.Message);
            }



            double[,] matrix3 = { { 1, 2, 3 }, { 3, 4, 3 } };
            double[,] matrix4 = { { 5, 6 }, { 8, 9 } };
            Console.WriteLine("Матрица А1:");
            PrintMatrix(matrix3);
            Console.WriteLine("Матрица А2:");
            PrintMatrix(matrix4);

            try
            {
                double[,] result = MultiplyMatrices(matrix3, matrix4);
                Console.WriteLine("\nРезультат умножения (A * B):");
                PrintMatrix(result);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("\nОшибка: " + ex.Message);
            }
        }



        static void GetVowelsAndConsonants(char[] chars)
        {
            string vowels = "уеыаоэяиюё";
            string consonants = "йцкнгшщзхфвпрлджчсмтб";
            int vowelsCount = 0;
            int consonantsCount = 0;

            foreach (char c in chars)
            {
                if (vowels.Contains(char.ToLower(c)))
                {
                    vowelsCount++;
                }
                else if (consonants.Contains(char.ToLower(c)))
                {
                    consonantsCount++;
                }
            }

            Console.WriteLine($"Гласные: {vowelsCount}");
            Console.WriteLine($"Согласные: {consonantsCount}");
        }
        public static void PrintMatrix(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write($"{matrix[i, j],8:F2}");
                }
                Console.WriteLine();
            }
        }
        public static double[,] MultiplyMatrices(double[,] matrixA, double[,] matrixB)
        {
            int rowsA = matrixA.GetLength(0);
            int colsA = matrixA.GetLength(1);
            int rowsB = matrixB.GetLength(0);
            int colsB = matrixB.GetLength(1);
            if (colsA != rowsB)
            {
                throw new ArgumentException("Матрицы несовместимы для умножения.");
            }
            double[,] resultMatrix = new double[rowsA, colsB];
            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < colsB; j++)
                {
                    for (int k = 0; k < colsA; k++)
                    {
                        resultMatrix[i, j] += matrixA[i, k] * matrixB[k, j];
                    }
                }
            }
            return resultMatrix;
        }


        public static void Task6_5()
        {
            Console.WriteLine("\n\nЗадание 6.2 Лаба");
            // Пример инициализации двух матриц
            LinkedList<LinkedList<int>> matrixA = new LinkedList<LinkedList<int>>();
            matrixA.AddLast(new LinkedList<int>(new[] { 1, 2, 3 }));
            matrixA.AddLast(new LinkedList<int>(new[] { 4, 5, 6 }));

            LinkedList<LinkedList<int>> matrixB = new LinkedList<LinkedList<int>>();
            matrixB.AddLast(new LinkedList<int>(new[] { 7, 8 }));
            matrixB.AddLast(new LinkedList<int>(new[] { 9, 10 }));
            matrixB.AddLast(new LinkedList<int>(new[] { 11, 12 }));

            Console.WriteLine("Матрица A:");
            PrintMatrix(matrixA);

            Console.WriteLine("Матрица B:");
            PrintMatrix(matrixB);

            // Умножение матриц
            LinkedList<LinkedList<int>> resultMatrix = MultiplyMatrices(matrixA, matrixB);

            Console.WriteLine("Результат умножения матриц:");
            PrintMatrix(resultMatrix);
        }

        static void PrintMatrix(LinkedList<LinkedList<int>> matrix)
        {
            foreach (var row in matrix)
            {
                Console.WriteLine(string.Join(" ", row));
            }
            Console.WriteLine();
        }

        public static LinkedList<LinkedList<int>> MultiplyMatrices(LinkedList<LinkedList<int>> matrixA, LinkedList<LinkedList<int>> matrixB)
        {
            int rowsA = matrixA.Count;
            int colsA = matrixA.First.Value.Count;
            int rowsB = matrixB.Count;
            int colsB = matrixB.First.Value.Count;

           
            if (colsA != rowsB)
            {
                throw new InvalidOperationException("Количество столбцов первой матрицы должно быть равно количеству строк второй матрицы.");
            }

          
            LinkedList<LinkedList<int>> resultMatrix = new LinkedList<LinkedList<int>>();

            for (int i = 0; i < rowsA; i++)
            {
                var row = new LinkedList<int>();
                for (int j = 0; j < colsB; j++)
                {
                    row.AddLast(0);
                }
                resultMatrix.AddLast(row);
            }

         
            int rowIndex = 0;
            foreach (var rowA in matrixA)
            {
                int colIndex = 0;
                foreach (var colB in GetColumns(matrixB))
                {
                    int sum = 0;
                    int index = 0;
                    foreach (var valueA in rowA)
                    {
                        sum += valueA * colB.ElementAt(index);
                        index++;
                    }

                   
                    var resultRow = resultMatrix.ElementAt(rowIndex);

                   
                    int currentIndex = 0;
                    foreach (var item in resultRow)
                    {
                        if (currentIndex == colIndex)
                        {
                            resultRow.Remove(item); 
                            resultRow.AddLast(sum); 
                            break;
                        }
                        currentIndex++;
                    }
                    colIndex++;
                }
                rowIndex++;
            }

            return resultMatrix;
        }

        public static IEnumerable<LinkedList<int>> GetColumns(LinkedList<LinkedList<int>> matrix)
        {
            int cols = matrix.First.Value.Count;

            for (int j = 0; j < cols; j++)
            {
                var column = new LinkedList<int>();
                foreach (var row in matrix)
                {
                    column.AddLast(row.ElementAt(j));
                }

                yield return column;
            }
        }
        static void Task6_6()
        {
            Console.WriteLine("\n\nЛабораторная 6.3");
            // Получение массива температур для каждого месяца
            Dictionary<string, double[]> monthlyTemperatures = GenerateRandomMonthlyTemperatures();

            // Вывод температур для каждого месяца
            Console.WriteLine("Температуры по месяцам:");
            foreach (var month in monthlyTemperatures)
            {
                Console.WriteLine($"{month.Key}: {string.Join(", ", month.Value)}");
            }

            // Вычисление и сортировка средних температур по возрастанию
            var monthlyAverages = new List<KeyValuePair<string, double>>();

            foreach (var month in monthlyTemperatures)
            {
                double average = CalculateAverage(month.Value);
                monthlyAverages.Add(new KeyValuePair<string, double>(month.Key, average));
            }

            // Сортировка по средним температурам
            monthlyAverages.Sort((x, y) => x.Value.CompareTo(y.Value));

            // Вывод отсортированных средних температур
            Console.WriteLine("\nОтсортированные средние температуры:");
            foreach (var avg in monthlyAverages)
            {
                Console.WriteLine($"{avg.Key}: {avg.Value:F2}°C");
            }
        }

        // Метод для генерации случайных температур по каждому месяцу
        static Dictionary<string, double[]> GenerateRandomMonthlyTemperatures()
        {

            string[] monthNames = { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь",
                                "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };


            Dictionary<string, double[]> monthlyTemperatures = new Dictionary<string, double[]>();

            Random random = new Random();

   
            for (int month = 0; month < 12; month++)
            {
                double[] dailyTemperatures = new double[30];

          
                for (int day = 0; day < 30; day++)
                {
                    dailyTemperatures[day] = random.Next(-30, 30); 
                }

                monthlyTemperatures[monthNames[month]] = dailyTemperatures;
            }

            return monthlyTemperatures;
        }

        // Метод для вычисления средней температуры
        static double CalculateAverage(double[] temperatures)
        {
            double sum = 0;
            foreach (var temp in temperatures)
            {
                sum += temp;
            }
            return sum / temperatures.Length;
        }
    }
}

