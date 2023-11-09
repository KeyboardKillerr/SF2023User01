using DataModel.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.IO;

namespace SF2023User01.WpfApp.Converters;

internal class ImagePathConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        //pack://application:,,,/Resources/Images/Products/E345R4.jpg
        string workingDirectory = Environment.CurrentDirectory;
        string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
        projectDirectory += "\\Resources\\Images\\Products\\";
        if (value is null) return projectDirectory + "picture.png";
        if (value is string path)
        {
            if (File.Exists(projectDirectory + path)) return projectDirectory + path;
            else return projectDirectory + "picture.png";
        }
        return Binding.DoNothing;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
