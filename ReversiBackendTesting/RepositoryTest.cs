using NUnit.Framework;
using ReversiRestApi.Models;
using ReversiRestApi.Enums;
using ReversiRestAPI.Interfaces;
using ReversiRestAPI.Models;

namespace ReversiBackendTesting
{
    [TestFixture]
    public class RepositoryTest
    {

        private IGameRepository repository;

        [SetUp]
        public void Setup()
        {
            repository = new GameRepository();
        }

        [Test]
        public void GetGames_InitialCount_IsThree()
        {
            Assert.AreEqual(3, repository.GetGames().Count);
        }

        [Test]
        public void List_DuplicateString_NotAdded()
        {
            var initialCount = repository.GetGames().Count;

            repository.AddGame(new Game
            {
                Token = "aaabbb"
            });

            Assert.AreEqual(initialCount, repository.GetGames().Count);
        }

        [Test]
        public void List_NewItem_ListCountIncreased()
        {
            var initialCount = repository.GetGames().Count;

            repository.AddGame(new Game
            {
                Token = "ggghhh"
            });

            Assert.AreNotEqual(initialCount, repository.GetGames().Count);
        }

        [Test]
        public void GetGame_ValidToken_ResultFound()
        {
            Assert.IsNotNull(repository.GetGame("aaabbb"));
        }

        [Test]
        public void GetGame_InvalidToken_ResultNull()
        {
            Assert.IsNull(repository.GetGame("abcdef"));
        }
    }
}
