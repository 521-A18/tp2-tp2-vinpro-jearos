using Bogus;
using TP2.Models.Entities;
using TP2.Services;
using Moq;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Xunit;
using TP2.Services.Interfaces;

namespace TP2.UnitTests.Services
{
    public class AuthentificationServiceTests
    {
        private List<User> _userList;
        private User _userTest;
        private Mock<IRepository<User>> _userRepository;
        private Mock<ICryptoService> _cryptoService;
        private AuthentificationService _authentificationService;

        public AuthentificationServiceTests()
        {
            _userList = new List<User>();
            _userTest = new User()
            {
                Login = "123",
                HashedPassword = "password",
                PasswordSalt = "salt"
            };
            _userList.Add(_userTest);
            _userRepository = new Mock<IRepository<User>>();
            _cryptoService = new Mock<ICryptoService>();
            _userRepository.Setup(user => user.GetAll()).Returns(_userList);
            _authentificationService = new AuthentificationService(_userRepository.Object, _cryptoService.Object);
        }

        [Fact]
        public void CheckLogin_WhenUserNameAndUserPasswordIsValid_ShouldReturnTrue()
        {
            string userToTest = "123";
            string passwordToTest = "password";
            _cryptoService.Setup(hash => hash.HashSHA512(passwordToTest, _userTest.PasswordSalt)).Returns(passwordToTest);
            _authentificationService.LogIn(userToTest, passwordToTest);
            Assert.True(_authentificationService.IsUserAuthenticated);
        }

        [Fact]
        public void CheckLogin_WhenUserPasswordIsNotValid_ShouldReturnFalse()
        {
            string userToTest = "123";
            string passwordToTest = "passwod";
            _cryptoService.Setup(hash => hash.HashSHA512(passwordToTest, _userTest.PasswordSalt)).Returns(passwordToTest);
            _authentificationService.LogIn(userToTest, passwordToTest);
            Assert.False(_authentificationService.IsUserAuthenticated);
        }

        [Fact]
        public void CheckLogin_WhenUserNameIsNotValid_ShouldReturnFalse()
        {
            string userToTest = null;
            string passwordToTest = "password";
            _authentificationService.LogIn(userToTest, passwordToTest);
            Assert.False(_authentificationService.IsUserAuthenticated);
        }
    }
}
