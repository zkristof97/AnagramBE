using Logic.implementations;
using Logic.interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repository.interfaces;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class AnagramFinderLogicTests
    {
        private readonly Dictionary<string, List<string>> expectedResultsDictionary;

        private readonly AnagramFinderLogic _service;
        private readonly Mock<IWordRepository> _repoMock = new Mock<IWordRepository>();

        public AnagramFinderLogicTests()
        {
            _service = new AnagramFinderLogic(_repoMock.Object);

            expectedResultsDictionary = new Dictionary<string, List<string>>
            {
                {"t·rol" , new List<string>() { "rot·l", "olt·r", "t·rol", "r·olt", "r·tol" } },
                {"rrz··" , new List<string>() { "r·z·r" } }
            };
        }

        [TestMethod]
        public void GetAnagramsByWord_Should_Return_Strings()
        {
            foreach (var expectedResult in expectedResultsDictionary)
            {
                string searchTerm = expectedResult.Key;
                List<string> words = expectedResult.Value;

                _repoMock.Setup(x => x.GetAll()).Returns(words);

                var anagrams = _service.GetAnagramsByWord(searchTerm);

                CollectionAssert.AreEqual(words, anagrams);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(NoSearchTermException))]
        public void GetAnagramsByWord_Should_Throw_No_SearchTerm_Exception()
        {
            string searchTerm = null;

            _service.GetAnagramsByWord(searchTerm);
        }

        [TestMethod]
        [ExpectedException(typeof(NoMatchException))]
        public void GetAnagramsByWord_Should_Throw_No_Match_Exception()
        {
            string searchTerm = "xxxxx";

            _repoMock.Setup(x => x.GetAll()).Returns(new List<string>());

            _service.GetAnagramsByWord(searchTerm);
        }

        [TestMethod]
        [ExpectedException(typeof(SearchTermTooLongException))]
        public void GetAnagramsByWord_Should_Throw_SearchTerm_Too_Long_Exception()
        {
            string searchTerm = "veeeeeeeeeeeeeryLongWord";

            _repoMock.Setup(x => x.GetAll()).Returns(new List<string>());

            _service.GetAnagramsByWord(searchTerm);
        }
    }
}
