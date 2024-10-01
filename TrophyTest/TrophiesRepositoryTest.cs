using ObligatoriskOpgave1;

namespace TrophyTest;

[TestClass]
public class TrophiesRepositoryTest
{
    private ITrophiesRepository _repo;

    //Invalide objekter                                  readonly kan ikke ændres når den først er tildelt
    private readonly Trophy competitionNullTrophy = new() { Id = 2, Competition = "Hesteløb", Year = 1990 };
    private readonly Trophy competitionShortTrophy = new() { Id = 3, Competition = "Is", Year = 2000 };
    private readonly Trophy competitionYearInvalid = new() { Id = 4, Competition = "Væddemål", Year = 2025 };
    private readonly Trophy competitionYearToOld = new() { Id = 5, Competition = "Skiløb", Year = 1969 };

    [TestInitialize]
    public void Init()
    {
        _repo = new TrophiesRepository(); // her oprettets en ny instans og tildeles _repo - så listen her er tom

        _repo.Add(new Trophy() { Competition = "Gang", Year = 2000 }); // her tilføjes der en ny instant til listen
        _repo.Add(new Trophy() { Competition = "Spring", Year = 2001 });
        _repo.Add(new Trophy() { Competition = "Basketball", Year = 2023 });
        _repo.Add(new Trophy() { Competition = "Fifa", Year = 1999 });
    }

    [TestMethod]
    
    public void GetTest()
    {
        IEnumerable<Trophy> trophies = _repo.Get();
        Assert.AreEqual(4, trophies.Count()); //sammenligner de to og ser om der er 4 trofæer
        Assert.AreEqual(trophies.First().Competition,"Gang"); //sammeligner om det er korrekt værdi i ""

    }

    [TestMethod]
    public void GetTestSortedCompetition()
    {
        IEnumerable<Trophy> sortedTrophies = _repo.Get(orderBy: "competition");
        Assert.AreEqual(sortedTrophies.First().Competition, "Basketball"); //sorterer efter første competition
    }

    [TestMethod]
    public void GetSortedTrophiesYear()
    {

        IEnumerable<Trophy> sortedTrophiesYear = _repo.Get(orderBy: "year");
        Assert.AreEqual(sortedTrophiesYear.First().Year, 1999); // sorterer efter year og tjekker resultatet først    }

    }

    [TestMethod]
    public void GetByIdTest()
    {
        Assert.IsNotNull(_repo.GetById(3)); // henter et objekt fra _repo med unikke spefikation id
        Assert.IsNull(_repo.GetById(100)); // denne metode sikrer, at der ikke findes et objekt med id'et 100 og vil returnere null
    }


    [TestMethod]
    public void AddTest()
    {
        Trophy t = new() { Competition = "Test", Year = 2000 }; // t er reference til det nye objekt 
        Assert.AreEqual(5, _repo.Add(t).Id);
        Assert.AreEqual(5, _repo.Get().Count());

        Assert.ThrowsException<ArgumentOutOfRangeException>(() => _repo.Add(competitionYearInvalid)); 

    }

    [TestMethod]
    public void UpdateTest()
    {
        Assert.AreEqual(4, _repo.Get().Count()); //tjekker om der er 4 trofæer på listen _repo
        Trophy t = new() { Competition = "Test", Year = 2000 }; //her bliver der initaliseret ny instans af klassen Trophy med to egenskaber og dens værdier competition = test og year =2000. bruges til initalisere syntaksen og oprette nyt objekt og bruge dets egenskab
        Assert.IsNull(_repo.Update(100, t)); // tjekker om der er id'et med 100?
        Assert.AreEqual(1, _repo.Update(1, t)?.Id); // tjekker om update returerer en competition med id 3, hvis den eksisterer rturner den. ? - betyder hvis der ikke findes en med det specifikke id, returner den null uden exception
        Assert.AreEqual(_repo.Get().First().Competition, "Test");
        Assert.AreEqual(4, _repo.Get().Count());  //tester om der stadig er 4 trofæer og antaller ikke er ændret efter opdateringen

        Assert.ThrowsException<ArgumentOutOfRangeException>(() => _repo.Update(3, competitionYearInvalid));
    }

    [TestMethod]
    public void RemoveTest()
    {
        Assert.IsNull(_repo.Remove(100)); // slette det ønskede objekt fra liste, hvis den matcher id'et
        Assert.AreEqual(1, _repo.Remove(1)?.Id); //tjekker id'et matcher og sletter 
        Assert.AreEqual(3, _repo.Get().Count()); // tjekker i listen _repo, at antallet af trofæer er korrekt efter remove af en trofæ, så der er 3 tilbage efter en er slettet 
    }


}
