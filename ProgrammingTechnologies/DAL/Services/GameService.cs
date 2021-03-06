﻿using ProgrammingTechnologies.BO.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace ProgrammingTechnologies.DAL.Services
{
    public class GameService : IService<Game>
    {
        private DatabaseService database;

        public GameService(DatabaseService databaseService)
        {
            database = databaseService;
        }

        #region CRUD

        public void CreateServicedObject(ref Game game)
        {
            string instruction = string.Format("insert into Games (title, description, category, user_id) values " + 
                "('{0}', '{1}', {2}, {3})", game.Title, game.Description, game.Category, game.UserId );
            Console.WriteLine(instruction);
            database.ExecuteInstruction(instruction);
            game = GetServicedObjectWhere($"title = '{game.Title}' and description = '{game.Description}'");
        }

        public Game GetServicedObjectWhere(string condition)
        {
            string query = string.Format("select * from Games where {0}", condition);
            DataTable result = database.ExecuteQuery(query);
            return new Game()
            {
                Id = Convert.ToInt32(result.Rows[0]["id"]),
                Title = result.Rows[0]["title"].ToString(),
                Description = result.Rows[0]["description"].ToString(),
                Category = Convert.ToInt32(result.Rows[0]["category"]),
                UserId= Convert.ToInt32(result.Rows[0]["user_id"])
            };
        }

        public void UpdateServicedObject(ref Game game)
        {
            database.ExecuteInstruction(string.Format("update Games set title = '{0}', description = '{1}', category = {2}, user_id = {3} where id = {4}",
                game.Title, game.Description, game.Category, game.UserId, game.Id));
            game = GetServicedObjectWhere($"id = {game.Id}");
        }

        public void DeleteServicedObjectWhere(string condition)
        {
            database.ExecuteInstruction(string.Format("delete from Games where {0}", condition));
        }

        public void DeleteServicedObject(Game game)
        {
            database.ExecuteInstruction(string.Format("delete from Games where id = {0}", game.Id));
        }

        #endregion

        public List<Game> GetAllServicedObjects()
        {
            DataTable result = database.ExecuteQuery("select * from Games");
            List<Game> games = new List<Game>();
            foreach (DataRow row in result.Rows)
            {
                games.Add(new Game()
                {
                    Id = Convert.ToInt32(row["id"]),
                    Title = row["title"].ToString(),
                    Description = row["description"].ToString(),
                    Category = Convert.ToInt32(row["category"]),
                    UserId = Convert.ToInt32(row["user_id"])
                });
            }
            return games;
        }

        public List<Game> GetAllServicedObjectsWhere(string condition)
        {
            string query = string.Format("select * from Games where {0}", condition);
            DataTable result = database.ExecuteQuery(query);
            List<Game> games = new List<Game>();
            foreach (DataRow row in result.Rows)
            {
                games.Add(new Game()
                {
                    Id = Convert.ToInt32(row["id"]),
                    Title = row["title"].ToString(),
                    Description = row["description"].ToString(),
                    Category = Convert.ToInt32(row["category"]),
                    UserId = Convert.ToInt32(row["user_id"])
                });
            }
            return games;
        }
    }
}
