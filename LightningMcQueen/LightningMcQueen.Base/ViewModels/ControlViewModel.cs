using Model;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Reactive;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Threading;
namespace LightningMcQueen.ViewModels;

internal class ControlViewModel : ReactiveObject
{
    private Timer _dailySaveTimer;
    private ObservableCollection<State> _states=new ObservableCollection<State>();
    public ObservableCollection<State> States
    {
        get => _states;
        set
        {
            _states = value;
            OnPropertyChanged();
        }
    }
    private ReactiveCommand<Unit, Unit> _addCommand;
    public ReactiveCommand<Unit, Unit> AddCommand => _addCommand ??= ReactiveCommand.Create(() => AddNewState());
    public event PropertyChangedEventHandler PropertyChanged;
    public ReactiveCommand<Unit, Unit> _saveCommand;
    public ReactiveCommand<Unit, Unit> SaveCommand => _saveCommand ??= ReactiveCommand.Create(() => SaveToFile());
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    private void AddNewState()
    {
        
        States.Add(new State { Id = 1, dateOnly = DateOnly.FromDateTime(DateTime.Now), Capital = "Moscow" });
    }
    private void SaveToFile()
    {
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\States";
        string fileName = $"states_{DateTime.Now:yyyy-MM-dd}.txt";
        string filePath = Path.Combine(folderPath, fileName);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        var statesAsStrings = States.Select(state => $"{state.Id};{state.dateOnly};{state.Capital}");
        var statesText = string.Join(Environment.NewLine, statesAsStrings);
        File.WriteAllText(filePath, statesText);
        }
    private void StartDailySaveTimer()
    {
        TimeSpan saveInterval = TimeSpan.FromDays(1);
        TimeSpan dueTime = TimeSpan.FromSeconds(10); // Запустить первое сохранение через 10 секунд
        _dailySaveTimer = new Timer(state => SaveToFile(), null, dueTime, saveInterval);
    }
    private void ClearData()
    {
        States.Clear();
    }
    public ControlViewModel()
    {
        StartDailySaveTimer();
    }
}
