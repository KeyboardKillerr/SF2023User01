using DataModel;
using DataModel.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ViewModel.ViewModels;
using ViewModelBase.Commands.AsyncCommands;
using ViewModelBase.Commands.QuickCommands;

namespace ViewModel;

public class Presentation : ViewModelBase.ViewModelBase
{
    public LoginViewModel LoginVM { get; init; }
    public MainViewModel MainVM { get; init; }
    public EditingViewModel EditVM { get; init; }

    public Presentation()
    {
        LoginVM = new(this);
        MainVM = new(this);
        EditVM = new(this);
    }
}
