using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.Models
{
    public class Pilot : Employee
    {
        public bool IsCaptain { get; set; }
        public bool HasCompass { get; set; }

        public Pilot(string name, DateTime birthDate, bool isCaptain) : base(name, birthDate)
        {
            IsCaptain = isCaptain;
            HasCompass = false;
        }
        
        public void ReceiveCompass()
        {
            HasCompass = true;
        }

        // Metoda, która sprawdza, czy pilot powinien już otrzymać kompas
        public void CheckAndAssignCompass()
        {
            DateTime hireDate = DateTime.Now.AddMonths(-1); // Zakładamy, że piloci są zatrudnieni od miesiąca
            TimeSpan employmentDuration = DateTime.Now - hireDate;

            if (!HasCompass && employmentDuration.Days >= 30)
            {
                ReceiveCompass();  // Pilot otrzymuje kompas po 30 dniach.
                Console.WriteLine($"{Name} otrzymał kompas, minęło 30 dni od zatrudnienia.");
            }
        }

        public override bool IsReady()
        {
            return HasCompass;
        }
    }
}
