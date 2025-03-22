using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.Models
{
    public class FlightAttendant : Employee
    {
        public List<string> Languages { get; set; }

        public FlightAttendant(string name, DateTime birthDate, List<string> languages) : base(name, birthDate)
        {
            Languages = languages;
        }

        public override bool IsReady()
        {
            return true;
        }

        public bool SpeaksLanguage(string language)
        {
            return Languages.Contains(language);
        }
    }
}
