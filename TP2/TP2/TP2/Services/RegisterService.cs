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
        private ISecureStorageService _secureStorageService;
        private IPageDialogService _pageDialogService;

        public RegisterService(IRepository<User> repository, ISecureStorageService secureStorageService, IPageDialogService pageDialogService)
        {
            _cryptoService = new CryptoService();
            _repository = repository;
            _secureStorageService = secureStorageService;
            _pageDialogService = pageDialogService;
        }

        public void RegisterUser(string email, string password)
        {
            string salt = _cryptoService.GenerateSalt();
            List<User> userList = new List<User>();
            userList = _repository.GetAll().ToList();

            if (CheckUser(email, userList))
            {
                User newUser = new User()
                {
                    Login = email,
                    HashedPassword = _cryptoService.HashSHA512(password, salt),
                    PasswordSalt = salt
                };

                _repository.Add(newUser);
                _secureStorageService.SetEncryptionKeyAsync(_repository.GetAll().FirstOrDefault(x => x.Login == email).Id.ToString(), _cryptoService.GenerateEncryptionKey());
            }
            else _pageDialogService.DisplayAlertAsync(UiText.ALERT, UiText.EMAIL_ALREADY_EXIST, UiText.OK);
        }

        public bool CheckUser(string email, List<User> list)
        {
            foreach (User user in list)
            {
                if (user.Login == email) return false;
            }

            return true;
        }
    }
}
