using System.Collections.Generic;
using System.Linq;
using Prism.Services;
using TP2.Externalization;
using TP2.Models.Entities;
using TP2.Services.Interfaces;
using Xamarin.Essentials;

namespace TP2.Services
{
    public class RegisterService : IRegisterService
    {
        private ICryptoService _cryptoService;
        private IRepository<User> _repository;
        private IFavoriteRegionListService _favoriteRegionListService;
        private List<User> _userList;

        public RegisterService(IRepository<User> repository, IFavoriteRegionListService favoriteRegionListService)
        {
            _cryptoService = new CryptoService();
            _repository = repository;
            _favoriteRegionListService = favoriteRegionListService;
        }

        public void RegisterUser(string email, string password)
        {
            string salt = _cryptoService.GenerateSalt();
             User newUser = new User()
             {
                 Login = email,
                 HashedPassword = _cryptoService.HashSHA512(password, salt),
                 PasswordSalt = salt,
             };
             _favoriteRegionListService.AddUserFavoriteList(newUser.Login);
             _repository.Add(newUser);
        }

        public bool CheckUser(string email)
        {
            _userList = _repository.GetAll().ToList();
            foreach (User user in _userList)
            {
                if (user.Login == email) return true;
            }

            return false;
        }
    }
}
