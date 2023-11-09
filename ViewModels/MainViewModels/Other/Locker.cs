using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ViewModel.Other;

internal class Locker
{
    public event EventHandler<TickEventArgs>? Tick;
    public event EventHandler? TimerFinished;
    public int SecondsLeft { get; private set; }
    public int SecondsToWait { get; private init; }
    private CancellationTokenSource? TokenSource { get; set; }
    public Locker(int seconds)
    {
        if (seconds < 0) throw new ArgumentException("Negative seconds", nameof(seconds));
        SecondsLeft = 0;
        SecondsToWait = seconds;
    }
    public void Cancel()
    {
        if (TokenSource is null) return;
        TokenSource.Cancel();
    }
    public void Lock()
    {
        TokenSource = new();
        LockPrivate(TokenSource.Token);
    }
    private void Finish()
    {
        SecondsLeft = 0;
        TimerFinishedNotify();
    }
    private async void LockPrivate(CancellationToken cancellationToken)
    {
        for (int i = SecondsToWait; i > 0; i--)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                Finish();
                return;
            }
            SecondsLeft = i;
            TickNotify();
            await Task.Delay(1000, CancellationToken.None);
        }
        Finish();
        TimerFinishedNotify();
    }
    private void TickNotify() => Tick?.Invoke(this, new TickEventArgs(SecondsLeft));
    private void TimerFinishedNotify() => TimerFinished?.Invoke(this, EventArgs.Empty);
}

internal class TickEventArgs : EventArgs
{
    private readonly int _secondsLeft;
    public int SecondsLeft
    {
        get { return _secondsLeft; }
    }
    public TickEventArgs(int secondsLeft)
    {
        _secondsLeft = secondsLeft;
    }
}