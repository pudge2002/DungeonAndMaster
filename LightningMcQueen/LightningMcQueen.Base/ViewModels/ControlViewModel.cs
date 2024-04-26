using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightningMcQueen.ViewModels;

internal class ControlViewModel : ReactiveObject
{
    [Reactive] public string State { get; set; } = "gdidghodgio";
}
