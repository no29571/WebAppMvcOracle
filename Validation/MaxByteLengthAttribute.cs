using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebAppMvc.Validation
{
    public class MaxByteLengthAttribute : ValidationAttribute
    {
        public MaxByteLengthAttribute(int length)
            => Length = length;

        public int Length { get; }

        public string GetErrorMessage() =>
            $"The maximum length is {Length} bytes.";

        protected override ValidationResult? IsValid(
            object? value, ValidationContext validationContext)
        {
            //var student = (Student)validationContext.ObjectInstance;
            var str = (string?)value;
            if (str == null)
            {
                return ValidationResult.Success;
            }
            else
            {
                int len = Encoding.GetEncoding("UTF-8").GetBytes(str).Length;
                if (len <= Length)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }
        }
    }
}
