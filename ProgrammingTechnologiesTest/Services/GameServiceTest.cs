using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammingTechnologies.Models;
using ProgrammingTechnologies.Services;

namespace ProgrammingTechnologiesTest.Services
{
    [TestClass]
    public class GameServiceTest
    {
        private User user;

        private Game GetNewGame()
        {
            return new Game()
            {
                Title = "Test Title",
                Description = "Test Description",
                Category = 1,
                UserId = user.Id
            };
        }

        [TestInitialize]
        public void TestInitialize()
        {
            UserService userService = new UserService(new DatabaseService());

            user = new User()
            {
                Email = "test@test.com",
                Name = "Tester",
                LastName = "Testing",
                Password = "password"
            };

            userService.CreateUser(ref user);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            DatabaseService databaseService = new DatabaseService();

            databaseService.ExecuteInstruction("delete from Games");
            databaseService.ExecuteInstruction("delete from Users");
        }

        [TestMethod]
        public void TestCreateGame()
        {
            Game game = GetNewGame();
            GameService gameService = new GameService(new DatabaseService());
            gameService.CreateGame(ref game);
            Assert.IsNotNull(game.Id);
        }

        [TestMethod]
        public void TestGetGame()
        {
            Game game = GetNewGame();
            GameService gameService = new GameService(new DatabaseService());
            gameService.CreateGame(ref game);
            Game result = gameService.GetGameWhere($"id = {game.Id}");
            Assert.AreEqual(game.Title, result.Title);
        }

        [TestMethod]
        public void TestUpdateGame()
        {
            Game game = GetNewGame();
            GameService gameService = new GameService(new DatabaseService());
            gameService.CreateGame(ref game);
            game.Title = "wujaa";
            gameService.UpdateGame(ref game);
            Game result = gameService.GetGameWhere($"id = {game.Id}");
            Assert.AreEqual("wujaa", result.Title);
        }

        [TestMethod]
        public void TestDeleteGame()
        {
            Game game = GetNewGame();
            GameService gameService = new GameService(new DatabaseService());
            gameService.CreateGame(ref game);
            int gamesBefore = gameService.GetAllGames().Count;
            gameService.DeleteGameWhere($"id = {game.Id}");
            int gamesAfter = gameService.GetAllGames().Count;
            Assert.AreEqual(1, gamesBefore - gamesAfter);
        }
    }
}
