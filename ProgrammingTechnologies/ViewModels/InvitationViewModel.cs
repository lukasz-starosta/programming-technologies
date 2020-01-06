using ProgrammingTechnologies.BO.Models;

namespace ProgrammingTechnologies.ViewModels
{
    internal class InvitationViewModel : ViewModel<Invitation>
    {
        public InvitationViewModel()
        {
            Name = "Invitations";
        }

        protected override void AddItem()
        {
            throw new System.NotImplementedException();
        }

        protected override void DeleteItem()
        {
            throw new System.NotImplementedException();
        }

        protected override void UpdateItem()
        {
            throw new System.NotImplementedException();
        }
    }
}