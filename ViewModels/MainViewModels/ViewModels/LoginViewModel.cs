using DataModel;
using DataModel.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ViewModel.Exceptions;
using ViewModel.Other;
using ViewModelBase.Commands.AsyncCommands;
using ViewModelBase.Commands.QuickCommands;

namespace ViewModel.ViewModels;

public class LoginViewModel : ViewModelBase.ViewModelBase
{
    private readonly DataManager data = Helper.DataModel;
    private Presentation OuterContext { get; init; }

    public Command LoginCommand { get; private init; }
    public Command UpdateCaptchaCommand { get; private init; }
    public Command GuestLoginCommand { get; private init; }

    public Captcha Captcha { get; private init; }
    private Locker LoginLocker { get; init; }
    private Counter Attempts { get; init; }

    internal LoginViewModel(Presentation outerContext)
    {
        OuterContext = outerContext;
        Attempts = new(2);
        Captcha = new();

        LoginCommand = new(LoginFunction, LoginCanExecute);
        UpdateCaptchaCommand = new(Captcha.Update, CanExecute);
        GuestLoginCommand = new(GuestLoginFunction, CanExecute);

        LoginLocker = new(10);
        LoginLocker.Tick += HandleTick;
        LoginLocker.TimerFinished += HandleTimerFinish;
    }

    #region Captcha

    private string _enteredCaptcha = "";
    public string EnteredCaptcha
    {
        get { return _enteredCaptcha; }
        set
        {
            value = value.Trim();
            Set(ref _enteredCaptcha, value);
        }
    }

    private bool _captchaIsVisible = false;
    public bool CaptchaIsVisible
    {
        get { return _captchaIsVisible; }
        set
        {
            Set(ref _captchaIsVisible, value);
        }
    }

    private void HandleBadAttempt()
    {
        Captcha.Update();
        Attempts.Increase();

        if (Attempts.CurrentValue == 1)
        {
            CaptchaIsVisible = true;
            Helper.ErrorHandler?.ErrorHandle(new BadAttemptException());
        }
        else if (Attempts.CurrentValue == 2) LoginLocker.Lock();
    }
    #endregion Captcha

    #region TimerHandler
    private void HandleTick(object? sender, TickEventArgs e)
    {
        LoginButtonText = $"Заблокировано на {e.SecondsLeft} секунд";
    }

    private void HandleTimerFinish(object? sender, EventArgs e)
    {
        LoginButtonText = "Войти";
        LoginCommand.RaiseCanExecuteChanged();
    }
    #endregion

    #region CommandFunctions
    private void GuestLoginFunction()
    {
        CurrentUser = Guest;
        Helper.LoginToMain?.Invoke(null);
        ResetAll();
    }

    private void LoginFunction()
    {
        if (Attempts.CurrentValue != 0 && !Captcha.Verify(_enteredCaptcha))
        {
            HandleBadAttempt();
            return;
        }

        var user = data.Users.Items.FirstOrDefault(u => u.Login == _login);
        if (user is null)
        {
            HandleBadAttempt();
            return;
        }
        if (User.GetHashString(_password) != user.Password)
        {
            HandleBadAttempt();
            return;
        }

        CurrentUser = user;
        Helper.LoginToMain?.Invoke(null);
        ResetAll();
    }
    #endregion

    #region CanExecute
    private bool LoginCanExecute()
    {
        if (_login.IsNullOrEmpty()) return false;
        //if (_password.IsNullOrEmpty()) return false;
        if (LoginLocker.SecondsLeft != 0) return false;
        else return true;
    }
    private static bool CanExecute()
    {
        return true;
    }
    #endregion

    #region Fields
    private string _login = "";
    public string Login
    {
        get { return _login; }
        set
        {
            value = value.Trim();
            if (Set(ref _login, value))
            {
                LoginCommand.RaiseCanExecuteChanged();
            }
        }
    }

    private string _password = "";
    public string Password
    {
        get { return _password; }
        set
        {
            if (Set(ref _password, value))
            {
                LoginCommand.RaiseCanExecuteChanged();
            }
        }
    }
    #endregion

    private void ResetAll()
    {
        Login = "";
        Password = "";
        EnteredCaptcha = "";
        LoginLocker.Cancel();
        Captcha.Update();
        Attempts.Reset();
        CaptchaIsVisible = false;
    }

    private static readonly User Guest = new()
    {
        FirstName = "",
        MiddleName = "",
        LastName = "",
        Login = "",
        Password = "",
        Role = "Guest"
    };

    private User _currentUser = Guest;
    public User? CurrentUser
    {
        get { return _currentUser; }
        set
        {
            value ??= Guest;
            if (Set(ref _currentUser, value)) OuterContext.MainVM.UpdateCurrentUser();
        }
    }

    private string _loginButtonText = "Войти";
    public string LoginButtonText
    {
        get { return _loginButtonText; }
        set { Set(ref _loginButtonText, value); }
    }
}
