namespace StudentsAndCourses
{
    using System;
    using Interfaces;

    public class Student : IPerson, IComparable<IPerson>
    {
        public Student(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", this.FirstName, this.LastName);
        }

        public int CompareTo(IPerson other)
        {
            if (this.LastName.CompareTo(other.LastName) == 0)
            {
                return this.FirstName.CompareTo(other.FirstName);
            }

            return this.LastName.CompareTo(other.LastName);
        }
    }
}
