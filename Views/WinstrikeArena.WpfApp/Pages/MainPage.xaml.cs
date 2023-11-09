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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SF2023User01.WpfApp.Pages;

/// <summary>
/// Логика взаимодействия для GamesPage.xaml
/// </summary>
public partial class MainPage : Page
{
    private Presentation Model { get; init; }
    public MainPage()
    {
        InitializeComponent();
        DataContext = App.ViewModel;
        if (DataContext is Presentation viewmodel) Model = viewmodel;
    }
}
