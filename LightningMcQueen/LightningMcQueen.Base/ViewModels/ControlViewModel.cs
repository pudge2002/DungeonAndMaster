using Model;
using Newtonsoft.Json;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
namespace LightningMcQueen.ViewModels;

internal class ControlViewModel : ReactiveObject
{
    private IDisposable _stateTimer;

    private Random _random = new Random();
    public ObservableCollection<State> States { get; set; } = new ObservableCollection<State>();
    private void AddNewState()
    {
        string capital = "OK";
        if (_random.NextDouble() < 0.1)
        {
            capital = "Error";
        }

        States.Add(new State { Id = States.Count + 1, dateTime = DateTime.Now, Capital = capital });

        if (DateTime.Now.TimeOfDay.Ticks % TimeSpan.TicksPerDay >= (new TimeSpan(0,0,0)).Ticks 
            && DateTime.Now.TimeOfDay.Ticks % TimeSpan.TicksPerDay < (new TimeSpan(0, 0, 1)).Ticks
            )
        {
            SaveToFileWithClear();
        }

    }
    private void StartStateTimer()
    {
        TimeSpan interval = TimeSpan.FromSeconds(1);

        _stateTimer = Observable.Timer(interval, interval)
            .SubscribeOn(RxApp.TaskpoolScheduler) // Запустить таймер в пуле потоков
            .ObserveOn(RxApp.MainThreadScheduler) // Переключиться на главный поток для обновления UI
            .Subscribe(_ =>
            {
                AddNewState();

            });

    }
    private void SaveToFile()
    {
        // Get the path of the executable file
        string exePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        string folderPath = exePath + "\\States";
        string fileName = $"states_{DateTime.Now:dd-MM-yyyy_HH-mm}.json";
        string filePath = Path.Combine(folderPath, fileName);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Convert the States list to JSON
        string statesJson = JsonConvert.SerializeObject(States, Newtonsoft.Json.Formatting.Indented);

        // Save the JSON string to the file
        File.WriteAllText(filePath, statesJson);
    }

    private void SaveToFileWithClear()
    {
        try
        {
            SaveToFile();
            ClearData();
        }
        catch (IOException ex)
        {
            // Выводим сообщение об ошибке, если не удалось сохранить файл
            MessageBox.Show($"Не удалось сохранить файл.\n\n{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    private void ClearData()
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            States.Clear();
        });
    }

    private void CreateDirectory()
    {
        string exePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        string folderPath = exePath + "\\States";

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
    }

    public ControlViewModel()
    {
        CreateDirectory();
        StartStateTimer();
    }
}
