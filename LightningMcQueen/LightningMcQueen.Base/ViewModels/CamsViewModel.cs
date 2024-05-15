using Prism.Commands;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LightningMcQueen.ViewModels;

internal class CamsViewModel : ReactiveObject
{

    [Reactive] public string VideoPath { get; set; } = "..\\..\\..\\..\\..\\Video\\Aglomachine.mkv";

}
