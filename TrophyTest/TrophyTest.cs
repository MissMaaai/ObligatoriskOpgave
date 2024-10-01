using ObligatoriskOpgave1;

namespace TrophyTest
{
    [TestClass]
    public class TrophyTest
    {
        private Trophy validTrophy = new Trophy { Id = 1, Competition = "Spring", Year = 2014 };
        private Trophy trophyCompetitionNull = new Trophy { Id = 2, Competition = null, Year = 2014 };
        private Trophy trophyCompetitionToShort = new Trophy { Id = 3, Competition = "Up", Year = 2014 };
        private Trophy trophyYearToOld = new Trophy { Id = 4, Competition = "Spring", Year = 1969 };
        private Trophy trophyYearInvalid = new Trophy { Id = 5, Competition = "Spring", Year = 2025 };

        [TestMethod]

        public void TestToString()
        {
            string str = validTrophy.ToString();
            Assert.AreEqual("1, Spring, 2014", str);
        }

        [TestMethod]
        public void ValidateYearTest() //tester altså år
        {
            validTrophy.ValidateYear();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => trophyYearToOld.ValidateYear());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => trophyYearInvalid.ValidateYear());
        }

        [TestMethod]
        public void ValidateCompetitionTest() // tester competition
        {
            validTrophy.ValidateCompetition();
            Assert.ThrowsException<ArgumentNullException>(() => trophyCompetitionNull.ValidateCompetition());
            Assert.ThrowsException<ArgumentException> (() => trophyCompetitionToShort.ValidateCompetition());
        }

        [TestMethod]
        public void ValidateTest() //tester min validate metoder
        {
            validTrophy.Validate();
            Assert.ThrowsException<ArgumentNullException>(() => trophyCompetitionNull.Validate());
            Assert.ThrowsException<ArgumentException>(() => trophyCompetitionToShort.Validate());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => trophyYearToOld.Validate());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => trophyYearInvalid.Validate());
        }
    }
}