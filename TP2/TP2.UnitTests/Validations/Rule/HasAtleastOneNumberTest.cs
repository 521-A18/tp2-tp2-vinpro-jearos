using TP2.Validations.Rules;
using Xunit;

namespace TP2.UnitTests.Validations.Rule
{
    public class HasAtleastOneNumberTest
    {
        private readonly HasAtleastOneNumber<string> _rule;

        public HasAtleastOneNumberTest()
        {
            _rule = new HasAtleastOneNumber<string>();
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
        [InlineData("testwithoutNumber")]
        public void Check_WhenPwDoesntContainNumber_ShouldReturnFalse(string badPw)
        {
            var isValid = _rule.Check(badPw);

            Assert.False(isValid);
        }

        [Fact]
        public void Check_WhenPwHasOneNumber_ShouldReturnTrue()
        {
            var isValid = _rule.Check("testWith1");

            Assert.True(isValid);
        }

        [Fact]
        public void Check_WhenPwAsMoreThanTwoNumber_ShouldReturnTrue()
        {
            var isValid = _rule.Check("testWithTwo12");

            Assert.True(isValid);
        }
    }
}
