using TP2.Validations.Rules;
using Xunit;

namespace TP2.UnitTests.Validations.Rule
{
    public class HasAtleastOneLowercaseCharacterTest
    {
        private readonly HasAtleastOneLowercaseCharacter<string> _rule;

        public HasAtleastOneLowercaseCharacterTest()
        {
            _rule = new HasAtleastOneLowercaseCharacter<string>();
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
        [InlineData("TEST")]
        [InlineData("TESTWITHOUTLOWERCASE")]
        public void Check_WhenPwDoesntContainLowercaseCharater_ShouldReturnFalse(string badPw)
        {
            var isValid = _rule.Check(badPw);

            Assert.False(isValid);
        }

        [Fact]
        public void Check_WhenPwHasOneLowercaseCharacter_ShouldReturnTrue()
        {
            var isValid = _rule.Check("TESTwITHLOWERCASE");

            Assert.True(isValid);
        }

        [Fact]
        public void Check_WhenPwAsMoreThanTwoLowercaseCharacter_ShouldReturnTrue()
        {
            var isValid = _rule.Check("TESTwiTHLOWERCASE");

            Assert.True(isValid);
        }
    }
}
