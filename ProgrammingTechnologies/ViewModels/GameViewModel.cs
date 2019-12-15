using ProgrammingTechnologies.BO.Models;
using ProgrammingTechnologies.DAL.Services;
using System.Collections.ObjectModel;

namespace ProgrammingTechnologies.ViewModels
{
    internal class GameViewModel
    {
        private readonly DatabaseService DatabaseService;
        private readonly GameService GameService;
        public GameViewModel()
        {
            DatabaseService = new DatabaseService();
            GameService = new GameService(DatabaseService);

            Games = new ObservableCollection<Game>(GameService.GetAllServicedObjects());
        }

        public ObservableCollection<Game> Games
        {
            get; set;
        }
    }
}
