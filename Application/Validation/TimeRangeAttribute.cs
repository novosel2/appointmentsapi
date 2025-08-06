using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation;

public class TimeRangeAttribute : ValidationAttribute
{
    private readonly string _startPropertyName;

    public TimeRangeAttribute(string startPropertyName)
    {
        _startPropertyName = startPropertyName;
        ErrorMessage = "End time must be later than start time.";
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("End time is required.");
        }

        var endTime = (TimeOnly)value;

        var startProperty = validationContext.ObjectType.GetProperty(_startPropertyName);

        if (startProperty == null)
        {
            return new ValidationResult($"Unknown property: {_startPropertyName}");
        }

        var startTime = startProperty.GetValue(validationContext.ObjectInstance) as TimeOnly?;

        if (endTime <= startTime)
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success!;
    }
}
