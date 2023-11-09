using ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.IO;
using System.Windows.Media.Animation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using CaptchaGen.SkiaSharp;
using SkiaSharp;

namespace SF2023User01.WpfApp.Pages;

/// <summary>
/// Логика взаимодействия для LoginPage.xaml
/// </summary>
public partial class LoginPage : Page
{
    private Presentation Model { get; init; }
    public LoginPage()
    {
        InitializeComponent();
        DataContext = App.ViewModel;
        if (DataContext is Presentation viewmodel) Model = viewmodel;
        Model.LoginVM.Captcha.PropertyChanged += DisplayCaptcha;
        Model.LoginVM.Captcha.Update();
    }

    private void DisplayCaptcha(object sender, EventArgs e)
    {
        var imageSource = new BitmapImage();
        imageSource.BeginInit();
        imageSource.StreamSource = new CaptchaGenerator().GenerateImageAsStream(Model.LoginVM.Captcha.Text);
        imageSource.EndInit();
        CaptchaImage.Source = imageSource;
    }

    private void PassBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        Model.LoginVM.Password = PassBox.Password;
    }
}
