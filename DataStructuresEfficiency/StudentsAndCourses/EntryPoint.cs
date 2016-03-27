namespace StudentsAndCourses
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Interfaces;

    public class EntryPoint
    {
        public static void Main()
        {
            string path = "..\\..\\students.txt";

            var studentsByCourses = FillItems(path);

            PrintItems(studentsByCourses);

            Console.WriteLine();
        }

        private static IDictionary<string, SortedSet<IPerson>> FillItems(string path)
        {
            IDictionary<string, SortedSet<IPerson>> studentsByCourses = new SortedDictionary<string, SortedSet<IPerson>>();
            StreamReader reader = new StreamReader(path);

            using (reader)
            {
                string line = reader.ReadLine();

                while (line != null)
                {
                    var lineTokens = line
                        .Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries)
                        .ToArray();

                    string studentFirstName = lineTokens[0].Trim();
                    string studentLastName = lineTokens[1].Trim();
                    string courseName = lineTokens[2].Trim();

                    var student = new Student(studentFirstName, studentLastName);

                    if (!studentsByCourses.ContainsKey(courseName))
                    {
                        studentsByCourses[courseName] = new SortedSet<IPerson>();
                    }

                    studentsByCourses[courseName].Add(student);

                    line = reader.ReadLine();
                }
            }

            return studentsByCourses;
        }

        private static void PrintItems(IDictionary<string, SortedSet<IPerson>> studentsByCourses)
        {
            foreach (KeyValuePair<string, SortedSet<IPerson>> studentsByCourse in studentsByCourses)
            {
                Console.WriteLine(string.Format("{0}: {1}", studentsByCourse.Key, string.Join(", ", studentsByCourse.Value)));
            }
        }
    }
}
