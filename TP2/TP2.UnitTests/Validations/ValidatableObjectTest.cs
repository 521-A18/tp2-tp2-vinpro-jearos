using System.ComponentModel;
using TP2.UnitTests.Mock;
using TP2.Validations;
using Xunit;

namespace TP2.UnitTests.Validations
{
    public class ValidatableObjectTest
    {
        private readonly ValidatableObject<string> _validatableObject;
        private readonly MockValidationRule<string> _mockValidationRule;
        private bool _eventRaised;

        public ValidatableObjectTest()
        {
            _mockValidationRule = new MockValidationRule<string>();
            _validatableObject = new ValidatableObject<string>();

        }

        [Fact]
        public void Validate_WhenNoRuleHasBeenAdded_ThenValidatableObjectShouldBeValid()
        {
            _validatableObject.Validate();

            Assert.True(_validatableObject.IsValid);
        }

        [Fact]
        public void Validate_WhenNoRuleHasBeenAdded_ThenValidatableObjectShouldContainNoError()
        {
            _validatableObject.Validate();

            Assert.Empty(_validatableObject.Errors);
        }

        [Fact]
        public void Validate_WhenRuleIsHonored_ThenValidatableObjectShouldBeValid()
        {
            _validatableObject.AddValidationRule(_mockValidationRule);
            _mockValidationRule.SetCheckReturnValueTo(true);

            _validatableObject.Validate();

            Assert.True(_validatableObject.IsValid);
        }


        [Fact]
        public void Validate_WhenRuleIsHonored_ThenValidatableObjectShouldContainsNoError()
        {
            _validatableObject.AddValidationRule(_mockValidationRule);
            _mockValidationRule.SetCheckReturnValueTo(true);

            _validatableObject.Validate();

            Assert.Empty(_validatableObject.Errors);
        }

        [Fact]
        public void Validate_WhenRuleIsNotHonored_ThenValidatableObjectShouldNotBeValid()
        {
            _validatableObject.AddValidationRule(_mockValidationRule);
            _mockValidationRule.SetCheckReturnValueTo(false);

            _validatableObject.Validate();

            Assert.False(_validatableObject.IsValid);
        }

        [Fact]
        public void Validate_WhenRuleIsNotHonored_ThenRuleErrorShouldBeAddedToValidatableObjectErrors()
        {
            _validatableObject.AddValidationRule(_mockValidationRule);
            _mockValidationRule.SetCheckReturnValueTo(false);

            _validatableObject.Validate();

            Assert.Single(_validatableObject.Errors);
        }

        [Fact]
        public void Validate_WhenRulesAreNotHonored_ThenValidatableObjectShouldNotBeValid()
        {
            _validatableObject.AddValidationRule(_mockValidationRule);
            _validatableObject.AddValidationRule(_mockValidationRule);
            _mockValidationRule.SetCheckReturnValueTo(false);

            _validatableObject.Validate();

            Assert.False(_validatableObject.IsValid);
        }

        [Fact]
        public void Validate_WhenRulesAreNotHonored_ThenRulesErrorShouldBeAddedToValidatableObjectErrors()
        {
            const int NUMBER_OF_RULES_ADDED = 2;

            _validatableObject.AddValidationRule(_mockValidationRule);
            _validatableObject.AddValidationRule(_mockValidationRule);
            _mockValidationRule.SetCheckReturnValueTo(false);

            _validatableObject.Validate();

            Assert.Equal(NUMBER_OF_RULES_ADDED, _validatableObject.Errors.Count);
        }

        [Fact]
        public void Errors_WhenSetToNewValue_ShouldRaisePropertyChangedEvent()
        {
            _validatableObject.PropertyChanged += RaiseProperty;
            _validatableObject.AddValidationRule(_mockValidationRule);
            _mockValidationRule.SetCheckReturnValueTo(false);

            _validatableObject.Validate();

            Assert.True(_eventRaised);
        }

        [Fact]
        public void IsValid_WhenSetToNewValue_ShouldRaisePropertyChangedEvent()
        {
            _validatableObject.PropertyChanged += RaiseProperty;
            _validatableObject.AddValidationRule(_mockValidationRule);
            _mockValidationRule.SetCheckReturnValueTo(false);

            _validatableObject.Validate();

            Assert.True(_eventRaised);
        }

        [Fact]
        public void Value_WhenSetToNewValue_ShouldRaisePropertyChangedEvent()
        {
            _validatableObject.PropertyChanged += RaiseProperty;

            _validatableObject.Value = "new value";

            Assert.True(_eventRaised);
        }

        private void RaiseProperty(object sender, PropertyChangedEventArgs e)
        {
            _eventRaised = true;
        }
    }
}
