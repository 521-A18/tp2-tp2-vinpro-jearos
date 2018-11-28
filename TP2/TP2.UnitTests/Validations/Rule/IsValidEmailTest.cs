using TP2.Validations.Rules;
using Xunit;

namespace TP2.UnitTests.Validations.Rule
{
    public class IsValidEmailTest
    {
        private readonly IsValidEmail<string> _rule;

        public IsValidEmailTest()
        {
            _rule = new IsValidEmail<string>();
        }

        [Fact]
        public void Check_WhenEmailIsNull_ShouldReturnFalse()
        {
            var isValid = _rule.Check(null);

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Check_WhenEmailIsEmpty_ShouldReturnFalse(string emptyString)
        {
            var isValid = _rule.Check(emptyString);

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("test.com@test")]
        public void Check_WhenEmailDotIsBeforeArobase_ShouldReturnFalse(string badEmail)
        {
            var isValid = _rule.Check(badEmail);

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("test@.com")]
        public void Check_WhenEmailAsNothingBetweenArobaseAndDot_ShouldReturnFalse(string badEmail)
        {
            var isValid = _rule.Check(badEmail);

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("EmailTest.ca")]
        [InlineData("EmailTest@ca")]
        public void Check_WhenEmailDoesntContainArobaseOrDot_ShouldReturnFalse(string badEmail)
        {
            var isValid = _rule.Check(badEmail);

            Assert.False(isValid);
        }

        [Theory]
        [InlineData("EmailTest@@test.ca")]
        [InlineData("EmailTest@tset..ca")]
        public void Check_WhenEmailContain2ArobaseOr2Dot_ShouldReturnFalse(string badEmail)
        {
            var isValid = _rule.Check(badEmail);

            Assert.False(isValid);
        }

        [Theory]
        //[InlineData("&d@d.c")] => true?? | verifier si nous pouvons mettre des caracters special avant le @
        [InlineData("d@test.?")]
        [InlineData("d@?.com")]
        public void Check_withSpecialCharacter_ShouldReturnFalse(string badEmail)
        {
            var isValid = _rule.Check(badEmail);

            Assert.False(isValid);
        }

        [Fact]
        public void Check_WhenEmailIsCorrect_ShouldReturnTrue()
        {
            var isValid = _rule.Check("goodEmailTest@test.ca");

            Assert.True(isValid);
        }
    }
}
