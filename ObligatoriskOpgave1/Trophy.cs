using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ObligatoriskOpgave1
{
    public class Trophy
    {
        public int Id { get; set; }
        public string Competition { get; set; }
        public int Year { get; set; }

        public override string ToString()
        {
            return $"{Id}, {Competition}, {Year}";  
        }

        public Trophy(int id, string competition, int year)
        {
            Id = id;
            Competition = competition;
            Year = year;

        }

        public Trophy()
        {
        }

        public void ValidateCompetition()
        {
            if (Competition == null)
                throw new ArgumentNullException(" Enter competition");
            if (Competition.Length < 3)
                    throw new ArgumentException(" Name must be at more than 3 character");
        }


        public void ValidateYear()
        {
            if (Year < 1970 || Year > 2024)
                throw new ArgumentOutOfRangeException("Year must be more than 1970 and under 2024");
        }

        public void Validate()
        {
            ValidateCompetition();
            ValidateYear();  
        }
    }
}
