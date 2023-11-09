using DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelBase.Commands.AsyncCommands;
using ViewModelBase.Commands.QuickCommands;

namespace ViewModel.ViewModels;

public class EditingViewModel : ViewModelBase.ViewModelBase
{
    private readonly DataManager data = Helper.DataModel;
    private Presentation OuterContext { get; init; }

    public EditingViewModel(Presentation outerContext)
    {
        OuterContext = outerContext;
    }

    public void OnWindowClosing(object sender, CancelEventArgs e)
    {
        // Handle closing logic, set e.Cancel as needed
        Helper.ErrorHandler?.ErrorHandle(new Exception(message: "Ещё раз нажмёшь эту кнопку я тебе палец откушу"));
    }
}
