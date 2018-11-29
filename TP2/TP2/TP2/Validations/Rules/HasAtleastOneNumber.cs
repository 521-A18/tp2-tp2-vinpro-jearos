using System.Linq;
using TP2.Validations.Interface;

namespace TP2.Validations.Rules
{
    public class HasAtleastOneNumber<T> : IValidationRule<T>
    {
        public string ErrorMessage { get; set; }

        public bool Check(T value)
        {
            if (value == null) return false;

            string valueInString = value.ToString();

            return valueInString.Any(char.IsDigit);
        }
    }
}
