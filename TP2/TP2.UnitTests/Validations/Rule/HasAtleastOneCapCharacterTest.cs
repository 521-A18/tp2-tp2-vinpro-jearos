using TP2.Validations.Rules;
using Xunit;

namespace TP2.UnitTests.Validations.Rule
{
    public class HasAtleastOneCapCharacterTest
    {
        private readonly HasAtleastOneCapCharacter<string> _rule;

        public HasAtleastOneCapCharacterTest()
        {
            _rule = new HasAtleastOneCapCharacter<string>();
        }

        [Fact]
        public void Check_WhenPwIsNull_ShouldReturnFalse()
        {
            var isValid = _rule.Check(null);

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Check_WhenPwIsEmpty_ShouldReturnFalse(string emptyString)
        {
            var isValid = _rule.Check(emptyString);

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("test")]
        [InlineData("testwithoutcapital")]
        public void Check_WhenPwDoesntContainCapCharater_ShouldReturnFalse(string badPw)
        {
            var isValid = _rule.Check(badPw);

            Assert.False(isValid);
        }

        [Fact]
        public void Check_WhenPwHasOneCapCharacter_ShouldReturnTrue()
        {
            var isValid = _rule.Check("testWithcapital");

            Assert.True(isValid);
        }

        [Fact]
        public void Check_WhenPwAsMoreThanTwoCapCharacter_ShouldReturnTrue()
        {
            var isValid = _rule.Check("testWithTwocapitalletter");

            Assert.True(isValid);
        }
    }
}
