using System;
using System.Collections.Generic;
using System.Text;
using TP2.Validations.Interface;

namespace TP2.UnitTests.Mock
{
    public class MockValidationRule<T> : IValidationRule<T>
    {

        private bool _checkReturnValue;

        public string ErrorMessage { get; set; }

        public MockValidationRule()
        {
            ErrorMessage = "Error";
        }
        public bool Check(T value)
        {
            return _checkReturnValue;
        }

        public void SetCheckReturnValueTo(bool checkValueToReturn)
        {
            _checkReturnValue = checkValueToReturn;
        }
    }
}
