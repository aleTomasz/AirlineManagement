using AirlineManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement
{
    internal class Program
    {
        static List<Pilot> pilots = new List<Pilot>();
        static List<FlightAttendant> attendants = new List<FlightAttendant>();
        static List<Flight> flights = new List<Flight>();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Witaj w AirlineManagement");
                Console.WriteLine("1. Dodaj pilota");
                Console.WriteLine("2. Dodaj stewarda");
                Console.WriteLine("3. Utwórz lot");
                Console.WriteLine("4. Sprawdź gotowość lotu do startu");
                Console.WriteLine("5. Wyjdź");
                Console.Write("Wybierz opcję: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        pilots.Add(CreatePilot());
                        break;
                    case 2:
                        attendants.Add(CreateFlightAttendant());
                        break;
                    case 3:
                        flights.Add(CreateFlight(pilots, attendants));
                        break;
                    case 4:
                        CheckFlightReadiness();
                        break;
                    case 5:
                        return;  // Wyjście z programu
                    default:
                        Console.WriteLine("Niepoprawny wybór.");
                        break;
                }
            }
        }

        private static Pilot CreatePilot()
        {
            string name;
            DateTime birthDate;
            bool isCaptain;

            // Walidacja imienia
            while (true)
            {
                Console.Write("Podaj imię pilota: ");
                name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    break;
                }
                Console.WriteLine("Błąd: Imię nie może być puste.");
            }

            // Walidacja daty urodzenia
            while (true)
            {
                Console.Write("Podaj datę urodzenia pilota (RRRR-MM-DD): ");
                if (DateTime.TryParse(Console.ReadLine(), out birthDate))
                {
                    break;
                }
                Console.WriteLine("Błąd: Nieprawidłowy format daty. Spróbuj ponownie.");
            }

            // Walidacja dla kapitana (true/false)
            while (true)
            {
                Console.Write("Czy pilot jest kapitanem? (true/false): ");
                if (bool.TryParse(Console.ReadLine(), out isCaptain))
                {
                    break;
                }
                Console.WriteLine("Błąd: Wpisz 'true' lub 'false'.");
            }

            var pilot = new Pilot(name, birthDate, isCaptain);

            // Drukowanie danych pilota po jego utworzeniu
            Console.WriteLine("\nPilot został utworzony:");
            Console.WriteLine($"Imię: {pilot.Name}");
            Console.WriteLine($"Data urodzenia: {pilot.BirthDate.ToShortDateString()}");
            Console.WriteLine($"Kapitan: {pilot.IsCaptain}");
            Console.WriteLine($"Czy ma kompas: {pilot.HasCompass}");
            Console.WriteLine();

            return pilot;
        }

        private static FlightAttendant CreateFlightAttendant()
        {
            string name;
            DateTime birthDate;

            // Walidacja imienia
            while (true)
            {
                Console.Write("Podaj imię stewarda: ");
                name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    break;
                }
                Console.WriteLine("Błąd: Imię nie może być puste.");
            }

            // Walidacja daty urodzenia
            while (true)
            {
                Console.Write("Podaj datę urodzenia stewarda (RRRR-MM-DD): ");
                if (DateTime.TryParse(Console.ReadLine(), out birthDate))
                {
                    break;
                }
                Console.WriteLine("Błąd: Nieprawidłowy format daty. Spróbuj ponownie.");
            }

            // Walidacja języków
            List<string> languages = new List<string>();
            Console.WriteLine("Podaj języki, którymi mówi steward (wpisz 'stop', aby zakończyć): ");

            while (true)
            {
                string language = Console.ReadLine();
                if (language.ToLower() == "stop") break;

                if (!string.IsNullOrWhiteSpace(language))
                {
                    languages.Add(language);
                }
                else
                {
                    Console.WriteLine("Błąd: Język nie może być pusty.");
                }
            }

            var attendant = new FlightAttendant(name, birthDate, languages);

            // Drukowanie danych stewarda po jego utworzeniu
            Console.WriteLine("\nSteward został utworzony:");
            Console.WriteLine($"Imię: {attendant.Name}");
            Console.WriteLine($"Data urodzenia: {attendant.BirthDate.ToShortDateString()}");
            Console.WriteLine("Języki:");
            foreach (var lang in attendant.Languages)
            {
                Console.WriteLine($"- {lang}");
            }
            Console.WriteLine();

            return attendant;
        }

        private static Flight CreateFlight(List<Pilot> pilots, List<FlightAttendant> attendants)
        {

            // Sprawdzanie, czy są dostępni kapitanowie
            bool hasCaptain = pilots.Exists(p => p.IsCaptain);
            if (!hasCaptain)
            {
                Console.WriteLine("Błąd: Nie ma dostępnych kapitanów. Utwórz przynajmniej jednego kapitana przed utworzeniem lotu.");
                return null;  // Nie możemy kontynuować bez kapitanów
            }

            // Sprawdzanie, czy są dostępni stewardzi
            if (attendants.Count < 3)
            {
                Console.WriteLine("Błąd: Nie ma wystarczającej liczby stewardów. Utwórz przynajmniej trzech stewardów przed utworzeniem lotu.");
                return null;  // Nie możemy kontynuować bez stewardów
            }

            string flightId;
            string flightLanguage;

            // Walidacja identyfikatora lotu
            while (true)
            {
                Console.Write("Podaj identyfikator lotu: ");
                flightId = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(flightId))
                {
                    break;
                }
                Console.WriteLine("Błąd: Identyfikator lotu nie może być pusty.");
            }

            // Walidacja języka lotu
            while (true)
            {
                Console.Write("Podaj język lotu(poski, angielski, niemiecki, hiszpański: ");
                flightLanguage = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(flightLanguage))
                {
                    break;
                }
                Console.WriteLine("Błąd: Język lotu nie może być pusty.");
            }

            // Wybór kapitana
            int captainIndex;
            while (true)
            {
                Console.WriteLine("Wybierz kapitana (podaj numer): ");
                for (int i = 0; i < pilots.Count; i++)
                {
                    Console.WriteLine($"{i}: {pilots[i].Name} (Captain: {pilots[i].IsCaptain})");
                }

                if (int.TryParse(Console.ReadLine(), out captainIndex) && captainIndex >= 0 && captainIndex < pilots.Count)
                {
                    break;
                }
                Console.WriteLine("Błąd: Niepoprawny numer kapitana.");
            }
            Pilot captain = pilots[captainIndex];

            // Wybór drugiego pilota
            int coPilotIndex;
            while (true)
            {
                Console.WriteLine("Wybierz drugiego pilota (podaj numer): ");
                for (int i = 0; i < pilots.Count; i++)
                {
                    if (i != captainIndex)
                    {
                        Console.WriteLine($"{i}: {pilots[i].Name} (Captain: {pilots[i].IsCaptain})");
                    }
                }

                if (int.TryParse(Console.ReadLine(), out coPilotIndex) && coPilotIndex >= 0 && coPilotIndex < pilots.Count && coPilotIndex != captainIndex)
                {
                    break;
                }
                Console.WriteLine("Błąd: Niepoprawny numer drugiego pilota.");
            }
            Pilot coPilot = pilots[coPilotIndex];

            // Wybór stewardów
            List<FlightAttendant> selectedAttendants = new List<FlightAttendant>();
            while (true)
            {
                Console.WriteLine("Wybierz 3 stewardów (podaj numery oddzielone spacją): ");
                for (int i = 0; i < attendants.Count; i++)
                {
                    Console.WriteLine($"{i}: {attendants[i].Name}");
                }

                string[] attendantIndices = Console.ReadLine().Split(' ');
                if (attendantIndices.Length == 3)
                {
                    try
                    {
                        foreach (var index in attendantIndices)
                        {
                            selectedAttendants.Add(attendants[int.Parse(index)]);
                        }
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Błąd: Niepoprawny wybór stewardów.");
                        selectedAttendants.Clear();
                    }
                }
                else
                {
                    Console.WriteLine("Błąd: Musisz wybrać dokładnie 3 stewardów.");
                }
            }

            var flight = new Flight(flightId, flightLanguage, captain, coPilot, selectedAttendants);

            // Drukowanie danych lotu po jego utworzeniu
            Console.WriteLine("\nLot został utworzony:");
            Console.WriteLine($"Identyfikator lotu: {flight.FlightId}");
            Console.WriteLine($"Język lotu: {flight.FlightLanguage}");
            Console.WriteLine($"Kapitan: {flight.Captain.Name} (Czy ma kompas: {flight.Captain.HasCompass})");
            Console.WriteLine($"Drugi pilot: {flight.CoPilot.Name} (Czy ma kompas: {flight.CoPilot.HasCompass})");

            Console.WriteLine("Stewardzi:");
            foreach (var attendant in flight.Attendants)
            {
                Console.WriteLine($"- {attendant.Name}, Języki: {string.Join(", ", attendant.Languages)}");
            }
            Console.WriteLine();

            return flight;
        }

        private static void CheckFlightReadiness()
        {
            if (flights.Count == 0)
            {
                Console.WriteLine("Nie ma dostępnych lotów.");
                return;
            }

            Console.WriteLine("Wybierz lot do sprawdzenia (podaj numer): ");
            for (int i = 0; i < flights.Count; i++)
            {
                Console.WriteLine($"{i}: Lot {flights[i].FlightId} (język: {flights[i].FlightLanguage})");
            }

            int flightIndex = int.Parse(Console.ReadLine());
            var selectedFlight = flights[flightIndex];

            Console.WriteLine("\nSprawdzanie gotowości lotu do startu...");
            if (selectedFlight.IsReadyToTakeOff())
            {
                Console.WriteLine("Lot jest gotowy do startu.");
            }
            else
            {
                Console.WriteLine("Lot nie jest gotowy do startu.");
            }
        }
    }
}
