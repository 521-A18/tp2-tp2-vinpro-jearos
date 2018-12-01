using TP2.Models.Entities;
using TP2.Services.Interfaces;

namespace TP2.Services
{
    public class RegisterService : IRegisterService
    {
        private ICryptoService _cryptoService;
        private IRepository<User> _repository;
        
        public RegisterService(IRepository<User> repository)
        {
            _cryptoService = new CryptoService();
            _repository = repository;
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
        }
    }
}
