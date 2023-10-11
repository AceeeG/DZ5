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
                Console.WriteLine($"{student.Value.surname} {student.Value.name} {student.Value.birth_year} {student.Value.exam} {student.Value.points}\n");
            }
        }

        static void DoExercise2(string[] link)
        {
            Console.WriteLine("Задание 2 - словарь студентов\n");

            FileInfo student_file = new FileInfo(link[0]);
            Dictionary<string, Student> students_dict= new Dictionary<string, Student>();
            if (student_file.Exists)
            {
                string[] student_arr = File.ReadAllLines(link[0]);

                for(int i = 0; i < student_arr.Length; i++)
                {
                    string[] student_data = student_arr[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    Student student = new Student();

                    students_dict.Add(student_data[0] + student_data[1], student);
                }
                Console.WriteLine("Введите <Новый студент>, если хотите создать нового студента\n" +
                    "Введите <Удалить>, чтобы удалить студента\n" +
                    "Введите <Сортировать> чтобы отсортировать студентов по баллам\nВведите действие:\n");
                ReadAllStudents(students_dict);
            }

        }

        static void Main(string[] link)
        {
            DoExercise2(link);

            Console.ReadKey();
        }
    }
}
