using TP2.Validations.Interface;

namespace TP2.Validations.Rules
{
    public class HasMoreThanTenCharacters<T> : IValidationRule<T> 
    {
        public string ErrorMessage { get; set; }
        
        public bool Check(T value)
        {
            if (value == null) return false;

            if (value.ToString().Length < 10) return false;

            return true;
        }
    }
}
