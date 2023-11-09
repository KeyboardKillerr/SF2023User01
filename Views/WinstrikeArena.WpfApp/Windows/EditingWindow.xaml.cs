using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace SF2023User01.WpfApp.Windows;

/// <summary>
/// Логика взаимодействия для EditingWindow.xaml
/// </summary>
public partial class EditingWindow : Window
{
    private static EditingWindow Instance;
    private EditingWindow()
    {
        Closing += App.ViewModel.EditVM.OnWindowClosing;
        Closing += OnWindowClosing;
        DataContext = App.ViewModel;
        InitializeComponent();
    }
    public static EditingWindow Get()
    {
        Instance ??= new();
        return Instance;
    }

    public static void OnWindowClosing(object sender, CancelEventArgs e) => Instance = null;
}
