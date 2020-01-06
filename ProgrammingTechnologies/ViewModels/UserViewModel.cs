using ProgrammingTechnologies.BO.Models;

namespace ProgrammingTechnologies.ViewModels
{
    internal class UserViewModel : ViewModel<User>
    {
        public UserViewModel()
        {
            Name = "Users";
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