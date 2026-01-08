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

            [cite_start]// Prüfung auf Duplikate (Logik-Prüfung im Manager) [cite: 220]
            if (persons.Any(p => p.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                                 p.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException($"Die Person '{firstName} {lastName}' existiert bereits im Adressbuch.");
            }

            Console.Write("Alter: ");
            string ageInput = Console.ReadLine();

            // Wir werfen einen Fehler, wenn das Format falsch ist, anstatt "else" zu nutzen
            if (!int.TryParse(ageInput, out int age))
            {
                throw new FormatException("Das Alter muss eine ganze Zahl sein.");
            }

            Console.Write("Straße: ");
            string street = Console.ReadLine() ?? "";
            Console.Write("Hausnummer: ");
            string number = Console.ReadLine() ?? "";
            Console.Write("Postleitzahl: ");
            string postalCode = Console.ReadLine() ?? "";

            Address address = new Address(street, number, postalCode);

            // Hier wird der Konstruktor aufgerufen -> Validierung von Person.cs greift
            if (type?.Trim().ToLower() == "l")
            {
                Console.Write("Fach: ");
                string subject = Console.ReadLine() ?? "";
                if (string.IsNullOrWhiteSpace(subject))
                    throw new ArgumentException("Das Fach darf nicht leer sein.");

                persons.Add(new Teacher(firstName, lastName, age, address, subject));
                Console.WriteLine("Lehrer erfolgreich hinzugefügt.");
            }
            else
            {
                persons.Add(new Person(firstName, lastName, age, address));
                Console.WriteLine("Person erfolgreich hinzugefügt.");
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
