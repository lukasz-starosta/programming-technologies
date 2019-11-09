using ProgrammingTechnologies.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace ProgrammingTechnologies.Services
{
    public class EventService : IService<Event>
    {
        private DatabaseService database;

        public EventService(DatabaseService databaseService)
        {
            database = databaseService;
        }

        #region CRUD

        public void CreateServicedObject(ref Event _event)
        {
            string instruction = string.Format("insert into Events (title, description, date, user_id, game_id) values " +
                "('{0}', '{1}', '{2}', {3}, {4})", _event.Title, _event.Description, _event.Date.ToString("yyyy-MM-dd HH:mm:ss.fff"), _event.UserId, _event.GameId);
            Console.WriteLine(instruction);
            database.ExecuteInstruction(instruction);
            _event = GetServicedObjectWhere($"title = '{_event.Title}' and date = '{_event.Date.ToString("yyyy - MM - dd HH: mm:ss.fff")}'");
        }

        public Event GetServicedObjectWhere(string condition)
        {
            string query = string.Format("select * from Events where {0}", condition);
            DataTable result = database.ExecuteQuery(query);
            return new Event()
            {
                Id = Convert.ToInt32(result.Rows[0]["id"]),
                Title = result.Rows[0]["title"].ToString(),
                Description = result.Rows[0]["description"].ToString(),
                Date = Convert.ToDateTime(result.Rows[0]["date"]),
                UserId = Convert.ToInt32(result.Rows[0]["user_id"]),
                GameId = Convert.ToInt32(result.Rows[0]["game_id"])
            };
        }

        public void UpdateServicedObject(ref Event _event)
        {
            database.ExecuteInstruction(string.Format("update Events set " +
                "title = '{0}', description = '{1}', date = '{2}', user_id = {3}, game_id = {4} where id = {5}",
                _event.Title, _event.Description, _event.Date.ToString("yyyy-MM-dd HH:mm:ss.fff"), _event.UserId, _event.GameId, _event.Id));
            _event = GetServicedObjectWhere($"id = {_event.Id}");
        }

        public void DeleteServicedObjectWhere(string condition)
        {
            database.ExecuteInstruction(string.Format("delete from Events where {0}", condition));
        }

        public void DeleteServicedObject(Event _event)
        {
            database.ExecuteInstruction(string.Format("delete from Events where id = {0}", _event.Id));
        }

        #endregion

        public List<Event> GetAllServicedObjects()
        {
            DataTable result = database.ExecuteQuery("select * from Events");
            List<Event> events = new List<Event>();
            foreach (DataRow row in result.Rows)
            {
                events.Add(new Event()
                {
                    Id = Convert.ToInt32(row["id"]),
                    Title = row["title"].ToString(),
                    Description = row["description"].ToString(),
                    Date = Convert.ToDateTime(row["date"]),
                    UserId = Convert.ToInt32(row["user_id"]),
                    GameId = Convert.ToInt32(row["game_id"])
                });
            }
            return events;
        }

        public List<Event> GetAllServicedObjectsWhere(string condition)
        {
            string query = string.Format("select * from Events where {0}", condition);
            DataTable result = database.ExecuteQuery(query);
            List<Event> events = new List<Event>();
            foreach (DataRow row in result.Rows)
            {
                events.Add(new Event()
                {
                    Id = Convert.ToInt32(row["id"]),
                    Title = row["title"].ToString(),
                    Description = row["description"].ToString(),
                    Date = Convert.ToDateTime(row["date"]),
                    UserId = Convert.ToInt32(row["user_id"]),
                    GameId = Convert.ToInt32(row["game_id"])
                });
            }
            return events;
        }
    }
}
