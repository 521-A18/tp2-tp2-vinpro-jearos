using TP2.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using TP2.Services.Interfaces;

namespace TP2.Services
{
    public class AuthentificationService : IAuthentificationService
    {
        private IRepository<User> _userDatabase;
        private ICryptoService _cryptoService;

        public AuthentificationService(IRepository<User> userDatabase, ICryptoService cryptoService)
        {
            _userDatabase = userDatabase;
            _cryptoService = cryptoService;
        }

        public bool IsUserAuthenticated { get; private set; }

        public int AuthenticatedUserId { get; private set; }

        public void LogIn(string login, string password)
        {
            try
            {
                bool IsValidUser = false;
                var database = _userDatabase.GetAll();
                foreach (var element in database)
                {
                    if (element.Login == login)
                    {
                        var userPasswordWithSalt = _cryptoService.HashSHA512(password, element.PasswordSalt);
                        if (userPasswordWithSalt == element.HashedPassword)
                        {
                            IsValidUser = true;
                            IsUserAuthenticated = true;
                            AuthenticatedUserId = element.Id;
                            break;
                        }
                    }
                }
                if (IsValidUser == false)
                {
                    IsUserAuthenticated = false;
                }
            }
            catch
            {
                IsUserAuthenticated = false;
            }
        }

    }
}
