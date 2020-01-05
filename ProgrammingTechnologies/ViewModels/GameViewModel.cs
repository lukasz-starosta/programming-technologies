using ProgrammingTechnologies.BLL.Managers;
using ProgrammingTechnologies.BO.Models;
using ProgrammingTechnologies.Enums;
using ProgrammingTechnologies.Helpers;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProgrammingTechnologies.ViewModels
{
    internal class GameViewModel : ViewModel
    {
        private UserManager UserManager { get; set; }
        private GameManager GameManager { get; set; }
        public GameViewModel()
        {
            Name = "Games";
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
            AddGameCommand = new RelayCommand(() => Task.Run(() => AddGame()));
            DeleteGameCommand = new RelayCommand(() => Task.Run(() => DeleteGame()), CanDeleteGame);
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
        public ICommand AddGameCommand { get; private set; }
        public ICommand DeleteGameCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private User GetGameOwner(Game Game)
        {
            return Users.Where(User => User.Id == Game.UserId).First();
        }

        private void AddGame()
        {
            Game newGame = new Game()
            {
                // TODO: CHANGE THIS TO CURRENT USER
                UserId = 135,
                Title = "New game",
                Description = "",
                Category = (int)EnumCategory.Adventure,
            };

            GameManager.CreateManagedObject(ref newGame);

            // GUI can only be updated by using Dispatcher
            App.Current.Dispatcher.Invoke(() =>
            {
                Games.Add(newGame);
                SelectedGame = newGame;
                SelectedGame.GameOwner = GetGameOwner(SelectedGame);
            });
        }
        private void UpdateGame()
        {
            Game gameToUpdate = SelectedGame;
            GameManager.UpdateManagedObject(ref gameToUpdate);
            SelectedGame.GameOwner = GetGameOwner(gameToUpdate);
        }

        // Ensure at least 1 game is present
        private bool CanDeleteGame() { return Games.Count > 1; }
        private void DeleteGame()
        {
            int currentGameIndex = Games.IndexOf(SelectedGame);
            // Get previous game (but game at 0 if there is no game left)
            int previousGameIndex = currentGameIndex > 0 ? currentGameIndex - 1 : 0;
            GameManager.DeleteManagedObject(SelectedGame);

            // GUI can only be updated by using Dispatcher
            App.Current.Dispatcher.Invoke(() =>
            {
                Games.Remove(SelectedGame);
                SelectedGame = Games[previousGameIndex];
            });
        }
    }
}
