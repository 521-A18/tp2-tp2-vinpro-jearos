using Moq;
using TP2.Models.Entities;
using TP2.Services.Interfaces;
using TP2.Services;
using Xunit;
using TP2.UnitTests.Constante;
using Prism.Services;
using System.Collections.Generic;
using TP2.Externalization;
using System.Threading.Tasks;

namespace TP2.UnitTests.Services
{
    public class RegisterServiceTest
    {
        private IRegisterService _registerService;
        private Mock<IRepository<User>> _mockRepository;
        private Mock<ISecureStorageService> _mockSercureStorageService;
        private Mock<IPageDialogService> _mockPageDialogService;
        private ICryptoService _cryptoService;

        private List<User> _list;

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
            _mockPageDialogService = new Mock<IPageDialogService>();
            _cryptoService = new CryptoService();

            _list = new List<User>();
            _list.Add(user);

            _mockRepository.Setup(x => x.GetAll()).Returns(_list);

            _registerService = new RegisterService(_mockRepository.Object, _mockSercureStorageService.Object, _mockPageDialogService.Object);
        }

        [Fact]
        private void RegisterUser_WhenNewUserHasSameEmail_ShouldDisplayAlert()
        {
            _registerService.RegisterUser("123", "456");

            _mockPageDialogService.Verify(x => x.DisplayAlertAsync(UiText.ALERT, UiText.EMAIL_ALREADY_EXIST, UiText.OK), Times.AtLeastOnce);
        }
    }
}
