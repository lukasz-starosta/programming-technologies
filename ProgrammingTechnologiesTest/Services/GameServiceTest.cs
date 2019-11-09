using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammingTechnologies.Models;
using ProgrammingTechnologies.Services;

namespace ProgrammingTechnologiesTest.Services
{
    [TestClass]
    public class GameServiceTest
    {
        [TestMethod]
        public void TestCreateGame()
        {
            User user = new User()
            {
                Email = "test@test.com",
                Name = "Tester",
                LastName = "Testing",
                Password = "password"
            };
            UserService userService = new UserService(new DatabaseService());
            userService.CreateUser(user);
            user = userService.GetUserWhere("email = 'test@test.com'");
            Game game = new Game()
            {
                Title = "Test Title",
                Description = "Test Description",
                Category = 1,
                UserId = user.Id
            };
            GameService service = new GameService(new DatabaseService());
            service.CreateGame(game);
            service.DeleteGameWhere($"user_id = {user.Id}");
            userService.DeleteUser(user);
        }

        [TestMethod]
        public void TestGetGame()
        {
            User user = new User()
            {
                Email = "test@test.com",
                Name = "Tester",
                LastName = "Testing",
                Password = "password"
            };
            UserService userService = new UserService(new DatabaseService());
            userService.CreateUser(user);
            user = userService.GetUserWhere("email = 'test@test.com'");
            Game game = new Game()
            {
                Title = "Test Title",
                Description = "Test Description",
                Category = 1,
                UserId = user.Id
            };
            GameService service = new GameService(new DatabaseService());
            service.CreateGame(game);
            Game result = service.GetGameWhere($"title = '{game.Title}'");
            Assert.AreEqual(game.Title, result.Title);
            service.DeleteGameWhere($"user_id = {user.Id}");
            userService.DeleteUser(user);
        }

        [TestMethod]
        public void TestUpdateGame()
        {
            User user = new User()
            {
                Email = "test@test.com",
                Name = "Tester",
                LastName = "Testing",
                Password = "password"
            };
            UserService userService = new UserService(new DatabaseService());
            userService.CreateUser(user);
            user = userService.GetUserWhere("email = 'test@test.com'");
            Game game = new Game()
            {
                Title = "Test Title",
                Description = "Test Description",
                Category = 1,
                UserId = user.Id
            };
            GameService service = new GameService(new DatabaseService());
            service.CreateGame(game);
            game = service.GetGameWhere($"title = '{game.Title}'");
            game.Title = "wujaa";
            service.UpdateGame(game);
            Game result = service.GetGameWhere($"id = {game.Id}");
            Assert.AreEqual("wujaa", result.Title);
            service.DeleteGameWhere($"user_id = {user.Id}");
            userService.DeleteUser(user);
        }

        [TestMethod]
        public void TestDeleteGame()
        {

            User user = new User()
            {
                Email = "test@test.com",
                Name = "Tester",
                LastName = "Testing",
                Password = "password"
            };
            UserService userService = new UserService(new DatabaseService());
            userService.CreateUser(user);
            user = userService.GetUserWhere("email = 'test@test.com'");
            Game game = new Game()
            {
                Title = "Test Title",
                Description = "Test Description",
                Category = 1,
                UserId = user.Id
            };
            GameService service = new GameService(new DatabaseService());
            service.CreateGame(game);
            service.DeleteGameWhere($"user_id = {user.Id}");
            userService.DeleteUser(user);
        }
    }
}
