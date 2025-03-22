using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.Models
{
    public abstract class Employee
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }

        protected Employee(string name, DateTime birthDate) 
        {
            Name = name;
            BirthDate = birthDate;
        }

        public abstract bool IsReady();
    }
}
