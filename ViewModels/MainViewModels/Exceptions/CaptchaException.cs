using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Exceptions;

public class CaptchaException : Exception
{
    public CaptchaException(string? message = null) : base(message) { }
}
