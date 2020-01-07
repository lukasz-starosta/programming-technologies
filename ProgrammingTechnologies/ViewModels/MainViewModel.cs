namespace ProgrammingTechnologies.ViewModels
{
    internal class MainViewModel
    {
        public GameViewModel GameViewModel { get; set; }
        public EventViewModel EventViewModel { get; set; }
        public InvitationViewModel InvitationViewModel { get; set; }
        public UserViewModel UserViewModel { get; set; }

        public MainViewModel()
        {
            GameViewModel = new GameViewModel();
            EventViewModel = new EventViewModel();
            InvitationViewModel = new InvitationViewModel();
            UserViewModel = new UserViewModel();
        }
    }
}
