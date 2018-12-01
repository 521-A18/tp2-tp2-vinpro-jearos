using Moq;
using TP2.Models.Entities;
using TP2.Services.Interfaces;
using TP2.Services;
using Xunit;
using TP2.UnitTests.Constante;

namespace TP2.UnitTests.Services
{
    public class RegisterServiceTest
    {
        private IRegisterService _registerService;
        private Mock<IRepository<User>> _mockRepository;
        private Mock<ISecureStorageService> _mockSercureStorageService;

        public User user = new User()
        {
            Login = "123",
            HashedPassword = "456",
            PasswordSalt = "salty"
        };

        public RegisterServiceTest()
        {
            _mockRepository = new Mock<IRepository<User>>();
            _mockSercureStorageService = new Mock<ISecureStorageService>();
            _registerService = new RegisterService(_mockRepository.Object, _mockSercureStorageService.Object);
        }

        //[Fact]
        //private void RegisterUser_WhenNewUserIsCreate_ShouldAddUserToRepository()
        //{
           
        //    _registerService.RegisterUser(ConstanteTest.GOOD_EMAIL, ConstanteTest.GOOD_PASSWORD);

        //    _mockRepository.Verify(x => x.Add(user), Times.AtLeastOnce());
        //}
    }
}
