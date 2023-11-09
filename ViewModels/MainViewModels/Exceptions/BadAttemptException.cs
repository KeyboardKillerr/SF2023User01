using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Exceptions;

public class BadAttemptException : Exception
{
    public BadAttemptException(string? message = null) : base(message) { }
}
