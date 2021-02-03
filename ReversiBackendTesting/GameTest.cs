using NUnit.Framework;
using ReversiRestApi.Models;
using ReversiRestApi.Enums;

namespace ReversiRestApi.Test
{
    [TestFixture]
    public class GameTest
    {
        // Colors
        // None = 0
        // White = 1
        // Black = 2
        [Test]
        public void IsPossible_OutsideBoard_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 0 0 0 0 0
            // 1 0 0 0 0 0 0 0 0
            // 2 0 0 0 0 0 0 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 0 0 0
            // 5 0 0 0 0 0 0 0 0
            // 6 0 0 0 0 0 0 0 0
            // 7 0 0 0 0 0 0 0 0
            // 1 <
            // Act
            game.Moving = Color.White;
            var actual = game.IsPossible(8, 8);
            // Assert
            Assert.IsFalse(actual);
            game.Print();
        }
        [Test]
        public void IsPossible_StartSituatieZet23Black_ReturnTrue()
        {
            // Arrange
            Game game = new Game();

            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 0 0 0 0 0
            // 1 0 0 0 0 0 0 0 0
            // 2 0 0 0 2 0 0 0 0 <
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 0 0 0
            // 5 0 0 0 0 0 0 0 0
            // 6 0 0 0 0 0 0 0 0
            // 7 0 0 0 0 0 0 0 0
            // Act
            game.Moving = Color.Black;
            var actual = game.IsPossible(2, 3);
            // Assert
            game.Print();
            Assert.IsTrue(actual);
        }

        [Test]
        public void IsPossible_StartSituatieZet23White_ReturnFalse()
        {
            // Arrange
            Game game = new Game();

            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 0 0 0 0 0
            // 1 0 0 0 0 0 0 0 0
            // 2 0 0 0 1 0 0 0 0 <
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 0 0 0
            // 5 0 0 0 0 0 0 0 0
            // 6 0 0 0 0 0 0 0 0
            // 7 0 0 0 0 0 0 0 0
            // Act
            game.Moving = Color.White;
            var actual = game.IsPossible(2, 3);
            // Assert
            Assert.IsFalse(actual);
        }
        [Test]
        public void IsPossible_ZetAanDeRandBoven_ReturnTrue()
        {
            // Arrange
            Game game = new Game();
            game.Board[1, 3] = Color.White;
            game.Board[2, 3] = Color.White;
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 2 0 0 0 0 <
            // 1 0 0 0 1 0 0 0 0
            // 2 0 0 0 1 0 0 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 0 0 0
            // 5 0 0 0 0 0 0 0 0
            // 6 0 0 0 0 0 0 0 0
            // 7 0 0 0 0 0 0 0 0
            // Act
            game.Moving = Color.Black;
            var actual = game.IsPossible(0, 3);
            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void IsPossible_ZetAanDeRandBoven_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            game.Board[1, 3] = Color.White;
            game.Board[2, 3] = Color.White;
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 1 0 0 0 0 <
            // 1 0 0 0 1 0 0 0 0
            // 2 0 0 0 1 0 0 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 0 0 0
            // 5 0 0 0 0 0 0 0 0
            // 6 0 0 0 0 0 0 0 0
            // 7 0 0 0 0 0 0 0 0
            // Act
            game.Moving = Color.White;
            var actual = game.IsPossible(0, 3);
            // Assert
            Assert.IsFalse(actual);
            game.Print();
        }
        [Test]
        public void IsPossible_ZetAanDeRandBovenEnTotBenedenReedsGevuld_ReturnTrue()
        {
            // Arrange
            Game game = new Game();
            game.Board[1, 3] = Color.White;
            game.Board[2, 3] = Color.White;
            game.Board[3, 3] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[5, 3] = Color.White;
            game.Board[6, 3] = Color.White;
            game.Board[7, 3] = Color.Black;
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 2 0 0 0 0 <
            // 1 0 0 0 1 0 0 0 0
            // 2 0 0 0 1 0 0 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 1 1 0 0 0
            // 5 0 0 0 1 0 0 0 0
            // 6 0 0 0 1 0 0 0 0
            // 7 0 0 0 2 0 0 0 0
            // Act
            game.Moving = Color.Black;
            var actual = game.IsPossible(0, 3);
            // Assert
            Assert.IsTrue(actual);
        }
        [Test]
        public void IsPossible_ZetAanDeRandBovenEnTotBenedenReedsGevuld_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            game.Board[1, 3] = Color.White;
            game.Board[2, 3] = Color.White;
            game.Board[3, 3] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[5, 3] = Color.White;
            game.Board[6, 3] = Color.White;
            game.Board[7, 3] = Color.White;
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 2 0 0 0 0 <
            // 1 0 0 0 1 0 0 0 0
            // 2 0 0 0 1 0 0 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 1 1 0 0 0
            // 5 0 0 0 1 0 0 0 0
            // 6 0 0 0 1 0 0 0 0
            // 7 0 0 0 1 0 0 0 0
            // Act
            game.Moving = Color.Black;
            var actual = game.IsPossible(0, 3);
            // Assert
            Assert.IsFalse(actual);
        }
        [Test]
        public void IsPossible_ZetAanDeRandRechts_ReturnTrue()
        {
            // Arrange
            Game game = new Game();
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.White;
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 2 0 0 0 0
            // 1 0 0 0 1 0 0 0 0
            // 2 0 0 0 1 0 0 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 1 1 2 <
            // 5 0 0 0 0 0 0 0 0
            // 6 0 0 0 0 0 0 0 0
            // 7 0 0 0 0 0 0 0 0
            // Act
            game.Moving = Color.Black;
            var actual = game.IsPossible(4, 7);
            // Assert
            Assert.IsTrue(actual);
        }
        [Test]
        public void IsPossible_ZetAanDeRandRechts_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.White;
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 1 0 0 0 0
            // 1 0 0 0 1 0 0 0 0
            // 2 0 0 0 1 0 0 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 1 1 1 <
            // 5 0 0 0 0 0 0 0 0
            // 6 0 0 0 0 0 0 0 0
            // 7 0 0 0 0 0 0 0 0
            // Act
            game.Moving = Color.White;
            var actual = game.IsPossible(4, 7);
            // Assert
            Assert.IsFalse(actual);
        }
        [Test]
        public void IsPossible_ZetAanDeRandRechtsEnTotLinksReedsGevuld_ReturnTrue()
        {
            // Arrange
            Game game = new Game();
            game.Board[4, 0] = Color.Black;
            game.Board[4, 1] = Color.White;
            game.Board[4, 2] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[4, 4] = Color.White;
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.White;
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 0 0 0 0 0
            // 1 0 0 0 0 0 0 0 0
            // 2 0 0 0 0 0 0 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 2 1 1 1 1 1 1 2 <
            // 5 0 0 0 0 0 0 0 0
            // 6 0 0 0 0 0 0 0 0
            // 7 0 0 0 0 0 0 0 0
            // Act
            game.Moving = Color.Black;
            var actual = game.IsPossible(4, 7);
            // Assert
            Assert.IsTrue(actual);
        }
        [Test]
        public void IsPossible_ZetAanDeRandRechtsEnTotLinksReedsGevuld_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            game.Board[4, 0] = Color.Black;
            game.Board[4, 1] = Color.White;
            game.Board[4, 2] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[4, 4] = Color.White;
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.White;
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 0 0 0 0 0
            // 1 0 0 0 0 0 0 0 0
            // 2 0 0 0 0 0 0 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 2 1 1 1 1 1 1 1 <
            // 5 0 0 0 0 0 0 0 0
            // 6 0 0 0 0 0 0 0 0
            // 7 0 0 0 0 0 0 0 0
            // Act
            game.Moving = Color.White;
            var actual = game.IsPossible(4, 7);
            // Assert
            Assert.IsFalse(actual);
        }
        // 0 1 2 3 4 5 6 7
        //
        // 0 0 0 0 0 0 0 0 0
        // 1 0 0 0 0 0 0 0 0
        // 2 0 0 0 0 0 0 0 0
        // 3 0 0 0 1 2 0 0 0
        // 4 0 0 0 2 1 0 0 0
        // 5 0 0 0 0 0 0 0 0
        // 6 0 0 0 0 0 0 0 0
        // 7 0 0 0 0 0 0 0 0
        [Test]
        public void IsPossible_StartSituatieZet22White_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 0 0 0 0 0
            // 1 0 0 0 0 0 0 0 0
            // 2 0 0 1 0 0 0 0 0 <
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 0 0 0
            // 5 0 0 0 0 0 0 0 0
            // 6 0 0 0 0 0 0 0 0
            // 7 0 0 0 0 0 0 0 0
            // Act
            game.Moving = Color.White;
            var actual = game.IsPossible(2, 2);
            // Assert
            Assert.IsFalse(actual);
        }
        [Test]
        public void IsPossible_StartSituatieZet22Black_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 0 0 0 0 0
            // 1 0 0 0 0 0 0 0 0
            // 2 0 0 2 0 0 0 0 0 <
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 0 0 0
            // 5 0 0 0 0 0 0 0 0
            // 6 0 0 0 0 0 0 0 0
            // 7 0 0 0 0 0 0 0 0
            // Act
            game.Moving = Color.Black;
            var actual = game.IsPossible(2, 2);
            // Assert
            Assert.IsFalse(actual);
        }
        [Test]
        public void IsPossible_ZetAanDeRandRechtsBoven_ReturnTrue()
        {
            // Arrange
            Game game = new Game();
            game.Board[2, 5] = Color.Black;
            game.Board[1, 6] = Color.Black;
            game.Board[5, 2] = Color.White;
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 0 0 0 0 1 <
            // 1 0 0 0 0 0 0 2 0
            // 2 0 0 0 0 0 2 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 0 0 0
            // 5 0 0 1 0 0 0 0 0
            // 6 0 0 0 0 0 0 0 0
            // 7 0 0 0 0 0 0 0 0
            // Act
            game.Moving = Color.White;
            var actual = game.IsPossible(0, 7);
            // Assert
            Assert.IsTrue(actual);
        }
        [Test]
        public void IsPossible_ZetAanDeRandRechtsBoven_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            game.Board[2, 5] = Color.Black;
            game.Board[1, 6] = Color.Black;
            game.Board[5, 2] = Color.White;
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 0 0 0 0 2 <
            // 1 0 0 0 0 0 0 2 0
            // 2 0 0 0 0 0 2 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 0 0 0
            // 5 0 0 1 0 0 0 0 0
            // 6 0 0 0 0 0 0 0 0
            // 7 0 0 0 0 0 0 0 0
            // Act
            game.Moving = Color.Black;
            var actual = game.IsPossible(0, 7);
            // Assert
            Assert.IsFalse(actual);
        }
        [Test]
        public void IsPossible_ZetAanDeRandRechtsOnder_ReturnTrue()
        {
            // Arrange
            Game game = new Game();
            game.Board[2, 2] = Color.Black;
            game.Board[5, 5] = Color.White;
            game.Board[6, 6] = Color.White;
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 0 0 0 0 0
            // 1 0 0 0 0 0 0 0 0
            // 2 0 0 2 0 0 0 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 0 0 0
            // 5 0 0 0 0 0 1 0 0
            // 6 0 0 0 0 0 0 1 0
            // 7 0 0 0 0 0 0 0 2 <
            // Act
            game.Moving = Color.Black;
            var actual = game.IsPossible(7, 7);
            // Assert
            Assert.IsTrue(actual);
        }
        [Test]
        public void IsPossible_ZetAanDeRandRechtsOnder_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            game.Board[2, 2] = Color.Black;
            game.Board[5, 5] = Color.White;
            game.Board[6, 6] = Color.White;
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 0 0 0 0 0 <
            // 1 0 0 0 0 0 0 0 0
            // 2 0 0 2 0 0 0 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 0 0 0
            // 5 0 0 0 0 0 1 0 0
            // 6 0 0 0 0 0 0 1 0
            // 7 0 0 0 0 0 0 0 1
            // Act
            game.Moving = Color.White;
            var actual = game.IsPossible(7, 7);
            // Assert
            Assert.IsFalse(actual);
        }
        [Test]
        public void IsPossible_ZetAanDeRandLinksBoven_ReturnTrue()
        {
            // Arrange
            Game game = new Game();
            game.Board[1, 1] = Color.White;
            game.Board[2, 2] = Color.White;
            game.Board[5, 5] = Color.Black;
            // 0 1 2 3 4 5 6 7
            // v
            // 0 2 0 0 0 0 0 0 0 <
            // 1 0 1 0 0 0 0 0 0
            // 2 0 0 1 0 0 0 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 0 0 0
            // 5 0 0 0 0 0 2 0 0
            // 6 0 0 0 0 0 0 0 0
            // 7 0 0 0 0 0 0 0 0
            // Act
            game.Moving = Color.Black;
            var actual = game.IsPossible(0, 0);
            // Assert
            Assert.IsTrue(actual);
        }
        [Test]
        public void IsPossible_ZetAanDeRandLinksBoven_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            game.Board[1, 1] = Color.White;
            game.Board[2, 2] = Color.White;
            game.Board[5, 5] = Color.Black;
            // 0 1 2 3 4 5 6 7
            // v
            // 0 1 0 0 0 0 0 0 0 <
            // 1 0 1 0 0 0 0 0 0
            // 2 0 0 1 0 0 0 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 0 0 0
            // 5 0 0 0 0 0 2 0 0
            // 6 0 0 0 0 0 0 0 0
            // 7 0 0 0 0 0 0 0 0
            // Act
            game.Moving = Color.White;
            var actual = game.IsPossible(0, 0);
            // Assert
            Assert.IsFalse(actual);
        }
        [Test]
        public void IsPossible_ZetAanDeRandLinksOnder_ReturnTrue()
        {
            // Arrange
            Game game = new Game();
            game.Board[2, 5] = Color.White;
            game.Board[5, 2] = Color.Black;
            game.Board[6, 1] = Color.Black;
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 0 0 0 0 0
            // 1 0 0 0 0 0 0 0 0
            // 2 0 0 0 0 0 1 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 0 0 0
            // 5 0 0 2 0 0 0 0 0
            // 6 0 2 0 0 0 0 0 0
            // 7 1 0 0 0 0 0 0 0 <
            // Act
            game.Moving = Color.White;
            var actual = game.IsPossible(7, 0);
            // Assert
            Assert.IsTrue(actual);
        }
        [Test]
        public void IsPossible_ZetAanDeRandLinksOnder_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            game.Board[2, 5] = Color.White;
            game.Board[5, 2] = Color.Black;
            game.Board[6, 1] = Color.Black;
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 0 0 0 0 0 <
            // 1 0 0 0 0 0 0 0 0
            // 2 0 0 0 0 0 1 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 0 0 0
            // 5 0 0 2 0 0 0 0 0
            // 6 0 2 0 0 0 0 0 0
            // 7 2 0 0 0 0 0 0 0
            // Act
            game.Moving = Color.Black;
            var actual = game.IsPossible(7, 0);
            // Assert
            Assert.IsFalse(actual);
        }
        //---------------------------------------------------------------------------
        [Test]
        public void Move_BuitenBoard_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 0 0 0 0 0
            // 1 0 0 0 0 0 0 0 0
            // 2 0 0 0 0 0 0 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 0 0 0
            // 5 0 0 0 0 0 0 0 0
            // 6 0 0 0 0 0 0 0 0
            // 7 0 0 0 0 0 0 0 0
            // 1 <
            // Act
            game.Moving = Color.White;
            var actual = game.Move(8, 8);
            // Assert
            Assert.IsFalse(actual);
            Assert.AreEqual(Color.White, game.Board[3, 3]);
            Assert.AreEqual(Color.White, game.Board[4, 4]);
            Assert.AreEqual(Color.Black, game.Board[3, 4]);
            Assert.AreEqual(Color.Black, game.Board[4, 3]);
            Assert.AreEqual(Color.White, game.Moving);
        }
        [Test]
        public void Move_StartSituatieZet23Black_ReturnTrue()
        {
            // Arrange
            Game game = new Game();
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 0 0 0 0 0
            // 1 0 0 0 0 0 0 0 0
            // 2 0 0 0 2 0 0 0 0 <
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 0 0 0
            // 5 0 0 0 0 0 0 0 0
            // 6 0 0 0 0 0 0 0 0
            // 7 0 0 0 0 0 0 0 0
            // Act
            game.Moving = Color.Black;
            var actual = game.Move(2, 3);
            // Assert
            Assert.IsTrue(actual);
            Assert.AreEqual(Color.Black, game.Board[2, 3]);
            Assert.AreEqual(Color.Black, game.Board[3, 3]);
            Assert.AreEqual(Color.Black, game.Board[4, 3]);
            Assert.AreEqual(Color.White, game.Moving);
        }

        [Test]
        public void Move_StartSituatieZet23White_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 0 0 0 0 0
            // 1 0 0 0 0 0 0 0 0
            // 2 0 0 0 1 0 0 0 0 <
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 0 0 0
            // 5 0 0 0 0 0 0 0 0
            // 6 0 0 0 0 0 0 0 0
            // 7 0 0 0 0 0 0 0 0
            // Act
            game.Moving = Color.White;
            var actual = game.Move(2, 3);
            // Assert
            Assert.IsFalse(actual);
            Assert.AreEqual(Color.White, game.Board[3, 3]);
            Assert.AreEqual(Color.White, game.Board[4, 4]);
            Assert.AreEqual(Color.Black, game.Board[3, 4]);
            Assert.AreEqual(Color.Black, game.Board[4, 3]);
            Assert.AreEqual(Color.None, game.Board[2, 3]);
            Assert.AreEqual(Color.White, game.Moving);
        }
        [Test]
        public void Move_ZetAanDeRandBoven_ReturnTrue()
        {
            // Arrange
            Game game = new Game();
            game.Board[1, 3] = Color.White;
            game.Board[2, 3] = Color.White;
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 2 0 0 0 0 <
            // 1 0 0 0 1 0 0 0 0
            // 2 0 0 0 1 0 0 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 0 0 0
            // 5 0 0 0 0 0 0 0 0
            // 6 0 0 0 0 0 0 0 0
            // 7 0 0 0 0 0 0 0 0
            // Act
            game.Moving = Color.Black;
            var actual = game.Move(0, 3);
            // Assert
            Assert.IsTrue(actual);
            Assert.AreEqual(Color.Black, game.Board[0, 3]);
            Assert.AreEqual(Color.Black, game.Board[1, 3]);
            Assert.AreEqual(Color.Black, game.Board[2, 3]);
            Assert.AreEqual(Color.Black, game.Board[3, 3]);
            Assert.AreEqual(Color.Black, game.Board[4, 3]);
            Assert.AreEqual(Color.White, game.Moving);
        }
        [Test]
        public void Move_ZetAanDeRandBoven_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            game.Board[1, 3] = Color.White;
            game.Board[2, 3] = Color.White;
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 1 0 0 0 0 <
            // 1 0 0 0 1 0 0 0 0
            // 2 0 0 0 1 0 0 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 0 0 0
            // 5 0 0 0 0 0 0 0 0
            // 6 0 0 0 0 0 0 0 0
            // 7 0 0 0 0 0 0 0 0
            // Act
            game.Moving = Color.White;
            var actual = game.Move(0, 3);
            // Assert
            Assert.IsFalse(actual);
            Assert.AreEqual(Color.White, game.Board[3, 3]);
            Assert.AreEqual(Color.White, game.Board[4, 4]);
            Assert.AreEqual(Color.Black, game.Board[3, 4]);
            Assert.AreEqual(Color.Black, game.Board[4, 3]);
            Assert.AreEqual(Color.White, game.Board[1, 3]);
            Assert.AreEqual(Color.White, game.Board[2, 3]);
            Assert.AreEqual(Color.None, game.Board[0, 3]);
        }
        [Test]
        public void Move_ZetAanDeRandBovenEnTotBenedenReedsGevuld_ReturnTrue()
        {
            // Arrange
            Game game = new Game();
            game.Board[1, 3] = Color.White;
            game.Board[2, 3] = Color.White;
            game.Board[3, 3] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[5, 3] = Color.White;
            game.Board[6, 3] = Color.White;
            game.Board[7, 3] = Color.Black;
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 2 0 0 0 0 <
            // 1 0 0 0 1 0 0 0 0
            // 2 0 0 0 1 0 0 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 1 1 0 0 0
            // 5 0 0 0 1 0 0 0 0
            // 6 0 0 0 1 0 0 0 0
            // 7 0 0 0 2 0 0 0 0
            // Act
            game.Moving = Color.Black;
            var actual = game.Move(0, 3);
            // Assert
            Assert.IsTrue(actual);
            Assert.AreEqual(Color.Black, game.Board[0, 3]);
            Assert.AreEqual(Color.Black, game.Board[1, 3]);
            Assert.AreEqual(Color.Black, game.Board[2, 3]);
            Assert.AreEqual(Color.Black, game.Board[3, 3]);
            Assert.AreEqual(Color.Black, game.Board[4, 3]);
            Assert.AreEqual(Color.Black, game.Board[5, 3]);
            Assert.AreEqual(Color.Black, game.Board[6, 3]);
            Assert.AreEqual(Color.Black, game.Board[7, 3]);
        }
        [Test]
        public void Move_ZetAanDeRandBovenEnTotBenedenReedsGevuld_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            game.Board[1, 3] = Color.White;
            game.Board[2, 3] = Color.White;
            game.Board[3, 3] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[5, 3] = Color.White;
            game.Board[6, 3] = Color.White;
            game.Board[7, 3] = Color.White;
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 2 0 0 0 0 <
            // 1 0 0 0 1 0 0 0 0
            // 2 0 0 0 1 0 0 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 1 1 0 0 0
            // 5 0 0 0 1 0 0 0 0
            // 6 0 0 0 1 0 0 0 0
            // 7 0 0 0 1 0 0 0 0
            // Act
            game.Moving = Color.Black;
            var actual = game.Move(0, 3);
            // Assert
            Assert.IsFalse(actual);
            Assert.AreEqual(Color.White, game.Board[3, 3]);
            Assert.AreEqual(Color.White, game.Board[4, 4]);
            Assert.AreEqual(Color.Black, game.Board[3, 4]);
            Assert.AreEqual(Color.White, game.Board[4, 3]);
            Assert.AreEqual(Color.White, game.Board[1, 3]);
            Assert.AreEqual(Color.White, game.Board[2, 3]);
            Assert.AreEqual(Color.None, game.Board[0, 3]);
        }
        [Test]
        public void Move_ZetAanDeRandRechts_ReturnTrue()
        {
            // Arrange
            Game game = new Game();
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.White;
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 0 0 0 0 0
            // 1 0 0 0 0 0 0 0 0
            // 2 0 0 0 0 0 0 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 1 1 2 <
            // 5 0 0 0 0 0 0 0 0
            // 6 0 0 0 0 0 0 0 0
            // 7 0 0 0 0 0 0 0 0
            // Act
            game.Moving = Color.Black;
            var actual = game.Move(4, 7);
            // Assert
            Assert.IsTrue(actual);
            Assert.AreEqual(Color.Black, game.Board[4, 3]);
            Assert.AreEqual(Color.Black, game.Board[4, 4]);
            Assert.AreEqual(Color.Black, game.Board[4, 5]);
            Assert.AreEqual(Color.Black, game.Board[4, 6]);
            Assert.AreEqual(Color.Black, game.Board[4, 7]);
        }
        [Test]
        public void Move_ZetAanDeRandRechts_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.White;
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 1 0 0 0 0
            // 1 0 0 0 1 0 0 0 0
            // 2 0 0 0 1 0 0 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 1 1 1 <
            // 5 0 0 0 0 0 0 0 0
            // 6 0 0 0 0 0 0 0 0
            // 7 0 0 0 0 0 0 0 0
            // Act
            game.Moving = Color.White;
            var actual = game.Move(4, 7);
            // Assert
            Assert.IsFalse(actual);
            Assert.AreEqual(Color.White, game.Board[3, 3]);
            Assert.AreEqual(Color.White, game.Board[4, 4]);
            Assert.AreEqual(Color.Black, game.Board[3, 4]);
            Assert.AreEqual(Color.Black, game.Board[4, 3]);
            Assert.AreEqual(Color.White, game.Board[4, 5]);
            Assert.AreEqual(Color.White, game.Board[4, 6]);
            Assert.AreEqual(Color.None, game.Board[4, 7]);
        }
        [Test]
        public void Move_ZetAanDeRandRechtsEnTotLinksReedsGevuld_ReturnTrue()
        {
            // Arrange
            Game game = new Game();
            game.Board[4, 0] = Color.Black;
            game.Board[4, 1] = Color.White;
            game.Board[4, 2] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[4, 4] = Color.White;
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.White;
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 0 0 0 0 0
            // 1 0 0 0 0 0 0 0 0
            // 2 0 0 0 0 0 0 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 2 1 1 1 1 1 1 2 <
            // 5 0 0 0 0 0 0 0 0
            // 6 0 0 0 0 0 0 0 0
            // 7 0 0 0 0 0 0 0 0
            // Act
            game.Moving = Color.Black;
            var actual = game.Move(4, 7);
            // Assert
            Assert.IsTrue(actual);
            Assert.AreEqual(Color.Black, game.Board[4, 0]);
            Assert.AreEqual(Color.Black, game.Board[4, 1]);
            Assert.AreEqual(Color.Black, game.Board[4, 2]);
            Assert.AreEqual(Color.Black, game.Board[4, 3]);
            Assert.AreEqual(Color.Black, game.Board[4, 4]);
            Assert.AreEqual(Color.Black, game.Board[4, 5]);
            Assert.AreEqual(Color.Black, game.Board[4, 6]);
            Assert.AreEqual(Color.Black, game.Board[4, 7]);
        }
        [Test]
        public void Move_ZetAanDeRandRechtsEnTotLinksReedsGevuld_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            game.Board[4, 0] = Color.Black;
            game.Board[4, 1] = Color.White;
            game.Board[4, 2] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[4, 4] = Color.White;
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.White;
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 0 0 0 0 0
            // 1 0 0 0 0 0 0 0 0
            // 2 0 0 0 0 0 0 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 2 1 1 1 1 1 1 1 <
            // 5 0 0 0 0 0 0 0 0
            // 6 0 0 0 0 0 0 0 0
            // 7 0 0 0 0 0 0 0 0
            // Act
            game.Moving = Color.White;
            var actual = game.Move(4, 7);
            // Assert
            Assert.IsFalse(actual);
            Assert.AreEqual(Color.White, game.Board[3, 3]);
            Assert.AreEqual(Color.White, game.Board[4, 4]);
            Assert.AreEqual(Color.Black, game.Board[3, 4]);
            Assert.AreEqual(Color.White, game.Board[4, 3]);
            Assert.AreEqual(Color.Black, game.Board[4, 0]);
            Assert.AreEqual(Color.White, game.Board[4, 1]);
            Assert.AreEqual(Color.White, game.Board[4, 2]);
            Assert.AreEqual(Color.White, game.Board[4, 5]);
            Assert.AreEqual(Color.White, game.Board[4, 6]);
            Assert.AreEqual(Color.None, game.Board[4, 7]);
        }
        // 0 1 2 3 4 5 6 7
        //
        // 0 0 0 0 0 0 0 0 0
        // 1 0 0 0 0 0 0 0 0
        // 2 0 0 0 0 0 0 0 0
        // 3 0 0 0 1 2 0 0 0
        // 4 0 0 0 2 1 0 0 0
        // 5 0 0 0 0 0 0 0 0
        // 6 0 0 0 0 0 0 0 0
        // 7 0 0 0 0 0 0 0 0
        [Test]
        public void Move_StartSituatieZet22White_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 0 0 0 0 0
            // 1 0 0 0 0 0 0 0 0
            // 2 0 0 1 0 0 0 0 0 <
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 0 0 0
            // 5 0 0 0 0 0 0 0 0
            // 6 0 0 0 0 0 0 0 0
            // 7 0 0 0 0 0 0 0 0
            // Act
            game.Moving = Color.White;
            var actual = game.Move(2, 2);
            // Assert
            Assert.IsFalse(actual);
            Assert.AreEqual(Color.White, game.Board[3, 3]);
            Assert.AreEqual(Color.White, game.Board[4, 4]);
            Assert.AreEqual(Color.Black, game.Board[3, 4]);
            Assert.AreEqual(Color.Black, game.Board[4, 3]);
            Assert.AreEqual(Color.None, game.Board[2, 2]);
        }
        [Test]
        public void Move_StartSituatieZet22Black_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 0 0 0 0 0
            // 1 0 0 0 0 0 0 0 0
            // 2 0 0 2 0 0 0 0 0 <
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 0 0 0
            // 5 0 0 0 0 0 0 0 0
            // 6 0 0 0 0 0 0 0 0
            // 7 0 0 0 0 0 0 0 0
            // Act
            game.Moving = Color.Black;
            var actual = game.Move(2, 2);
            // Assert
            Assert.IsFalse(actual);
            Assert.AreEqual(Color.White, game.Board[3, 3]);
            Assert.AreEqual(Color.White, game.Board[4, 4]);
            Assert.AreEqual(Color.Black, game.Board[3, 4]);
            Assert.AreEqual(Color.Black, game.Board[4, 3]);
            Assert.AreEqual(Color.None, game.Board[2, 2]);
        }
        [Test]
        public void Move_ZetAanDeRandRechtsBoven_ReturnTrue()
        {
            // Arrange
            Game game = new Game();
            game.Board[2, 5] = Color.Black;
            game.Board[1, 6] = Color.Black;
            game.Board[5, 2] = Color.White;
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 0 0 0 0 1 <
            // 1 0 0 0 0 0 0 2 0
            // 2 0 0 0 0 0 2 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 0 0 0
            // 5 0 0 1 0 0 0 0 0
            // 6 0 0 0 0 0 0 0 0
            // 7 0 0 0 0 0 0 0 0
            // Act
            game.Moving = Color.White;
            var actual = game.Move(0, 7);
            // Assert
            Assert.IsTrue(actual);
            Assert.AreEqual(Color.White, game.Board[5, 2]);
            Assert.AreEqual(Color.White, game.Board[4, 3]);
            Assert.AreEqual(Color.White, game.Board[3, 4]);
            Assert.AreEqual(Color.White, game.Board[2, 5]);
            Assert.AreEqual(Color.White, game.Board[1, 6]);
            Assert.AreEqual(Color.White, game.Board[0, 7]);
        }
        [Test]
        public void Move_ZetAanDeRandRechtsBoven_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            game.Board[2, 5] = Color.Black;
            game.Board[1, 6] = Color.Black;
            game.Board[5, 2] = Color.White;
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 0 0 0 0 2 <
            // 1 0 0 0 0 0 0 2 0
            // 2 0 0 0 0 0 2 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 0 0 0
            // 5 0 0 1 0 0 0 0 0
            // 6 0 0 0 0 0 0 0 0
            // 7 0 0 0 0 0 0 0 0
            // Act
            game.Moving = Color.Black;
            var actual = game.Move(0, 7);
            // Assert
            Assert.IsFalse(actual);
            Assert.AreEqual(Color.White, game.Board[3, 3]);
            Assert.AreEqual(Color.White, game.Board[4, 4]);
            Assert.AreEqual(Color.Black, game.Board[3, 4]);
            Assert.AreEqual(Color.Black, game.Board[4, 3]);
            Assert.AreEqual(Color.Black, game.Board[1, 6]);
            Assert.AreEqual(Color.Black, game.Board[2, 5]);
            Assert.AreEqual(Color.White, game.Board[5, 2]);
            Assert.AreEqual(Color.None, game.Board[0, 7]);
        }
        [Test]
        public void Move_ZetAanDeRandRechtsOnder_ReturnTrue()
        {
            // Arrange
            Game game = new Game();
            game.Board[2, 2] = Color.Black;
            game.Board[5, 5] = Color.White;
            game.Board[6, 6] = Color.White;
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 0 0 0 0 0
            // 1 0 0 0 0 0 0 0 0
            // 2 0 0 2 0 0 0 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 0 0 0
            // 5 0 0 0 0 0 1 0 0
            // 6 0 0 0 0 0 0 1 0
            // 7 0 0 0 0 0 0 0 2 <
            // Act
            game.Moving = Color.Black;
            var actual = game.Move(7, 7);
            // Assert
            Assert.IsTrue(actual);
            Assert.AreEqual(Color.Black, game.Board[2, 2]);
            Assert.AreEqual(Color.Black, game.Board[3, 3]);
            Assert.AreEqual(Color.Black, game.Board[4, 4]);
            Assert.AreEqual(Color.Black, game.Board[5, 5]);
            Assert.AreEqual(Color.Black, game.Board[6, 6]);
            Assert.AreEqual(Color.Black, game.Board[7, 7]);
        }

        [Test]
        public void Move_ZetAanDeRandRechtsOnder_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            game.Board[2, 2] = Color.Black;
            game.Board[5, 5] = Color.White;
            game.Board[6, 6] = Color.White;
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 0 0 0 0 0
            // 1 0 0 0 0 0 0 0 0
            // 2 0 0 2 0 0 0 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 0 0 0
            // 5 0 0 0 0 0 1 0 0
            // 6 0 0 0 0 0 0 1 0
            // 7 0 0 0 0 0 0 0 1 <
            // Act
            game.Moving = Color.White;
            var actual = game.Move(7, 7);
            // Assert
            Assert.IsFalse(actual);
            Assert.AreEqual(Color.White, game.Board[3, 3]);
            Assert.AreEqual(Color.White, game.Board[4, 4]);
            Assert.AreEqual(Color.Black, game.Board[3, 4]);
            Assert.AreEqual(Color.Black, game.Board[4, 3]);
            Assert.AreEqual(Color.Black, game.Board[2, 2]);
            Assert.AreEqual(Color.White, game.Board[5, 5]);
            Assert.AreEqual(Color.White, game.Board[6, 6]);
            Assert.AreEqual(Color.None, game.Board[7, 7]);
        }
        [Test]
        public void Move_ZetAanDeRandLinksBoven_ReturnTrue()
        {
            // Arrange
            Game game = new Game();
            game.Board[1, 1] = Color.White;
            game.Board[2, 2] = Color.White;
            game.Board[5, 5] = Color.Black;
            // 0 1 2 3 4 5 6 7
            // v
            // 0 2 0 0 0 0 0 0 0 <
            // 1 0 1 0 0 0 0 0 0
            // 2 0 0 1 0 0 0 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 0 0 0
            // 5 0 0 0 0 0 2 0 0
            // 6 0 0 0 0 0 0 0 0
            // 7 0 0 0 0 0 0 0 0
            // Act
            game.Moving = Color.Black;
            var actual = game.Move(0, 0);
            // Assert
            Assert.IsTrue(actual);
            Assert.AreEqual(Color.Black, game.Board[0, 0]);
            Assert.AreEqual(Color.Black, game.Board[1, 1]);
            Assert.AreEqual(Color.Black, game.Board[2, 2]);
            Assert.AreEqual(Color.Black, game.Board[3, 3]);
            Assert.AreEqual(Color.Black, game.Board[4, 4]);
            Assert.AreEqual(Color.Black, game.Board[5, 5]);
        }
        [Test]
        public void Move_ZetAanDeRandLinksBoven_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            game.Board[1, 1] = Color.White;
            game.Board[2, 2] = Color.White;
            game.Board[5, 5] = Color.Black;
            // 0 1 2 3 4 5 6 7
            // v
            // 0 1 0 0 0 0 0 0 0 <
            // 1 0 1 0 0 0 0 0 0
            // 2 0 0 1 0 0 0 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 0 0 0
            // 5 0 0 0 0 0 2 0 0
            // 6 0 0 0 0 0 0 0 0
            // 7 0 0 0 0 0 0 0 0
            // Act
            game.Moving = Color.White;
            var actual = game.Move(0, 0);
            // Assert
            Assert.IsFalse(actual);
            Assert.AreEqual(Color.White, game.Board[3, 3]);
            Assert.AreEqual(Color.White, game.Board[4, 4]);
            Assert.AreEqual(Color.Black, game.Board[3, 4]);
            Assert.AreEqual(Color.Black, game.Board[4, 3]);
            Assert.AreEqual(Color.White, game.Board[1, 1]);
            Assert.AreEqual(Color.White, game.Board[2, 2]);
            Assert.AreEqual(Color.Black, game.Board[5, 5]);
            Assert.AreEqual(Color.None, game.Board[0, 0]);
        }
        [Test]
        public void Move_ZetAanDeRandLinksOnder_ReturnTrue()
        {
            // Arrange
            Game game = new Game();
            game.Board[2, 5] = Color.White;
            game.Board[5, 2] = Color.Black;
            game.Board[6, 1] = Color.Black;
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 0 0 0 0 0
            // 1 0 0 0 0 0 0 0 0
            // 2 0 0 0 0 0 1 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 0 0 0
            // 5 0 0 2 0 0 0 0 0
            // 6 0 2 0 0 0 0 0 0
            // 7 1 0 0 0 0 0 0 0 <
            // Act
            game.Moving = Color.White;
            var actual = game.Move(7, 0);
            // Assert
            Assert.IsTrue(actual);
            Assert.AreEqual(Color.White, game.Board[7, 0]);
            Assert.AreEqual(Color.White, game.Board[6, 1]);
            Assert.AreEqual(Color.White, game.Board[5, 2]);
            Assert.AreEqual(Color.White, game.Board[4, 3]);
            Assert.AreEqual(Color.White, game.Board[3, 4]);
            Assert.AreEqual(Color.White, game.Board[2, 5]);
        }
        [Test]
        public void Move_ZetAanDeRandLinksOnder_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            game.Board[2, 5] = Color.White;
            game.Board[5, 2] = Color.Black;
            game.Board[6, 1] = Color.Black;
            // 0 1 2 3 4 5 6 7
            // v
            // 0 0 0 0 0 0 0 0 0
            // 1 0 0 0 0 0 0 0 0
            // 2 0 0 0 0 0 1 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 0 0 0
            // 5 0 0 2 0 0 0 0 0
            // 6 0 2 0 0 0 0 0 0
            // 7 2 0 0 0 0 0 0 0 <
            // Act
            game.Moving = Color.Black;
            var actual = game.Move(7, 0);
            // Assert
            Assert.IsFalse(actual);
            Assert.AreEqual(Color.White, game.Board[3, 3]);
            Assert.AreEqual(Color.White, game.Board[4, 4]);
            Assert.AreEqual(Color.Black, game.Board[3, 4]);
            Assert.AreEqual(Color.Black, game.Board[4, 3]);
            Assert.AreEqual(Color.White, game.Board[2, 5]);
            Assert.AreEqual(Color.Black, game.Board[5, 2]);
            Assert.AreEqual(Color.Black, game.Board[6, 1]);
            Assert.AreEqual(Color.None, game.Board[7, 7]);
            Assert.AreEqual(Color.None, game.Board[7, 0]);
        }
        [Test]
        public void Pass_BlackAanZetNoneIsPossible_ReturnTrueEnWisselBeurt()
        {
            // Arrange (zowel White als Black kunnen niet meer)
            Game game = new Game();
            game.Board[0, 0] = Color.White;
            game.Board[0, 1] = Color.White;
            game.Board[0, 2] = Color.White;
            game.Board[0, 3] = Color.White;
            game.Board[0, 4] = Color.White;
            game.Board[0, 5] = Color.White;
            game.Board[0, 6] = Color.White;
            game.Board[0, 7] = Color.White;
            game.Board[1, 0] = Color.White;
            game.Board[1, 1] = Color.White;
            game.Board[1, 2] = Color.White;
            game.Board[1, 3] = Color.White;
            game.Board[1, 4] = Color.White;
            game.Board[1, 5] = Color.White;
            game.Board[1, 6] = Color.White;
            game.Board[1, 7] = Color.White;
            game.Board[2, 0] = Color.White;
            game.Board[2, 1] = Color.White;
            game.Board[2, 2] = Color.White;
            game.Board[2, 3] = Color.White;
            game.Board[2, 4] = Color.White;
            game.Board[2, 5] = Color.White;
            game.Board[2, 6] = Color.White;
            game.Board[2, 7] = Color.White;
            game.Board[3, 0] = Color.White;
            game.Board[3, 1] = Color.White;
            game.Board[3, 2] = Color.White;
            game.Board[3, 3] = Color.White;
            game.Board[3, 4] = Color.White;
            game.Board[3, 5] = Color.White;
            game.Board[3, 6] = Color.White;
            game.Board[3, 7] = Color.None;
            game.Board[4, 0] = Color.White;
            game.Board[4, 1] = Color.White;
            game.Board[4, 2] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[4, 4] = Color.White;
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.None;
            game.Board[4, 7] = Color.None;
            game.Board[5, 0] = Color.White;
            game.Board[5, 1] = Color.White;
            game.Board[5, 2] = Color.White;
            game.Board[5, 3] = Color.White;
            game.Board[5, 4] = Color.White;
            game.Board[5, 5] = Color.White;
            game.Board[5, 6] = Color.None;
            game.Board[5, 7] = Color.Black;
            game.Board[6, 0] = Color.White;
            game.Board[6, 1] = Color.White;
            game.Board[6, 2] = Color.White;
            game.Board[6, 3] = Color.White;
            game.Board[6, 4] = Color.White;
            game.Board[6, 5] = Color.White;
            game.Board[6, 6] = Color.White;
            game.Board[6, 7] = Color.None;
            game.Board[7, 0] = Color.White;
            game.Board[7, 1] = Color.White;
            game.Board[7, 2] = Color.White;
            game.Board[7, 3] = Color.White;
            game.Board[7, 4] = Color.White;
            game.Board[7, 5] = Color.White;
            game.Board[7, 6] = Color.White;
            game.Board[7, 7] = Color.White;
            // 0 1 2 3 4 5 6 7
            // v
            // 0 1 1 1 1 1 1 1 1
            // 1 1 1 1 1 1 1 1 1
            // 2 1 1 1 1 1 1 1 1
            // 3 1 1 1 1 1 1 1 0
            // 4 1 1 1 1 1 1 0 0
            // 5 1 1 1 1 1 1 0 2
            // 6 1 1 1 1 1 1 1 0
            // 7 1 1 1 1 1 1 1 1
            // Act
            game.Moving = Color.Black;
            var actual = game.Pass();
            // Assert
            Assert.IsTrue(actual);
            Assert.AreEqual(Color.White, game.Moving);
        }
        [Test]
        public void Pass_WhiteAanZetNoneIsPossible_ReturnTrueEnWisselBeurt()
        {
            // Arrange (zowel White als Black kunnen niet meer)
            Game game = new Game();
            game.Board[0, 0] = Color.White;
            game.Board[0, 1] = Color.White;
            game.Board[0, 2] = Color.White;
            game.Board[0, 3] = Color.White;
            game.Board[0, 4] = Color.White;
            game.Board[0, 5] = Color.White;
            game.Board[0, 6] = Color.White;
            game.Board[0, 7] = Color.White;
            game.Board[1, 0] = Color.White;
            game.Board[1, 1] = Color.White;
            game.Board[1, 2] = Color.White;
            game.Board[1, 3] = Color.White;
            game.Board[1, 4] = Color.White;
            game.Board[1, 5] = Color.White;
            game.Board[1, 6] = Color.White;
            game.Board[1, 7] = Color.White;
            game.Board[2, 0] = Color.White;
            game.Board[2, 1] = Color.White;
            game.Board[2, 2] = Color.White;
            game.Board[2, 3] = Color.White;
            game.Board[2, 4] = Color.White;
            game.Board[2, 5] = Color.White;
            game.Board[2, 6] = Color.White;
            game.Board[2, 7] = Color.White;
            game.Board[3, 0] = Color.White;
            game.Board[3, 1] = Color.White;
            game.Board[3, 2] = Color.White;
            game.Board[3, 3] = Color.White;
            game.Board[3, 4] = Color.White;
            game.Board[3, 5] = Color.White;
            game.Board[3, 6] = Color.White;
            game.Board[3, 7] = Color.None;
            game.Board[4, 0] = Color.White;
            game.Board[4, 1] = Color.White;
            game.Board[4, 2] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[4, 4] = Color.White;
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.None;
            game.Board[4, 7] = Color.None;
            game.Board[5, 0] = Color.White;
            game.Board[5, 1] = Color.White;
            game.Board[5, 2] = Color.White;
            game.Board[5, 3] = Color.White;
            game.Board[5, 4] = Color.White;
            game.Board[5, 5] = Color.White;
            game.Board[5, 6] = Color.None;
            game.Board[5, 7] = Color.Black;
            game.Board[6, 0] = Color.White;
            game.Board[6, 1] = Color.White;
            game.Board[6, 2] = Color.White;
            game.Board[6, 3] = Color.White;
            game.Board[6, 4] = Color.White;
            game.Board[6, 5] = Color.White;
            game.Board[6, 6] = Color.White;
            game.Board[6, 7] = Color.None;
            game.Board[7, 0] = Color.White;
            game.Board[7, 1] = Color.White;
            game.Board[7, 2] = Color.White;
            game.Board[7, 3] = Color.White;
            game.Board[7, 4] = Color.White;
            game.Board[7, 5] = Color.White;
            game.Board[7, 6] = Color.White;
            game.Board[7, 7] = Color.White;
            // 0 1 2 3 4 5 6 7
            // v
            // 0 1 1 1 1 1 1 1 1
            // 1 1 1 1 1 1 1 1 1
            // 2 1 1 1 1 1 1 1 1
            // 3 1 1 1 1 1 1 1 0
            // 4 1 1 1 1 1 1 0 0
            // 5 1 1 1 1 1 1 0 2
            // 6 1 1 1 1 1 1 1 0
            // 7 1 1 1 1 1 1 1 1
            // Act
            game.Moving = Color.White;
            var actual = game.Pass();
            // Assert
            Assert.IsTrue(actual);
            Assert.AreEqual(Color.Black, game.Moving);
        }
        [Test]
        public void Finished_NoneIsPossible_ReturnTrue()
        {
            // Arrange (zowel White als Black kunnen niet meer)
            Game game = new Game();
            game.Board[0, 0] = Color.White;
            game.Board[0, 1] = Color.White;
            game.Board[0, 2] = Color.White;
            game.Board[0, 3] = Color.White;
            game.Board[0, 4] = Color.White;
            game.Board[0, 5] = Color.White;
            game.Board[0, 6] = Color.White;
            game.Board[0, 7] = Color.White;
            game.Board[1, 0] = Color.White;
            game.Board[1, 1] = Color.White;
            game.Board[1, 2] = Color.White;
            game.Board[1, 3] = Color.White;
            game.Board[1, 4] = Color.White;
            game.Board[1, 5] = Color.White;
            game.Board[1, 6] = Color.White;
            game.Board[1, 7] = Color.White;
            game.Board[2, 0] = Color.White;
            game.Board[2, 1] = Color.White;
            game.Board[2, 2] = Color.White;
            game.Board[2, 3] = Color.White;
            game.Board[2, 4] = Color.White;
            game.Board[2, 5] = Color.White;
            game.Board[2, 6] = Color.White;
            game.Board[2, 7] = Color.White;
            game.Board[3, 0] = Color.White;
            game.Board[3, 1] = Color.White;
            game.Board[3, 2] = Color.White;
            game.Board[3, 3] = Color.White;
            game.Board[3, 4] = Color.White;
            game.Board[3, 5] = Color.White;
            game.Board[3, 6] = Color.White;
            game.Board[3, 7] = Color.None;
            game.Board[4, 0] = Color.White;
            game.Board[4, 1] = Color.White;
            game.Board[4, 2] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[4, 4] = Color.White;
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.None;
            game.Board[4, 7] = Color.None;
            game.Board[5, 0] = Color.White;
            game.Board[5, 1] = Color.White;
            game.Board[5, 2] = Color.White;
            game.Board[5, 3] = Color.White;
            game.Board[5, 4] = Color.White;
            game.Board[5, 5] = Color.White;
            game.Board[5, 6] = Color.None;
            game.Board[5, 7] = Color.Black;
            game.Board[6, 0] = Color.White;
            game.Board[6, 1] = Color.White;
            game.Board[6, 2] = Color.White;
            game.Board[6, 3] = Color.White;
            game.Board[6, 4] = Color.White;
            game.Board[6, 5] = Color.White;
            game.Board[6, 6] = Color.White;
            game.Board[6, 7] = Color.None;
            game.Board[7, 0] = Color.White;
            game.Board[7, 1] = Color.White;
            game.Board[7, 2] = Color.White;
            game.Board[7, 3] = Color.White;
            game.Board[7, 4] = Color.White;
            game.Board[7, 5] = Color.White;
            game.Board[7, 6] = Color.White;
            game.Board[7, 7] = Color.White;
            // 0 1 2 3 4 5 6 7
            // v
            // 0 1 1 1 1 1 1 1 1
            // 1 1 1 1 1 1 1 1 1
            // 2 1 1 1 1 1 1 1 1
            // 3 1 1 1 1 1 1 1 0
            // 4 1 1 1 1 1 1 0 0
            // 5 1 1 1 1 1 1 0 2
            // 6 1 1 1 1 1 1 1 0
            // 7 1 1 1 1 1 1 1 1
            // Act
            game.Moving = Color.White;
            var actual = game.Finished();
            // Assert
            Assert.IsTrue(actual);
        }
        [Test]
        public void Finished_NoneIsPossibleAllesBezet_ReturnTrue()
        {
            // Arrange (zowel White als Black kunnen niet meer)
            Game game = new Game();
            game.Board[0, 0] = Color.White;
            game.Board[0, 1] = Color.White;
            game.Board[0, 2] = Color.White;
            game.Board[0, 3] = Color.White;
            game.Board[0, 4] = Color.White;
            game.Board[0, 5] = Color.White;
            game.Board[0, 6] = Color.White;
            game.Board[0, 7] = Color.White;
            game.Board[1, 0] = Color.White;
            game.Board[1, 1] = Color.White;
            game.Board[1, 2] = Color.White;
            game.Board[1, 3] = Color.White;
            game.Board[1, 4] = Color.White;
            game.Board[1, 5] = Color.White;
            game.Board[1, 6] = Color.White;
            game.Board[1, 7] = Color.White;
            game.Board[2, 0] = Color.White;
            game.Board[2, 1] = Color.White;
            game.Board[2, 2] = Color.White;
            game.Board[2, 3] = Color.White;
            game.Board[2, 4] = Color.White;
            game.Board[2, 5] = Color.White;
            game.Board[2, 6] = Color.White;
            game.Board[2, 7] = Color.White;
            game.Board[3, 0] = Color.White;
            game.Board[3, 1] = Color.White;
            game.Board[3, 2] = Color.White;
            game.Board[3, 3] = Color.White;
            game.Board[3, 4] = Color.White;
            game.Board[3, 5] = Color.White;
            game.Board[3, 6] = Color.White;
            game.Board[3, 7] = Color.White;
            game.Board[4, 0] = Color.White;
            game.Board[4, 1] = Color.White;
            game.Board[4, 2] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[4, 4] = Color.White;
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.Black;
            game.Board[4, 7] = Color.Black;
            game.Board[5, 0] = Color.White;
            game.Board[5, 1] = Color.White;
            game.Board[5, 2] = Color.White;
            game.Board[5, 3] = Color.White;
            game.Board[5, 4] = Color.White;
            game.Board[5, 5] = Color.White;
            game.Board[5, 6] = Color.Black;
            game.Board[5, 7] = Color.Black;
            game.Board[6, 0] = Color.White;
            game.Board[6, 1] = Color.White;
            game.Board[6, 2] = Color.White;
            game.Board[6, 3] = Color.White;
            game.Board[6, 4] = Color.White;
            game.Board[6, 5] = Color.White;
            game.Board[6, 6] = Color.White;
            game.Board[6, 7] = Color.Black;
            game.Board[7, 0] = Color.White;
            game.Board[7, 1] = Color.White;
            game.Board[7, 2] = Color.White;
            game.Board[7, 3] = Color.White;
            game.Board[7, 4] = Color.White;
            game.Board[7, 5] = Color.White;
            game.Board[7, 6] = Color.White;
            game.Board[7, 7] = Color.White;
            // 0 1 2 3 4 5 6 7
            //
            // 0 1 1 1 1 1 1 1 1
            // 1 1 1 1 1 1 1 1 1
            // 2 1 1 1 1 1 1 1 1
            // 3 1 1 1 1 1 1 1 2
            // 4 1 1 1 1 1 1 2 2
            // 5 1 1 1 1 1 1 2 2
            // 6 1 1 1 1 1 1 1 2
            // 7 1 1 1 1 1 1 1 1
            // Act
            game.Moving = Color.White;
            var actual = game.Finished();
            // Assert
            Assert.IsTrue(actual);
        }
        [Test]
        public void Finished_WelIsPossible_ReturnFalse()
        {
            // Arrange
            Game game = new Game();
            // 0 1 2 3 4 5 6 7
            //
            // 0 0 0 0 0 0 0 0 0
            // 1 0 0 0 0 0 0 0 0
            // 2 0 0 0 0 0 0 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 0 0 0
            // 5 0 0 0 0 0 0 0 0
            // 6 0 0 0 0 0 0 0 0
            // 7 0 0 0 0 0 0 0 0
            //
            // Act
            game.Moving = Color.White;
            var actual = game.Finished();
            // Assert
            Assert.IsFalse(actual);
        }
        [Test]
        public void WinningColor_Gelijk_ReturnColorNone()
        {
            // Arrange
            Game game = new Game();
            // 0 1 2 3 4 5 6 7
            //
            // 0 0 0 0 0 0 0 0 0
            // 1 0 0 0 0 0 0 0 0
            // 2 0 0 0 0 0 0 0 0
            // 3 0 0 0 1 2 0 0 0
            // 4 0 0 0 2 1 0 0 0
            // 5 0 0 0 0 0 0 0 0
            // 6 0 0 0 0 0 0 0 0
            // 7 0 0 0 0 0 0 0 0
            //
            // Act
            var actual = game.WinningColor();
            // Assert
            Assert.AreEqual(Color.None, actual);
        }
        [Test]
        public void WinningColor_Black_ReturnColorBlack()
        {
            // Arrange
            Game game = new Game();
            game.Board[2, 3] = Color.Black;
            game.Board[3, 3] = Color.Black;
            game.Board[4, 3] = Color.Black;
            game.Board[3, 4] = Color.Black;
            game.Board[4, 4] = Color.White;
            // 0 1 2 3 4 5 6 7
            //
            // 0 0 0 0 0 0 0 0 0
            // 1 0 0 0 0 0 0 0 0
            // 2 0 0 0 2 0 0 0 0
            // 3 0 0 0 2 2 0 0 0
            // 4 0 0 0 2 1 0 0 0
            // 5 0 0 0 0 0 0 0 0
            // 6 0 0 0 0 0 0 0 0
            // 7 0 0 0 0 0 0 0 0
            //
            // Act
            var actual = game.WinningColor();
            // Assert
            Assert.AreEqual(Color.Black, actual);
        }
        [Test]
        public void WinningColor_White_ReturnColorWhite()
        {
            // Arrange
            Game game = new Game();
            game.Board[2, 3] = Color.White;
            game.Board[3, 3] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[3, 4] = Color.White;
            game.Board[4, 4] = Color.Black;
            // 0 1 2 3 4 5 6 7
            //
            // 0 0 0 0 0 0 0 0 0
            // 1 0 0 0 0 0 0 0 0
            // 2 0 0 0 1 0 0 0 0
            // 3 0 0 0 1 1 0 0 0
            // 4 0 0 0 1 2 0 0 0
            // 5 0 0 0 0 0 0 0 0
            // 6 0 0 0 0 0 0 0 0
            // 7 0 0 0 0 0 0 0 0
            //
            // Act
            var actual = game.WinningColor();
            // Assert
            Assert.AreEqual(Color.White, actual);
        }
    }
}
