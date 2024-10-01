using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatoriskOpgave1
{
    public interface ITrophiesRepository
    {
        IEnumerable<Trophy> Get(int? yearAfter = null, int? yearOnly = null, string? competitionIncludes = null, string? orderBy = null);
        Trophy GetById(int id);

        Trophy Add(Trophy trophy); //type reference

        Trophy Remove(int id);

        Trophy? Update(int id, Trophy trophy); // søger eksistende objekt ved hjælp af Id og tager Trophy trophy som er de nye oplysninger der er og opdaterer 

    }

}
