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
    internal class GameViewModel : ViewModel<Game>
    {
        private UserManager UserManager { get; set; }
        private GameManager GameManager { get; set; }
        public GameViewModel()
        {
            Name = "Games";
            GameManager = new GameManager(ServiceProvider.GetDatabaseDependentServices);
            UserManager = new UserManager(ServiceProvider.GetDatabaseDependentServices);

            Items = new ObservableCollection<Game>(GameManager.GetAllManagedObjects());
            SelectedItem = Items[0];

            Users = new ObservableCollection<User>(UserManager.GetAllManagedObjects());

            // Assign game owners to games
            foreach (Game Game in Items)
            {
                Game.GameOwner = GetGameOwner(Game);
            }

            Categories = new ObservableCollection<string>(Enum.GetNames(typeof(EnumCategory)));

            SubmitCommand = new RelayCommand(() => Task.Run(() => UpdateItem()), SelectedItem.isValid);
            AddCommand = new RelayCommand(() => Task.Run(() => AddItem()));
            DeleteCommand = new RelayCommand(() => Task.Run(() => DeleteItem()), CanDeleteItem);
        }

        public ObservableCollection<User> Users
        {
            get; set;

        }
        public ObservableCollection<string> Categories
        {
            get; set;
        }

        private User GetGameOwner(Game Game)
        {
            return Users.Where(User => User.Id == Game.UserId).First();
        }

        protected override void AddItem()
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
                Items.Add(newGame);
                SelectedItem = newGame;
                SelectedItem.GameOwner = GetGameOwner(SelectedItem);
            });
        }
        protected override void UpdateItem()
        {
            Game gameToUpdate = SelectedItem;
            GameManager.UpdateManagedObject(ref gameToUpdate);
            SelectedItem.GameOwner = GetGameOwner(gameToUpdate);
        }

        protected override void DeleteItem()
        {
            int currentGameIndex = Items.IndexOf(SelectedItem);
            // Get previous game (but game at 0 if there is no game left)
            int previousGameIndex = currentGameIndex > 0 ? currentGameIndex - 1 : 0;
            GameManager.DeleteManagedObject(SelectedItem);

            // GUI can only be updated by using Dispatcher
            App.Current.Dispatcher.Invoke(() =>
            {
                Items.Remove(SelectedItem);
                SelectedItem = Items[previousGameIndex];
            });
        }
    }
}
