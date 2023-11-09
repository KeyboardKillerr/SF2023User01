using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase;
using ViewModelBase.Commands.ErrorHandlers;
using DataModel;
using Microsoft.Identity.Client;

namespace ViewModel;

public static class Helper
{
    internal static DataManager DataModel = DataManager.Get(DataProvidersList.SqlServer);

    public static IErrorHandler? ErrorHandler { internal get; set; }
    public static Action<object?>? LoginToMain { internal get; set; }
    public static Action<object?>? MainToLogin { internal get; set; }
    public static Action<object?>? MainToEdit { internal get; set; }
}
