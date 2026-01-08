using System;

namespace AddressBook
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PersonManager personManager = new PersonManager();
            Menu menu = new Menu(personManager);
            bool running = true;

            while (running)
            {
                menu.ShowMainMenu();
                string input = Console.ReadLine() ?? "";
                running = menu.HandleUserInput(input);
            }
        }
    }
}