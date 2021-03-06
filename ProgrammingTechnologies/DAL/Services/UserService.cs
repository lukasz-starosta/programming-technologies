﻿using ProgrammingTechnologies.BO.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace ProgrammingTechnologies.DAL.Services
{
    /// <summary>
    /// Service responsible for operations performed on User model.
    /// </summary>
    public class UserService : IService<User>
    {
        DatabaseService database;

        public UserService(DatabaseService databaseService)
        {
            database = databaseService;
        }

        #region CRUD

        public void CreateServicedObject(ref User user)
        {
            string instruction = string.Format("insert into Users (name, last_name, email, password) values" +
                                               "('{0}', '{1}', '{2}', '{3}')", user.Name, user.LastName, user.Email, user.Password);
            database.ExecuteInstruction(instruction);
            user = GetServicedObjectWhere($"email = '{user.Email}'");
        }

        public User GetServicedObjectWhere(string condition)
        {
            string query = string.Format("select * from Users where {0}", condition);
            DataTable result = database.ExecuteQuery(query);
            if (result == null || result.Rows.Count == 0) return null;
            return new User()
            {
                Id = Convert.ToInt32(result.Rows[0]["id"]),
                Name = result.Rows[0]["name"].ToString(),
                LastName = result.Rows[0]["last_name"].ToString(),
                Email = result.Rows[0]["email"].ToString(),
                Password = result.Rows[0]["password"].ToString()
            };
        }

        public void UpdateServicedObject(ref User user)
        {
            database.ExecuteInstruction(string.Format("update Users set name = '{0}', last_name = '{1}', email = '{2}', password = '{3}' where id = {4}",
                user.Name, user.LastName, user.Email, user.Password, user.Id));
            user = GetServicedObjectWhere($"id = {user.Id}");
        }

        public void DeleteServicedObjectWhere(string condition)
        {
            database.ExecuteInstruction(string.Format("delete from Users where {0}", condition));
        }

        public void DeleteServicedObject(User user)
        {
            database.ExecuteInstruction(string.Format("delete from Users where id = {0}", user.Id));
        }

        #endregion

        public List<User> GetAllServicedObjects()
        {
            DataTable result = database.ExecuteQuery("select * from Users");
            List<User> users = new List<User>();
            foreach (DataRow row in result.Rows)
            {
                users.Add(new User()
                {
                    Id = Convert.ToInt32(row["id"]),
                    Name = row["name"].ToString(),
                    LastName = row["last_name"].ToString(),
                    Email = row["email"].ToString(),
                    Password = row["password"].ToString()
                });
            }
            return users;
        }

        public List<User> GetAllServicedObjectsWhere(string condition)
        {
            string query = string.Format("select * from Users where {0}", condition);
            DataTable result = database.ExecuteQuery(query);
            List<User> users = new List<User>();
            foreach (DataRow row in result.Rows)
            {
                users.Add(new User()
                {
                    Id = Convert.ToInt32(row["id"]),
                    Name = row["name"].ToString(),
                    LastName = row["last_name"].ToString(),
                    Email = row["email"].ToString(),
                    Password = row["password"].ToString()
                });
            }
            return users;
        }
    
    }
}
