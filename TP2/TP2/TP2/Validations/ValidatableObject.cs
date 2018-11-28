using System.Collections.Generic;
using System.Linq;
using Prism.Mvvm;
using TP2.Validations.Interface;

namespace TP2.Validations
{
    public class ValidatableObject<T> : BindableBase, IValidatableObject<T>
    {
        private readonly List<IValidationRule<T>> _validationRules;
        private List<string> _errors;
        private T _value;
        private bool _isValid;


        public List<string> Errors
        {
            get => _errors;
            private set
            {
                _errors = value;
                RaisePropertyChanged();
            }
        }

        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                RaisePropertyChanged();
            }
        }

        public bool IsValid
        {
            get => _isValid;
            private set
            {
                _isValid = value;
                RaisePropertyChanged();
            }
        }

        public ValidatableObject()
        {
            _isValid = true;
            _errors = new List<string>();
            _validationRules = new List<IValidationRule<T>>();
        }

        public void Validate()
        {
            Errors.Clear();

            IEnumerable<string> errors = _validationRules
                .Where(v => !v.Check(Value))
                .Select(v => v.ErrorMessage);

            Errors = errors.ToList();
            IsValid = !Errors.Any();
        }

        public void AddValidationRule(IValidationRule<T> validationRule)
        {
            _validationRules.Add(validationRule);
        }
    }
}
