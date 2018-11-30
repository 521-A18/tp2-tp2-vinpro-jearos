using TP2.Validations.Rules;
using Xunit;

namespace TP2.UnitTests.Validations.Rule
{
    public class HasMoreThanTenCharactersTest
    {
        private readonly HasMoreThanTenCharacters<string> _rule;

        public HasMoreThanTenCharactersTest()
        {
            _rule = new HasMoreThanTenCharacters<string>();
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
        [InlineData("testShort")]
        public void Check_WhenPwHasLessThanTenCharacter_ShouldReturnFalse(string badPw)
        {
            var isValid = _rule.Check(badPw);

            Assert.False(isValid);
        }

        [Fact]
        public void Check_WhenPwHasTenCharacter_ShouldReturnTrue()
        {
            var isValid = _rule.Check("testTestTe");

            Assert.True(isValid);
        }

        [Fact]
        public void Check_WhenPwHasMoreThanTenCharacter_ShouldReturnTrue()
        {
            var isValid = _rule.Check("testWithMoreThanTenLetter");

            Assert.True(isValid);
        }
    }
}
