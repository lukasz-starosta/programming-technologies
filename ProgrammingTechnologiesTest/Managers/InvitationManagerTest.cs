using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammingTechnologies.BLL.Managers;
using ProgrammingTechnologies.BO.Models;
using ProgrammingTechnologies.Helpers;
using ProgrammingTechnologiesTest.Helpers;

namespace ProgrammingTechnologiesTest.Managers
{
    [TestClass]
    public class InvitationManagerTest
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            DatabaseSeeder.SeedDatabase();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            DatabaseSeeder.CleanDatabase();
        }

        [TestMethod]
        public void TestGetinvitationEvent()
        {
            InvitationManager invitationManager = new InvitationManager(ServiceProvider.GetDatabaseDependentServices);

            Invitation invitation = invitationManager.GetAllManagedObjects()[3];

            Event _event = invitationManager.GetInvitationEvent(invitation);

            Assert.AreEqual(invitation.EventId, _event.Id);
        }

        [TestMethod]
        public void TestGetInvitationUser()
        {
            InvitationManager invitationManager = new InvitationManager(ServiceProvider.GetDatabaseDependentServices);

            Invitation invitation = invitationManager.GetAllManagedObjects()[3];

            User user = invitationManager.GetInvitationUser(invitation);

            Assert.AreEqual(invitation.UserId, user.Id);
        }
    }
}
