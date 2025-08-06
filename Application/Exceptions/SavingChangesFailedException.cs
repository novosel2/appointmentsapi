using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions;

public class SavingChangesFailedException : Exception
{
    public SavingChangesFailedException()
    {
    }

    public SavingChangesFailedException(string? message) : base(message)
    {
    }

    public SavingChangesFailedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
