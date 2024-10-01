using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatoriskOpgave1
{
    public class TrophiesRepository : ITrophiesRepository
    {
        private int _nextId = 1;
        private readonly List<Trophy> _trophies = new(); // new(); - kompilatoren forstår, at der skal oprettes instans af List<Trophy>. Den er konsistens og sikrer, at typeinformationen er angivet én gang og reducerer fejl.

        public Trophy Add(Trophy trophy)
        {
            trophy.Validate(); // den tjekker objektet opfylder de gyldige krav
            trophy.Id = _nextId++; // her sætter jeg id'et til at lade repository til at håndtere id'et
            _trophies.Add(trophy); // her tilføjes trophy til respository listen
            return trophy;      
        }

        public IEnumerable<Trophy> Get(int? yearAfter = null, int? yearOnly = null, string? competitionIncludes = null, string? orderBy = null)
        {
            IEnumerable<Trophy> result = new List<Trophy>(_trophies);

            // her er det filtrering efter: året efter, bestemt årstak og hvilket konkurrence det er 

            if (yearAfter != null)
            {
                result = result.Where(y => y.Year > yearAfter);
            }

            if (yearOnly != null)
            {
                result = result.Where(o => o.Year < yearOnly);
            }

            if (competitionIncludes != null)
            {
                result = result.Where(c => c.Competition.Contains(competitionIncludes));
            }

            //Her er det hvordan man ønsker det er sorteret 
            if (orderBy != null)
            {
                orderBy = orderBy.ToLower(); //sorter alt med små bogstaver, for at sikre den ikke laver fejl i tilfælde der er skrevet med store bogstaver
                switch (orderBy) // tjekker hvad  orderby indeholder, altså det betyder der findes flere måder at sortere på
                {
                    case "competition":
                    case "competition_asc": // sorteret A-Z
                        result = result.OrderBy(c => c.Competition);
                        break; // betyder vi stopper her og går videre uden ændringer
                    case "competion_desc": // sorterer omvendt Z-A 
                        result = result.OrderByDescending(c => c.Competition);
                        break;
                    case "year":
                    case "year_asc":
                        result = result.OrderBy(y => y.Year);
                        break;
                    default: //hvis ingen muligheder passer,  gør den ikke noget
                        break; 
                }
            }
            return result;
        }

        public Trophy GetById(int id)
        {
            return _trophies.Find(trophy => trophy.Id == id); //Lammda 
        }

        public Trophy Remove(int id)
        {
            Trophy? trophy = GetById(id);
            if (trophy == null)
            {
                return null;
            }
            _trophies.Remove(trophy);
            return trophy;
        }

        public Trophy? Update(int id, Trophy trophy)
        {
            trophy.Validate(); //tjekke om objektet opfylder kravene
            Trophy existingTrophy = GetById(id);  //henter allerede eksisterende Trophy instans fra listen baseret på den specifikke id
            if (existingTrophy == null) 
            {
                return null;
            }
            existingTrophy.Competition = trophy.Competition; // bliver sat til den nye værdi
            existingTrophy.Year = trophy.Year; // bliver sat til den  nye værdi
            return existingTrophy;
        }
    }
}
