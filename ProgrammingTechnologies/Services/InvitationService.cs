using ProgrammingTechnologies.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace ProgrammingTechnologies.Services
{
    public class InvitationService
    {
        private DatabaseService database;

        public InvitationService(DatabaseService databaseService)
        {
            database = databaseService;
        }

        #region CRUD

        public void CreateInvitation(Invitation invitation)
        {
            string instruction = string.Format("insert into Invitations (user_id, event_id) values " +
                "({0}, {1})", invitation.UserId, invitation.EventId);
            Console.WriteLine(instruction);
            database.ExecuteInstruction(instruction);
        }

        public Invitation GetInvitationWhere(string condition)
        {
            string query = string.Format("select * from Invitations where {0}", condition);
            DataTable result = database.ExecuteQuery(query);
            return new Invitation()
            {
                Id = Convert.ToInt32(result.Rows[0]["id"]),
                UserId = Convert.ToInt32(result.Rows[0]["user_id"]),
                EventId = Convert.ToInt32(result.Rows[0]["event_id"])
            };
        }

        public void UpdateInvitation(Invitation invitation)
        {
            Console.WriteLine(invitation.UserId);
            database.ExecuteInstruction(string.Format("update Invitations set user_id = {0}, event_id = {1} where id = {2}",
                invitation.UserId, invitation.EventId, invitation.Id));
        }

        public void DeleteInvitationWhere(string condition)
        {
            database.ExecuteInstruction(string.Format("delete from Invitations where {0}", condition));
        }

        public void DeleteInvitation(Invitation invitation)
        {
            database.ExecuteInstruction(string.Format("delete from Invitations where id = {0}", invitation.Id));
        }

        #endregion

        public List<Invitation> GetAllInvitations()
        {
            DataTable result = database.ExecuteQuery("select * from Invitations");
            List<Invitation> invitations = new List<Invitation>();
            foreach (DataRow row in result.Rows)
            {
                invitations.Add(new Invitation()
                {
                    Id = Convert.ToInt32(row["id"]),
                    UserId = Convert.ToInt32(row["user_id"]),
                    EventId = Convert.ToInt32(row["event_id"])
                });
            }
            return invitations;
        }

        public List<Invitation> GetAllinvitationsWhere(string condition)
        {
            string query = string.Format("select * from Invitations where {0}", condition);
            DataTable result = database.ExecuteQuery(query);
            List<Invitation> invitations = new List<Invitation>();
            foreach (DataRow row in result.Rows)
            {
                invitations.Add(new Invitation()
                {
                    Id = Convert.ToInt32(row["id"]),
                    UserId = Convert.ToInt32(row["user_id"]),
                    EventId = Convert.ToInt32(row["event_id"])
                });
            }
            return invitations;
        }
    }
}
