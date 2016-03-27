using System;
using System.Collections.Generic;
using Wintellect.PowerCollections;

public class PersonCollection : IPersonCollection
{
    private IDictionary<string, Person> personsByEmail;
    private IDictionary<string, SortedSet<Person>> personsByEmailDomain;
    private IDictionary<string, SortedSet<Person>> personsByNameAndTown;
    private OrderedDictionary<int, SortedSet<Person>> personsByAge;
    private IDictionary<string, OrderedDictionary<int, SortedSet<Person>>> personsByTownAndAge;

    public PersonCollection()
    {
        this.personsByEmail = new Dictionary<string, Person>();
        this.personsByEmailDomain = new Dictionary<string, SortedSet<Person>>();
        this.personsByNameAndTown = new Dictionary<string, SortedSet<Person>>();
        this.personsByAge = new OrderedDictionary<int, SortedSet<Person>>();
        this.personsByTownAndAge = new Dictionary<string, OrderedDictionary<int, SortedSet<Person>>>();
    }

    public int Count
    {
        get { return this.personsByEmail.Count; }
    }

    public bool AddPerson(string email, string name, int age, string town)
    {
        if (this.FindPerson(email) != null)
        {
            return false;
        }

        Person person = new Person()
        {
            Email = email,
            Name = name,
            Age = age,
            Town = town
        };

        this.personsByEmail.Add(email, person);

        var emailDomein = this.ExtractEmailDomain(email);
        this.personsByEmailDomain.AppendValueToKey(emailDomein, person);

        string nameAndTown = this.CombineNameAndTown(name, town);
        this.personsByNameAndTown.AppendValueToKey(nameAndTown, person);

        this.personsByAge.AppendValueToKey(age, person);

        this.personsByTownAndAge.EnsureKeyExists(town);
        this.personsByTownAndAge[town].AppendValueToKey(age, person);

        return true;
    }

    public Person FindPerson(string email)
    {
        Person person = null;

        bool personExist = this.personsByEmail.TryGetValue(email, out person);

        return person;
    }

    public bool DeletePerson(string email)
    {
        Person person = this.FindPerson(email);

        if (this.FindPerson(email) == null)
        {
            return false;
        }

        bool personDeleted = this.personsByEmail.Remove(email);

        var emailDomein = this.ExtractEmailDomain(email);
        this.personsByEmailDomain[emailDomein].Remove(person);

        string nameAndTown = this.CombineNameAndTown(person.Name, person.Town);
        this.personsByNameAndTown[nameAndTown].Remove(person);

        this.personsByAge[person.Age].Remove(person);

        this.personsByTownAndAge[person.Town][person.Age].Remove(person);

        return personDeleted;
    }

    public IEnumerable<Person> FindPersons(string emailDomain)
    {
        return this.personsByEmailDomain.GetValuesForKey(emailDomain);
    }

    public IEnumerable<Person> FindPersons(string name, string town)
    {
        string nameAndTown = this.CombineNameAndTown(name, town);

        return this.personsByNameAndTown.GetValuesForKey(nameAndTown);
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge)
    {
        var personsInRange = this.personsByAge.Range(startAge, true, endAge, true);

        foreach (KeyValuePair<int, SortedSet<Person>> personsByAge in personsInRange)
        {
            foreach (Person person in personsByAge.Value)
            {
                yield return person;
            }
        }
    }

    public IEnumerable<Person> FindPersons(
        int startAge, int endAge, string town)
    {
        if (!this.personsByTownAndAge.ContainsKey(town))
        {
            yield break;
        }

        var personsInRange = this.personsByTownAndAge[town].Range(startAge, true, endAge, true);

        foreach (KeyValuePair<int, SortedSet<Person>> personsByAge in personsInRange)
        {
            foreach (Person person in personsByAge.Value)
            {
                yield return person;
            }
        }
    }

    private string ExtractEmailDomain(string email)
    {
        var domain = email.Split('@')[1];

        return domain;
    }

    private string CombineNameAndTown(string name, string town)
    {
        const string separator = "|!|";

        return name + separator + town;
    }
}
