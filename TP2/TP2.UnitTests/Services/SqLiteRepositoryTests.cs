using Bogus;
using TP2.Models.Entities;
using TP2.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace TP2.UnitTests.Services
{
    public class SqLiteRepositoryTests
    {
        private readonly SqLiteRepository<User> _repository;
        private readonly SQLiteConnection _databaseSqLiteConnection;
        private readonly User _userToTest;

        public SqLiteRepositoryTests()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var databasePath = Path.Combine(path, "MyDatabaseTest.db3");
            if (File.Exists(databasePath))
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                File.Delete(databasePath);
            }
            _databaseSqLiteConnection = new SQLiteConnection(databasePath);
            _repository = new SqLiteRepository<User>(_databaseSqLiteConnection);
            var userToAdd = new Faker<User>()
                    .StrictMode(true)
                    .RuleFor(u => u.Id, f => f.IndexFaker)
                    .RuleFor(u => u.Login, f => f.Person.UserName)
                    .RuleFor(u => u.PasswordSalt, f => f.Person.UserName)
                    .RuleFor(u => u.HashedPassword, f => f.Person.UserName)
                    .Generate(1);
            _userToTest = userToAdd[0];
            _repository.Add(userToAdd[0]);
        }

        [Fact]
        public void Add_WhenProductIsValid_ShouldAddItToTheDataBase()
        {
            var userAdded = _databaseSqLiteConnection.Find<User>(_userToTest.Id);

            AssertAllProductFiledsAreSame(_userToTest, userAdded);
        }

        private void AssertAllProductFiledsAreSame(User expectedUser, User actualUser)
        {
            //Todo: utiliser un farmework comme fluent assertion pour éviter de comparer manuellement chacune des propriétés
            Assert.Equal(expectedUser.Id, actualUser.Id);
            Assert.Equal(expectedUser.Login, actualUser.Login);
            Assert.Equal(expectedUser.PasswordSalt, actualUser.PasswordSalt);
            Assert.Equal(expectedUser.HashedPassword, actualUser.HashedPassword);
        }

    }
}
