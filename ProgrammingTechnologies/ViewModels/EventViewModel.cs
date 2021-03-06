﻿using ProgrammingTechnologies.BLL.Managers;
using ProgrammingTechnologies.BO.Models;
using ProgrammingTechnologies.Helpers;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingTechnologies.ViewModels
{
    internal class EventViewModel : ViewModel<Event>
    {
        private EventManager EventManager { get; set; }
        public EventViewModel(ref User currentUser, ref ObservableCollection<Game> games, ref ObservableCollection<Event> events)
        {
            Name = "Events";
            Items = events;
            Games = games;

            CurrentUser = currentUser;

            EventManager = new EventManager(ServiceProvider.GetDatabaseDependentServices);

            if (Items.Count > 0)
            {
                SelectedItem = Items[0];

                foreach (Event e in Items)
                {
                    e.Game = GetEventGame(e);
                }
            }

            SubmitCommand = new RelayCommand(() => Task.Run(() => UpdateItem()), () => SelectedItem != null && SelectedItem.isValid());
            AddCommand = new RelayCommand(() => Task.Run(() => AddItem()));
            DeleteCommand = new RelayCommand(() => Task.Run(() => DeleteItem()), () => SelectedItem != null && CanDeleteItem());
        }

        public ObservableCollection<Game> Games
        {
            get; set;

        }
        public Game SelectedGame { get; set; }
        
        public ObservableCollection<string> Categories
        {
            get; set;
        }

        private Game GetEventGame(Event e)
        {
            return Games.Where(Game => Game.Id == e.GameId).FirstOrDefault();
        }

        protected override void AddItem()
        {
            Event newEvent = new Event()
            {
                Title = "New event",
                Description = "",
                Date = System.DateTime.Now,
                UserId = CurrentUser.Id,
                GameId = Games[0].Id
            };

            EventManager.CreateManagedObject(ref newEvent);

            // GUI can only be updated by using Dispatcher
            App.Current.Dispatcher.Invoke(() =>
            {
                Items.Add(newEvent);
                newEvent.Game = GetEventGame(newEvent);
                SelectedItem = newEvent;
            });
        }
        protected override void UpdateItem()
        {
            Event eventToUpdate = SelectedItem;
            EventManager.UpdateManagedObject(ref eventToUpdate);
            SelectedItem.Game = GetEventGame(eventToUpdate);
        }

        protected override void DeleteItem()
        {
            int currentItemIndex = Items.IndexOf(SelectedItem);
            // Get previous game (but game at 0 if there is no game left)
            int previousItemIndex = currentItemIndex > 0 ? currentItemIndex - 1 : 0;
            EventManager.DeleteManagedObject(SelectedItem);

            // GUI can only be updated by using Dispatcher
            App.Current.Dispatcher.Invoke(() =>
            {
                Items.Remove(SelectedItem);
                SelectedItem = Items[previousItemIndex];
            });
        }
    }
}