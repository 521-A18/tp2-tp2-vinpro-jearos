using TP2.Validations.Interface;

namespace TP2.Validations.Rules
{
    public class HasAtleastOneNumber<T> : IValidationRule<T>
    {
        public string ErrorMessage { get; set; }

        public bool Check(T value)
        {
            throw new System.NotImplementedException();
        }
    }
}
