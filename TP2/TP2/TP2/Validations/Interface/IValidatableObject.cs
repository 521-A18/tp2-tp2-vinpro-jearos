using System.Collections.Generic;

namespace TP2.Validations.Interface
{
    public interface IValidatableObject<T>
    {
        List<string> Errors { get; }
        T Value { get; set; }
        bool IsValid { get; }
        void Validate();
        void AddValidationRule(IValidationRule<T> validationRule);
    }
}
