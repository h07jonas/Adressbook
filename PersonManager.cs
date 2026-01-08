using System;
using System.Collections.Generic;
using System.Linq;

namespace AddressBook
{
    public class PersonManager
    {
        private List<Person> persons;

        public PersonManager()
        {
            persons = new List<Person>();
        }

        public void AddPerson()
        {
            Console.WriteLine("Möchten Sie eine Person (p) oder einen Lehrer (l) hinzufügen?");
            string? type = Console.ReadLine();

            Console.Write("Vorname: ");
            string firstName = Console.ReadLine() ?? "";
            Console.Write("Nachname: ");
            string lastName = Console.ReadLine() ?? "";
            Console.Write("Alter: ");

            if (int.TryParse(Console.ReadLine(), out int age))
            {
                Console.Write("Straße: ");
                string street = Console.ReadLine() ?? "";
                Console.Write("Hausnummer: ");
                string number = Console.ReadLine() ?? "";
                Console.Write("Postleitzahl: ");
                string postalCode = Console.ReadLine() ?? "";

                Address address = new Address(street, number, postalCode);

                if (type?.Trim().ToLower() == "l")
                {
                    Console.Write("Fach: ");
                    string subject = Console.ReadLine() ?? "";
                    persons.Add(new Teacher(firstName, lastName, age, address, subject));
                    Console.WriteLine("Lehrer hinzugefügt.");
                }
                else
                {
                    persons.Add(new Person(firstName, lastName, age, address));
                    Console.WriteLine("Person hinzugefügt.");
                }
            }
            else
            {
                Console.WriteLine("Ungültiges Alter.");
            }
        }

        public void ShowAllPersons()
        {
            if (persons.Count == 0)
            {
                Console.WriteLine("Keine Personen im Adressbuch.");
                return;
            }
            foreach (var person in persons)
            {
                person.PrintPerson();
            }
        }

        public void SearchPerson()
        {
            Console.Write("Name der gesuchten Person: ");
            string searchName = Console.ReadLine() ?? "";
            var result = persons.Where(p =>
                p.FirstName.Equals(searchName, StringComparison.OrdinalIgnoreCase) ||
                p.LastName.Equals(searchName, StringComparison.OrdinalIgnoreCase));

            if (!result.Any())
            {
                Console.WriteLine("Keine Person gefunden.");
            }
            else
            {
                foreach (var person in result)
                {
                    person.PrintPerson();
                }
            }
        }

        public void SearchByAddress()
        {
            Console.Write("Suche nach Straße oder PLZ: ");
            string searchTerm = Console.ReadLine() ?? "";
            var result = persons.Where(p =>
                p.Address.Street.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                p.Address.PostalCode.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));

            if (!result.Any())
            {
                Console.WriteLine("Keine Person mit dieser Adresse gefunden.");
            }
            else
            {
                foreach (var person in result)
                {
                    person.PrintPerson();
                }
            }
        }

        public void SortByFirstName()
        {
            persons = persons.OrderBy(p => p.FirstName).ToList();
            Console.WriteLine("Personen nach Vorname sortiert.");
            ShowAllPersons();
        }

        public void SortByLastName()
        {
            persons = persons.OrderBy(p => p.LastName).ToList();
            Console.WriteLine("Personen nach Nachname sortiert.");
            ShowAllPersons();
        }

        public void SortByAge()
        {
            persons = persons.OrderBy(p => p.Age).ToList();
            Console.WriteLine("Personen nach Alter sortiert.");
            ShowAllPersons();
        }
    }
}