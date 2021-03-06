﻿using NUnit.Framework;
using ReversiRestApi.Models;
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
            Assert.AreEqual(3, repository.GetGames().Result.Count);
        }

        [Test]
        public void List_NewItem_ListCountIncreased()
        {
            var initialCount = repository.GetGames().Result.Count;

            repository.AddGame(new Game());

            Assert.AreNotEqual(initialCount, repository.GetGames().Result.Count);
        }

        [Test]
        public void List_NewItem_GetsToken()
        {
            var token = repository.AddGame(new Game());
            Assert.IsNotNull(token);
        }

        [Test]
        public void List_NewItem_TwoDifferentTokens()
        {
            var token1 = repository.AddGame(new Game());
            var token2 = repository.AddGame(new Game());
            Assert.AreNotEqual(token1, token2);
        }

        [Test]
        public void GetGame_ValidToken_ResultFound()
        {
            Assert.IsNotNull(repository.GetGameAsync("aaabbb").Result);
        }

        [Test]
        public void GetGame_InvalidToken_ResultNull()
        {
            Assert.IsNull(repository.GetGameAsync("abcdef").Result);
        }

        [Test]
        public void GetGames_PlayerGames_GetOne()
        {
            Assert.AreEqual(1, repository.GetPlayerGames("abcdef").Result.Count);
        }

        [Test]
        public void GetGames_PlayerGames_IncreasedAfterAdd()
        {
            var playerToken = "abcdef";
            var initialCount = repository.GetPlayerGames(playerToken).Result.Count;

            repository.AddGame(new Game
            {
                Player1Token = playerToken
            });

            Assert.AreNotEqual(initialCount, repository.GetPlayerGames(playerToken));
        }
    }
}
