using System;
using System.Collections.Generic;
using System.Text;

namespace Music.API.Services
{
    public class ValidationResult
    {
        public bool Success { get; private set; }
        public string ErrorMessage { get; private set; }

        public static ValidationResult Ok => new ValidationResult { Success = true };
        public static ValidationResult ErrorOf(string message)
        {
            return new ValidationResult { Success = false, ErrorMessage = message };
        }

        private ValidationResult()
        {

        }
    }
}
