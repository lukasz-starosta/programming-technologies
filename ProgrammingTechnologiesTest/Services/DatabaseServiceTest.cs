using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammingTechnologies.Services;
using System;
using System.Data;

namespace ProgrammingTechnologiesTest.Services
{
    [TestClass]
    public class DatabaseServiceTest
    {

        [TestMethod]
        public void TestExecuteInsert()
        {
            DatabaseService database = new DatabaseService();
            database.ExecuteInstruction("insert into Users (name, last_name, email, password) VALUES ('Piotrek', 'Karczewski', 'test', 'password')");
            DataTable result = database.ExecuteQuery("select * from Users where name = 'Piotrek'");
            Assert.AreEqual("Piotrek", result.Rows[0]["name"].ToString());
        }

        [TestMethod]
        public void TestExecuteQuery()
        {
            DatabaseService database = new DatabaseService();
            DataTable result = database.ExecuteQuery("select * from Users where name = 'Piotrek'");
            Assert.AreEqual("Piotrek", result.Rows[0]["name"].ToString());
        }

        [TestMethod]
        public void TestExecuteUpdate()
        {
            DatabaseService database = new DatabaseService();
            database.ExecuteInstruction("update Users set last_name = 'Karczek' where  name = 'Piotrek' and email = 'test'");
            DataTable result = database.ExecuteQuery("select * from Users where name = 'Piotrek' and email = 'test'");
            Assert.AreEqual("Karczek", result.Rows[0]["last_name"].ToString());
        }

        [TestMethod]
        public void TestExecuteDelete()
        {
            DatabaseService database = new DatabaseService();
            database.ExecuteInstruction("delete from Users where name = 'Piotrek' and email = 'test'");
            DataTable result = database.ExecuteQuery("select * from Users where name = 'Piotrek' and email = 'test'");
            Assert.AreEqual(0, result.Rows.Count);
        }
    }
}
