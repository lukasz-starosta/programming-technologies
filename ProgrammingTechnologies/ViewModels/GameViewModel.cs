using ProgrammingTechnologies.BLL.Managers;
using ProgrammingTechnologies.BO.Models;
using ProgrammingTechnologies.Enums;
using ProgrammingTechnologies.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProgrammingTechnologies.ViewModels
{
    internal class GameViewModel
    {
        private UserManager UserManager { get; set; }
        private GameManager GameManager { get; set; }
        public GameViewModel()
        {
            GameManager = new GameManager(ServiceProvider.GetDatabaseDependentServices);
            UserManager = new UserManager(ServiceProvider.GetDatabaseDependentServices);

            Games = new ObservableCollection<Game>(GameManager.GetAllManagedObjects());
            SelectedGame = Games[0];

            Users = new ObservableCollection<User>(UserManager.GetAllManagedObjects());

            // Assign game owners to games
            foreach (Game Game in Games)
            {
                Game.GameOwner = GetGameOwner(Game);
            }

            Categories = new ObservableCollection<string>(Enum.GetNames(typeof(EnumCategory)));

            SubmitGameCommand = new RelayCommand(() => Task.Run(() => UpdateGame()), () => SelectedGame.IsValid);
        }

        public Game SelectedGame
        {
            get; set;
        }

        public ObservableCollection<Game> Games
        {
            get; set;

        }
        public ObservableCollection<User> Users
        {
            get; set;

        }
        public ObservableCollection<string> Categories
        {
            get; set;
        }

        public ICommand SubmitGameCommand { get; private set; }

        private User GetGameOwner(Game Game)
        {
           return Users.Where(User => User.Id == Game.UserId).First();
        }

        private void UpdateGame()
        {
            Game newGame = SelectedGame;
            GameManager.UpdateManagedObject(ref newGame);
        }
    }
}
