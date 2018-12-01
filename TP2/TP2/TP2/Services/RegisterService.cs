using System.Linq;
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


        public RegisterService(IRepository<User> repository, ISecureStorageService secureStorageService)
        {
            _cryptoService = new CryptoService();
            _repository = repository;
            _secureStorageService = secureStorageService;
        }

        public void RegisterUser(string email, string password)
        {
            string salt = _cryptoService.GenerateSalt();
            User newUser = new User()
            {
                Login = email,
                HashedPassword = _cryptoService.HashSHA512(password, salt),
                PasswordSalt = salt
            };

            _repository.Add(newUser);
            _secureStorageService.SetEncryptionKeyAsync(_repository.GetAll().FirstOrDefault(x => x.Login == email).Id.ToString(), _cryptoService.GenerateEncryptionKey());
        }
    }
}
