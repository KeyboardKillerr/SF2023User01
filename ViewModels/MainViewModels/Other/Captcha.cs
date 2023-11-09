using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Other;

public class Captcha : ViewModelBase.ViewModelBase
{
    public Captcha() => Generate();
    private void Generate()
    {
        var captchaText = "";
        var random = new Random();
        for (var i = 0; i < 4; i++)
            captchaText += (char)random.Next('a', 'z' + 1);
        Text = captchaText;
    }

    private string? _text;
    public string? Text
    {
        get { return _text; }
        set { Set(ref _text, value); }
    }

    public void Update() => Generate();

    public bool Verify(string input)
    {
        var text = _text;
        Generate();
        if (input == text) return true;
        return false;
    }
}
