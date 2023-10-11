using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace DZ5
{
    internal class Program
    {

        /// <summary>
        /// Метод подсчитывающий кол-во гласных и согласных в файле
        /// </summary>
        /// <param name="text"></param>
        /// <returns>Картеж счетчик</returns>
        static (int, int) CountVowelsAndConsotanants(char[] text)
        {
            const string eng_vowels = "aeiouy";
            (int, int) counter = (0, 0);
            for(int i  = 0; i < text.Length; i++)
            {
                if (eng_vowels.Contains(char.ToLower(text[i])))
                {
                    counter.Item1++;
                }
                else
                {
                    counter.Item2++;
                }
            }
            return counter;
        }
        /// <summary>
        /// Выполняет упражнение 1
        /// </summary>
        /// <param name="link"></param>
        static void DoExercise1(string[] link)
        {
            Console.WriteLine("Упражнение 1 - подсчитывает кол-во гласных и согласных из текстогово файла\n");

            FileInfo userFile = new FileInfo(link[0]);

            if (userFile.Exists)
            {
                char[] text_array = File.ReadAllText(link[0]).ToUpper().ToCharArray();

                (int, int) counter = CountVowelsAndConsotanants(text_array);

                Console.WriteLine($"В файле гласных букв: {counter.Item1}, согласных букв: {counter.Item2}\n");
            }
            else
            {
                Console.WriteLine("Файл не найден\n");
            }

        }
        /// <summary>
        /// Умножает матрицы второго порядка
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns>Двумерный массив - матрицу</returns>
        static int[,] MultiplyOfMatrix(int[,] matrix1, int[,] matrix2)
        {
            int[,] result = new int[2, 2];
            for(int i = 0; i < 2; i++)
            {
                for(int j = 0; j < 2; j++)
                {
                    result[i, j] = matrix1[i, 0] * matrix2[0, j] + matrix1[i, 1] * matrix2[1, j];
                }
            }
            return result;
        }

        /// <summary>
        /// Выводит матрицу на консоль
        /// </summary>
        /// <param name="matrix"></param>
        static void ReadMatrix(int[,] matrix)
        {
            int row = matrix.GetLength(0);
            int column = matrix.GetLength(1);

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
            }
        }

        /// <summary>
        /// Выполняет упражнение 2
        /// </summary>
        static void DoExercise2()
        {
            Console.WriteLine("Упражнение 2 - перемножение матриц\n");
            Random rnd = new Random();
            int[,] matrix1 = new int[2, 2];
            int[,] matrix2 = new int[2, 2];
            int[,] result;

            for(int i = 0; i <= 1; i++)
            {
                for(int j = 0; j <= 1; j++)
                {
                    matrix1[i, j] = rnd.Next(100);
                    matrix2[i, j] = rnd.Next(100);
                }
            }
            result = MultiplyOfMatrix(matrix1 , matrix2);
            ReadMatrix(matrix1);
            Console.Write(" умноженная на ");
            ReadMatrix(matrix2);
            Console.Write(" равна матрица ");
            ReadMatrix(result);
            Console.WriteLine("\n");

        }

        /// <summary>
        /// Считает среднюю температуру месяца
        /// </summary>
        /// <param name="temp"></param>
        /// <returns>Число - средняя температура</returns>
        static double[] CalculateAverageTemperature(int[,] temp)
        {
            int sum = 0;
            double[] month_avg = new double[12];
            for(int i = 0; i < 12; i++)
            {
                sum = 0;
                for(int j = 0;  j < 30; j++)
                {
                    sum += temp[i, j];
                }
                month_avg[i] = sum / 30;
            }
            return month_avg;
        }

        /// <summary>
        /// Выполняет упражнение 3
        /// </summary>
        static void DoExercise3()
        {
            Console.WriteLine("Упражнение 3 - считаем и выводим среднюю температуру за месяц");

            Random rnd = new Random();
            int[,] temp = new int[12, 30];
            for(int i = 0; i < 12; i++)
            {
                for(int j = 0; j < 30; j++)
                {
                    temp[i, j] = rnd.Next(-50, 50);
                }
            }
            double[] month_avg = CalculateAverageTemperature(temp);
            Array.Sort(month_avg);
            for(int i = 0; i < 12; i++)
            {
                Console.WriteLine($"Средняя температура в месяце {month_avg[i]}");
            }
            Console.WriteLine("");
        }

        /// <summary>
        /// Метод подсчитывающий кол-во гласных и согласных в файле
        /// </summary>
        /// <param name="text_list"></param>
        /// <returns>Картеж счетчик</returns>
        static (int, int) CountVowelsAndConsotanants(List<char> text_list)
        {
            const string eng_vowels = "aeiouy";
            (int, int) counter = (0, 0);
            foreach(char letter in text_list)
            {
                if (eng_vowels.Contains(char.ToLower(letter)))
                {
                    counter.Item1++;
                }
                else
                {
                    counter.Item2++;
                }
            }
            return counter;
        }

        /// <summary>
        /// Выполняет домашнее задание 1
        /// </summary>
        /// <param name="link"></param>
        static void DoHomeWork1(string[] link)
        {
            Console.WriteLine("Домашнее задание 1 - подсчитываем кол-во гласных и согласных в файле");

            FileInfo userFile = new FileInfo(link[0]);

            if (userFile.Exists)
            {
                List <char> text_array = new List<char>(File.ReadAllText(link[0]).ToUpper().ToCharArray());

                (int, int) counter = CountVowelsAndConsotanants(text_array);

                Console.WriteLine($"В файле гласных букв: {counter.Item1}, согласных букв: {counter.Item2}\n");
            }
            else
            {
                Console.WriteLine("Файл не найден\n");
            }
        }

        /// <summary>
        /// Перемножает матрицы
        /// </summary>
        /// <param name="matrix1"></param>
        /// <param name="matrix2"></param>
        /// <returns>Результат умножения двух матриц</returns>
        static LinkedList<int> MultiplyOfMatrix(LinkedList <int> matrix1, LinkedList <int> matrix2)
        {
            LinkedList<int> result = new LinkedList<int>();
            
            result.AddFirst(matrix1.First.Value * matrix2.First.Value + matrix1.First.Next.Value * matrix2.First.Next.Value);
            result.AddLast(matrix1.First.Value * matrix2.First.Next.Value + matrix1.Last.Previous.Value * matrix2.Last.Value);
            result.AddLast(matrix1.Last.Previous.Value * matrix2.First.Value + matrix1.Last.Value * matrix2.Last.Previous.Value);
            result.AddLast(matrix1.Last.Previous.Value * matrix2.First.Next.Value + matrix1.Last.Value * matrix2.Last.Value);

            return result;
        }

        /// <summary>
        /// Выводит матрицу на консоль
        /// </summary>
        /// <param name="matrix"></param>
        static void ReadMatrix(LinkedList<int> matrix)
        {
            int count = 0;
            foreach (int number in matrix)
            {
                Console.Write(number + " ");
                count++;
                if (count == 2)
                {
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Выполняет домашнее задание 2
        /// </summary>
        static void DoHomeWork2()
        {
            Console.WriteLine("Домашнее задание 2 - перемножение матриц\n");
            Random rnd = new Random();
            LinkedList<int> matrix1 = new LinkedList<int>();
            LinkedList<int> matrix2 = new LinkedList<int>();
            LinkedList<int> result = new LinkedList<int>();

            matrix1.AddFirst(rnd.Next(100));
            matrix2.AddFirst(rnd.Next(100));

            for (int i = 0; i < 3; i++)
            {
                matrix1.AddAfter(matrix1.First, rnd.Next(100));
                matrix2.AddAfter(matrix2.First, rnd.Next(100));
            }
            result = MultiplyOfMatrix(matrix1, matrix2);
            ReadMatrix(matrix1);
            Console.Write(" умноженная на \n");
            ReadMatrix(matrix2);
            Console.Write(" равна матрица \n");
            ReadMatrix(result);
            Console.WriteLine("\n");
        }

        /// <summary>
        /// Вычисляет среднюю температуру за месяц
        /// </summary>
        /// <param name="year"></param>
        /// <returns>Словарь со средними значениями</returns>
        static Dictionary<string, double> CalculateAverageTemperature(Dictionary<string, int[]> year)
        {
            Dictionary<string, double> month_avg = new Dictionary<string, double>();

            foreach (var month in year)
            {
                int sum = 0;
                for (int i = 0; i < month.Value.Length; i++)
                {
                    sum += month.Value[i];
                }

                month_avg.Add(month.Key, sum / month.Value.Length);
            }
            return month_avg;
        }

        static void ReadMonthAvg(Dictionary<string, double> month_avg)
        {
            foreach(var month in month_avg)
            {
                Console.WriteLine($"В {month.Key} средняя температура была {month.Value:F}");
            }
        }
        

        /// <summary>
        /// Выполняет домашнее задание 3
        /// </summary>
        static void DoHomeWork3()
        {
            Random rnd = new Random();

            Console.WriteLine("Домашнее задание 3 - считаем среднюю температуру за месяц");

            Dictionary<string, double> month_average = new Dictionary<string, double>();
            Dictionary<string, int[]> year = new Dictionary<string, int[]>();

            for(int i = 0; i < 12;  i++)
            {
                int[] month = new int[30];
                for(int j = 0; j < 30; j++)
                {
                    month[j] = rnd.Next(-50, 50);
                }
                switch(i)
                {
                    case 0:
                        year.Add("Январь", month);
                        break;
                    case 1:
                        year.Add("Февраль", month);
                        break;
                    case 2:
                        year.Add("Март", month);
                        break;
                    case 3:
                        year.Add("Апрель", month);
                        break;
                    case 4:
                        year.Add("Май", month);
                        break;
                    case 5:
                        year.Add("Июнь", month);
                        break;
                    case 6:
                        year.Add("Июль", month);
                        break;
                    case 7:
                        year.Add("Август", month);
                        break;
                    case 8:
                        year.Add("Сентябрь", month);
                        break;
                    case 9:
                        year.Add("Октябрь", month);
                        break;
                    case 10:
                        year.Add("Ноябрь", month);
                        break;
                    case 11:
                        year.Add("Декабрь", month);
                        break;
                }
            }
            month_average = CalculateAverageTemperature(year);
            ReadMonthAvg(month_average);

        }

        

        static void Main(string[] link)
        {
            DoExercise1(link);
            DoExercise2();
            DoExercise3();
            DoHomeWork1(link);
            DoHomeWork2();
            DoHomeWork3();


            Console.ReadKey();

        }
    }
}
