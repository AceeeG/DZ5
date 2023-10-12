using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Exercises
{
    struct Student
    {
        public string surname;
        public string name;
        public int birth_year;
        public string exam;
        public int points;

        public void MakeStudentArr(string[] student_arr)
        {
            surname = student_arr[0];
            name = student_arr[1];
            bool flag = Int32.TryParse(student_arr[2], out birth_year);
            string exam = student_arr[3];
            bool flag2 = Int32.TryParse(student_arr[4], out points);
        }
    }
    
    internal class Program
    {
        static void ReadAllStudents(Dictionary<string, Student> students_dict)
        {
            for (int i = 0; i < 10; i++)
            {
                KeyValuePair<string, Student> student = students_dict.ElementAt(i);
                Console.WriteLine($"{student.Value.surname} {student.Value.name} {student.Value.birth_year} {student.Value.exam} {student.Value.points}");
            }
        }

        static void DoExercise2(string[] link)
        {
            Console.WriteLine("Задание 2 - словарь студентов\n");

            FileInfo student_file = new FileInfo(link[0]);
            Dictionary<string, Student> students_dict= new Dictionary<string, Student>();

            string[] student_arr = File.ReadAllLines(link[0]);
            if (student_file.Exists)
            {
                for (int i = 0; i < student_arr.Length; i++)
                {
                    string[] student_data = student_arr[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    Student student = new Student();

                    students_dict.Add(student_data[0] + student_data[1], student);
                }
                Console.WriteLine("Введите <Новый студент>, если хотите создать нового студента\n" +
                    "Введите <Удалить>, чтобы удалить студента\n" +
                    "Введите <Сортировать> чтобы отсортировать студентов по баллам\nВведите действие:\n");
                ReadAllStudents(students_dict);

                string choise = Console.ReadLine().ToLower();
                switch (choise)
                {
                    case "новый студент":
                        ReadAllStudents(students_dict);
                        Student student1 = new Student();
                        Console.WriteLine("Введите фамилию студента\n");
                        student1.surname = Console.ReadLine();
                        Console.WriteLine("Введите имя студента\n");
                        student1.name = Console.ReadLine();
                        Console.WriteLine("Введите год рождения студента\n");
                        bool birth_year_flag = Int32.TryParse(Console.ReadLine(), out student1.birth_year);
                        if (!birth_year_flag)
                        {
                            do
                            {
                                Console.WriteLine("Вы ввели не число, введите число\n");
                                birth_year_flag = Int32.TryParse(Console.ReadLine(), out student1.birth_year);
                            } while (birth_year_flag);
                        }
                        Console.WriteLine("Введите доп.экзамен(Информатика, Физика, Английский), который студент сдавал\n");
                        student1.exam = Console.ReadLine();
                        Console.WriteLine("Ввведите количество баллов по нему\n");
                        bool points_flag = Int32.TryParse(Console.ReadLine(), out student1.points);
                        if (!points_flag)
                        {
                            do
                            {
                                Console.WriteLine("Вы ввели не число, введите число\n");
                                points_flag = Int32.TryParse(Console.ReadLine(), out student1.points);
                            } while (points_flag);
                        }
                        students_dict.Add(student1.surname + student1.name, student1);
                        ReadAllStudents(students_dict);
                        break;
                    case "удалить":
                        Console.WriteLine("Введите фамилию и имя студента, которого нужно удалить\n");
                        string key = Console.ReadLine();
                        students_dict.Remove(key);
                        if (students_dict.Remove(key))
                        {
                            Console.WriteLine($"Студент {key} был удалён\n");
                        }
                        else
                        {
                            Console.WriteLine($"Студент {key} не найден");
                        }
                        break;
                    case "сортировать":
                        students_dict = students_dict.OrderBy(exam => exam.Value.exam).ToDictionary(exam => exam.Key, student => student.Value);
                        break;
                    default:
                        Console.WriteLine("Неверная команда");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Файл не найден");
            }

        }

        static void Main(string[] link)
        {
            DoExercise2(link);

            Console.ReadKey();
        }
    }
}
