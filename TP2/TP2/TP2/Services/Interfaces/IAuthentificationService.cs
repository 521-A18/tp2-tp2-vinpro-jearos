using System;
using System.Collections.Generic;
using System.Text;

namespace TP2.Services.Interfaces
{
    public interface IAuthentificationService
    {
        bool IsUserAuthenticated { get; }
        int AuthenticatedUserId { get; }
        string AuthenticatedUserName { get; }
        void LogIn(string login, string password);
    }
}
