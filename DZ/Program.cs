using System;
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

        }
        static void Task6_3()
        {
            Console.WriteLine("Упражнение 6.3");
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
            Console.WriteLine("Задание 6.2");

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
    }
}
