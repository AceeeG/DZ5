using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;

namespace Exercises
{
    struct Student
    {
        public string surname;
        public string name;
        public int birth_year;
        public string exam;
        public int points;
    }

    struct Granny
    {
        public string name;
        public DateTime age;
        public string[] sickness;
        public string[] pills;

    }

    struct Hospital
    {
        public string name;
        public string[] sickness;
        public int capacity;
        public int max_capacity;
    }



    internal class Program
    {
        /// <summary>
        /// Перемешивает индексы и картинки
        /// </summary>
        /// <param name="images"></param>
        /// <param name="image_num"></param>
        static void MixingImages(List<Image> images, List<int> image_num)
        {
            Random rnd = new Random();
            for (int i = images.Count - 1; i >= 1; i--)
            {
                int j = rnd.Next(i + 1);
                Image img = images[j];
                int transition_num = image_num[j];
                images[j] = images[i];
                image_num[j] = image_num[i];
                images[i] = img;
                image_num[i] = transition_num;
            }
        }

        /// <summary>
        /// Выполняет упражнение 1
        /// </summary>
        static void DoExercise1()
        {
            List<Image> images = new List<Image>();
            List<int> image_num = new List<int>();

            for (int i = 0; i < 32; i++)
            {
                string img = "C:/Users/User/source/repos/DZ5/DZ5/Exercises/img/" + i.ToString() + ".png";
                images.Add(Image.FromFile(img));
                images.Add(Image.FromFile(img));
            }
            for(int j = 0; j < 64; j++)
            {
                image_num.Add(j);
            }
            MixingImages(images, image_num);
            Console.WriteLine("Номера до перешивания\n");
            foreach(int num in image_num)
            {
                Console.Write($"{num} ");
            }
            
            Console.WriteLine("\nНомера после перешивания\n");
            foreach (int num in image_num)
            {
                Console.Write($"{num} ");
            }
            



            
        }




        /// <summary>
        /// Выводит всех студентов на консоль
        /// </summary>
        /// <param name="students_dict"></param>
        static void ReadAllStudents(Dictionary<string, Student> students_dict)
        {
            for (int i = 0; i < 10; i++)
            {
                KeyValuePair<string, Student> student = students_dict.ElementAt(i);
                Console.WriteLine($"Фамилия: {student.Value.surname} Имя: {student.Value.name} Год рождения: {student.Value.birth_year} Экзамен: {student.Value.exam} Баллы: {student.Value.points}");
            }
        }


        /// <summary>
        /// Выполняет упражнение 2
        /// </summary>
        /// <param name="link"></param>
        static void DoExercise2(string[] link)
        {
            Console.WriteLine("Задание 2 - словарь студентов\n");

            FileInfo student_file = new FileInfo(link[0]);
            Dictionary<string, Student> students_dict= new Dictionary<string, Student>();

            List<string> student_list = File.ReadAllLines(link[0]).ToList();
            if (student_file.Exists)
            {
                foreach(string stud in student_list)
                {
                    string[] split_stud = stud.Split(new[] { " " }, StringSplitOptions.None);
                    string name = split_stud[0];
                    Student student = new Student();

                    students_dict.Add(split_stud[0] + split_stud[1], student);
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
                        ReadAllStudents(students_dict);
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

        /// <summary>
        /// Метод выводит информацию о больницах и бабушках
        /// </summary>
        /// <param name="granny_list"></param>
        /// <param name="hospitals"></param>
        static void ReadHospitalsAndGranny(List<Granny> granny_list, Stack<Hospital> hospitals)
        {
            Stack<Hospital> hospitals_stack = new Stack<Hospital>(hospitals);
            for (int i = 0; i < hospitals.Count; i++)
            {
                string sickness = "";
                Hospital hospital = hospitals_stack.Pop();
                for(int j = 0; j < hospital.sickness.Length; j++)
                {
                    sickness += $"{hospital.sickness[j]} ";
                    Console.WriteLine($"Название: {hospital.name}, Лечатся болезни: {sickness}, Заполненность: {hospital.capacity}, Макс. вместимость: {hospital.max_capacity}");
                }
               
            }
            for (int i = 0; i < granny_list.Count; i++)
            {
                string sickness = "";
                string pills = "";
                Granny granny = granny_list[i];
                for (int j = 0; j < granny.sickness.Length; j++)
                {
                    sickness += $"{granny.sickness[j]} ";
                }
                for (int n = 0; n < granny.pills.Length; n++)
                {
                    pills += $"{granny.pills[n]} ";
                }
                Console.WriteLine($"Имя: {granny.name}, Возраст: {granny.age}, Болезни: {sickness}, Таблетки: {pills}");
            }
        }

        /// <summary>
        /// Метод определяет в какую больницу попадет бабушка
        /// </summary>
        /// <param name="granny_queue"></param>
        /// <param name="hospitals"></param>
        /// <returns>true или false, означающие попала бабка в больницу или нет</returns>
        static bool GetToTheHospital(Queue<Granny> granny_queue, Stack<Hospital> hospitals)
        {
            int counter = 0;
            Granny granny = granny_queue.Dequeue();
            Stack<Hospital> hospitals_2 = new Stack<Hospital>();

            

            if(granny.sickness.Length == 0)
            {
                for(int i = 0; i < (hospitals.Count + hospitals_2.Count); i++)
                {
                    Hospital hospital = hospitals.Pop();
                    if(hospital.capacity < hospital.max_capacity)
                    {
                        hospital.capacity++;
                        hospitals.Push(hospital);
                        foreach (Hospital bad_hospital in hospitals_2)
                        {
                            hospitals.Push(bad_hospital);
                        }
                        ;
                    }

                    hospitals_2.Push(hospital);
                    return true;
                }   
            }
            else
            {
                for (int i = 0; i < (hospitals.Count + hospitals_2.Count); i++)
                {
                    Hospital hospital = hospitals.Pop();
                    foreach (string sickness in granny.sickness)
                    {
                        if (Array.IndexOf(hospital.sickness, sickness.ToLower()) != -1)
                        {
                            counter++;
                        }
                    }
                    if (counter / granny.sickness.Length > 0.5 && hospital.capacity < hospital.max_capacity)
                    {
                        hospital.capacity++;
                        hospitals.Push(hospital);
                        foreach(Hospital bad_hospital in hospitals_2)
                        {
                            hospitals.Push(bad_hospital);
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    hospitals_2.Push(hospital);
                    
                }
            }
            foreach (Hospital bad_hospitals in hospitals_2)
            {
                hospitals.Push(bad_hospitals);
            }
            return false;

        }
        static void DoExercise3()
        {
            Queue<Granny> granny_queue = new Queue<Granny>();
            List<Granny> granny_list = new List<Granny>();
            Stack<Hospital> hospitals = new Stack<Hospital>();

            Hospital hospital_1 = new Hospital();
            hospital_1.name = "RKB";
            hospital_1.sickness = new string[]{"орви", "артрит", "гайморит", "коронавирус"};
            hospital_1.capacity = 0;
            hospital_1.max_capacity = 2;

            Hospital hospital_2 = new Hospital();
            hospital_2.name = "KFU's hospital";
            hospital_2.sickness = new string[] { "орви", "туберкулез", "цироз печени", "пневмония" };
            hospital_2.capacity = 0;
            hospital_2.max_capacity = 5;
            int i = 0;


            Console.WriteLine("Задание 3 - работа с бабульками\nСоздайте 5 бабуль\n");
            do
            {
                ReadHospitalsAndGranny(granny_list, hospitals);
                Granny granny1 = new Granny();
                Console.WriteLine("Введите имя бабушки\n");
                granny1.name = Console.ReadLine();
                Console.WriteLine("Введите день, месяц, год рождения бабушки(xx.xx.xxxx)\n");
                bool age_flag = DateTime.TryParse(Console.ReadLine(), out granny1.age);
                if (!age_flag)
                {
                    do
                    {
                        Console.WriteLine("Вы ввели не целое число");
                        age_flag = DateTime.TryParse(Console.ReadLine(), out granny1.age);
                    } while (age_flag);
                }
                Console.WriteLine("Введите болезни бабушки(через пробел), если ей нужно только спросить нажмите ENTER\n");
                granny1.sickness = Console.ReadLine().ToLower().Split(new string[] { " ", ",", ", " }, StringSplitOptions.RemoveEmptyEntries);
                Console.Write("Введите лекарства, которые бабушка принимает(через пробел), если их нет нажмите ENTER\n");
                granny1.pills = Console.ReadLine().ToLower().Split(new string[] { " ", ",", ", " }, StringSplitOptions.RemoveEmptyEntries);

                bool res = GetToTheHospital(granny_queue, hospitals);
                if (res)
                {
                    Console.WriteLine($"Бабушка попала в больницу");
                    granny_list.Add(granny1);

                }
                else
                {
                    Console.WriteLine("Бабушка плачет на улицe\n");
                }
                i++;

            } while (i < 5);


        }

        static void Main(string[] link)
        {
            //DoExercise1();
            DoExercise2(link);
            //DoExercise3();

            Console.ReadKey();
        }
    }
}
