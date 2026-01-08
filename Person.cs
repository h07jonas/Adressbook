using System;

namespace AddressBook
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public Address Address { get; set; }

        public Person(string firstName, string lastName, int age, Address address)
        {
            // Validierung: Vorname darf nicht leer sein
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("Der Vorname darf nicht leer sein.", nameof(firstName));

            // Validierung: Nachname darf nicht leer sein
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentException("Der Nachname darf nicht leer sein.", nameof(lastName));

            // Validierung: Alter darf nicht negativ sein
            if (age < 0)
                throw new ArgumentOutOfRangeException(nameof(age), "Das Alter darf nicht negativ sein.");

            // (Optional) Adresse könnte auch geprüft werden, hier prüfen wir nur auf null
            if (address == null)
                throw new ArgumentNullException(nameof(address), "Die Adresse darf nicht fehlen.");

            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Address = address;
        }

        public virtual void PrintPerson()
        {
            Console.WriteLine($"{FirstName} {LastName}, {Age} Jahre - {Address}");
        }
    }
}
