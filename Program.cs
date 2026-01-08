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
                // Menü anzeigen
                menu.ShowMainMenu();
                string input = Console.ReadLine() ?? "";

                // Hier beginnt der Schutzblock (Exception Handling)
                try
                {
                    running = menu.HandleUserInput(input);
                }
                catch (FormatException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[Format-Fehler]: {ex.Message}");
                    Console.ResetColor();
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[Werte-Fehler]: {ex.Message}"); // z.B. negatives Alter
                    Console.ResetColor();
                }
                catch (ArgumentException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[Eingabe-Fehler]: {ex.Message}"); // z.B. leerer Name
                    Console.ResetColor();
                }
                catch (InvalidOperationException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[Logik-Fehler]: {ex.Message}"); // z.B. Duplikat
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine($"[Unerwarteter Fehler]: {ex.Message}");
                    Console.ResetColor();
                }

                // Kurze Pause, damit der User den Fehler lesen kann
                if (running)
                {
                    Console.WriteLine("Drücke eine Taste um fortzufahren...");
                    Console.ReadKey();
                }
            }
        }
    }
}
