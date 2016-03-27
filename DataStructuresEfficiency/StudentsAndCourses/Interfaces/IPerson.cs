namespace StudentsAndCourses.Interfaces
{
    using System;
    public interface IPerson : IComparable<IPerson>
    {
        string FirstName { get; }

        string LastName { get; }
    }
}
