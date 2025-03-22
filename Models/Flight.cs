using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.Models
{
    public class Flight
    {
        public string FlightId { get; set; }
        public string FlightLanguage { get; set; }
        public Pilot Captain { get; set; }
        public Pilot CoPilot { get; set; }
        public List<FlightAttendant> Attendants { get; set; }

        public Flight(string flightId, string flightLanguage, Pilot captain, Pilot coPilot, List<FlightAttendant> attendants) 
        { 
            FlightId = flightId;
            FlightLanguage = flightLanguage;
            Captain = captain;
            CoPilot = coPilot;
            Attendants = attendants;
        }

        public bool IsReadyToTakeOff()
        {
            bool isReady = true;

            // Sprawdzenie, czy kapitan ma kompas
            Captain.CheckAndAssignCompass();
            if (!Captain.IsReady())
            {
                LogClearanceStep("Kapitan nie jest gotowy (brak kompasu).");
                isReady = false;
            }
            else
            {
                LogClearanceStep("Kapitan jest gotowy (posiada kompas).");
            }

            // Sprawdzenie, czy drugi pilot ma kompas
            CoPilot.CheckAndAssignCompass();
            if (!CoPilot.IsReady())
            {
                LogClearanceStep("Drugi pilot nie jest gotowy (brak kompasu).");
                isReady = false;
            }
            else
            {
                LogClearanceStep("Drugi pilot jest gotowy (posiada kompas).");
            }

            // Sprawdzenie, czy wszyscy stewardzi mówią językiem lotu
            foreach (var attendant in Attendants)
            {
                if (!attendant.SpeaksLanguage(FlightLanguage))
                {
                    LogClearanceStep($"Steward {attendant.Name} nie mówi językiem lotu: {FlightLanguage}.");
                    isReady = false;
                }
                else
                {
                    LogClearanceStep($"Steward {attendant.Name} mówi językiem lotu: {FlightLanguage}.");
                }
            }

            if (isReady)
            {
                LogClearanceStep("Lot jest gotowy do startu.");
            }
            else
            {
                LogClearanceStep("Lot nie jest gotowy do startu.");
            }

            return isReady;
        }

        private void LogClearanceStep(string message) 
        { 
            Console.WriteLine(message);
        }
    }
}
