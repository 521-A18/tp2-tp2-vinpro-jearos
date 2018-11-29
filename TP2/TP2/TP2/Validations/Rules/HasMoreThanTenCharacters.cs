using System;
using System.Collections.Generic;
using System.Text;
using TP2.Validations.Interface;

namespace TP2.Validations.Rules
{
    public class HasMoreThanTenCharacters<T> : IValidationRule<T> 
    {
        public string ErrorMessage { get; set; }
        
        public bool Check(T value)
        {
            if (value == null) return false;

            string valueInString = value.ToString();

            if (valueInString.Length < 10) return false;

            return true;
        }
    }
}
