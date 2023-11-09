using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ViewModel;
using ViewModel.Exceptions;
using ViewModelBase.Commands.ErrorHandlers;

namespace SF2023User01.WpfApp;

internal class ErrorHandler : IErrorHandler
{
    public void ErrorHandle(Exception e)
    {
        if (e is BadAttemptException)
        {
            MessageBox.Show("Логин или пароль введены неправильно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        if (e is CaptchaException)
        {
            MessageBox.Show("Капча введена неправильно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        MessageBox.Show(e.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}
