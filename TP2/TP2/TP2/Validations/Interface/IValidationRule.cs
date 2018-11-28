namespace TP2.Validations.Interface
{
    public interface IValidationRule<T>
    {
        string ErrorMessage { get; set; }

        bool Check(T value);
    }
}
