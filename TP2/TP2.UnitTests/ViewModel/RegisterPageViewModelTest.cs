using System.ComponentModel;
using Moq;
using Prism.Navigation;
using TP2.Validations;
using TP2.ViewModels;
using Xunit;

namespace TP2.UnitTests.ViewModel
{
    public class RegisterPageViewModelTest
    {
        private readonly RegisterPageViewModel _registerPageViewModels;
        private Mock<INavigationService> _mockNavigationService;
        private bool _eventRaised = false;

        public RegisterPageViewModelTest()
        {
            _mockNavigationService = new Mock<INavigationService>();
            _registerPageViewModels = new RegisterPageViewModel(_mockNavigationService.Object);
        }

        [Fact]
        public void Email_WhenSetToNewValue_ShouldRaisePropertyChangedEvent()
        {
            _registerPageViewModels.PropertyChanged += RaiseProperty;

            _registerPageViewModels.Email = new ValidatableObject<string>();

            Assert.True(_eventRaised);
        }

        [Fact]
        public void Password_WhenSetToNewValue_ShouldRaisePropertyChangedEvent()
        {
            _registerPageViewModels.PropertyChanged += RaiseProperty;

            _registerPageViewModels.Password = new ValidatableObject<string>();

            Assert.True(_eventRaised);
        }

        [Fact]
        public void PasswordConfirm_WhenSetToNewValue_ShouldRaisePropertyChangedEvent()
        {
            _registerPageViewModels.PropertyChanged += RaiseProperty;

            _registerPageViewModels.PasswordConfirm = new ValidatableObject<string>();

            Assert.True(_eventRaised);
        }

        private void RaiseProperty(object sender, PropertyChangedEventArgs e)
        {
            _eventRaised = true;
        }
    }
}
