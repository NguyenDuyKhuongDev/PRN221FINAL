using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace FinalPRN221.Models.ViewModel
{
    public class AccountEditVM
    {
        private const string LimitDOB = "01/01/1920";
        private const string LimitStartDate = "01/01/2000";
        public string ID { get; set; }
        public string? Name { get; set; }

        [DateAttributeGreaterThanDateTime(LimitDOB, ErrorMessage = "Date of Birth must be after 01/01/1920.")]
        public DateTime? DateOfBirth { get; set; }

        public string? Address { get; set; }
        public bool? Gender { get; set; }
        public int? DepartmentID { get; set; }
        public int? PositionID { get; set; }

        [DateAttributeGreaterThanDateTime(LimitStartDate, ErrorMessage = "Start Date must be after 01/01/2000.")]
        public DateTime? StartDate { get; set; }
    }

    public class DateAttributeGreaterThanDateTimeAttribute : ValidationAttribute
    {
        private readonly DateTime _comparisonDate;

        public DateAttributeGreaterThanDateTimeAttribute(string comparisonDate)
        {
            if (string.IsNullOrEmpty(comparisonDate))
            {
                throw new ArgumentException("Invalid date format");
            }
            if (!DateTime.TryParse(comparisonDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out _comparisonDate))
            {
                throw new ArgumentException("Invalid date format");
            }
        }

        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;
            if (value is not DateTime myDateTime) return new ValidationResult("Invalid format date");
            if (myDateTime < _comparisonDate) return new ValidationResult(ErrorMessage ?? $"Date must be greater than {_comparisonDate:yyyy-MM-dd}");
            return ValidationResult.Success;
        }
    }
}