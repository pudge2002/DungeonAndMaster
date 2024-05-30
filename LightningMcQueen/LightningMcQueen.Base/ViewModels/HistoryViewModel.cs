using DynamicData.Binding;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Model;
using Newtonsoft.Json;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using Xceed.Wpf.Toolkit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace LightningMcQueen.ViewModels
{
    internal class HistoryViewModel : ReactiveObject
    {

        public List<DateTime> AvailableTimes { get; set; }
        public ReactiveCommand<Unit, Unit> FilterCommand { get; }
        public ReactiveCommand<Unit, Unit> ClearFilterCommand { get; }
        [Reactive] public DateTime? From {  get; set; }
        [Reactive] public DateTime? To { get; set; }
        [Reactive] public string filename { get; set; }
        public ObservableCollection<State> ShowableStates { get; set; }=new ObservableCollection<State>();
        public List<State> BackStates { get; set; } = new List<State>();
        public ReactiveCommand<Unit, Unit> OpenFileCommand { get; private set; }

        public HistoryViewModel()
        {
            OpenFileCommand = ReactiveCommand.Create(() => OpenFile());
            FilterCommand = ReactiveCommand.Create(Filter);
            ClearFilterCommand= ReactiveCommand.Create(ClearFilter);
        }
        public void OpenFile()
        {
           
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();

            // Set the properties of the OpenFileDialog
            openFileDialog.InitialDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\States"; ;
            openFileDialog.Filter = "JSON files (*.json)|*.json";
            openFileDialog.RestoreDirectory = true;

            // Show the OpenFileDialog and check if the user selected a file
            if (openFileDialog.ShowDialog() == true)
            {
                // Get the path of the selected file
                string filePath = openFileDialog.FileName;
                filename=Path.GetFileName(filePath);
                // Read the contents of the file
                string json =File.ReadAllText(filePath);


                // Deserialize the JSON string into a list of states
                
                ObservableCollection<State> newStates = JsonConvert.DeserializeObject<ObservableCollection<State>>(json);
                ShowableStates.Clear();
                foreach (State state in newStates)
                {
                    ShowableStates.Add(state);
                    BackStates.Add(state);
                }
                AvailableTimes = ShowableStates
                    .Select(s => new DateTime(s.dateTime.Year, s.dateTime.Month, s.dateTime.Day, s.dateTime.Hour, s.dateTime.Minute, 0))
                    .Distinct()
                    .OrderBy(t => t)
                    .ToList();

                // Установка начального времени в TimePicker
                From = AvailableTimes.First();
                To = AvailableTimes.Last();

            }
        }
        private void Filter()
        {
            if (!From.HasValue || !To.HasValue)
            {
                return;
            }

            List<State> filteredStates = BackStates
             .Where(s => s.dateTime.TimeOfDay >= From.Value.TimeOfDay && s.dateTime.TimeOfDay <= To.Value.TimeOfDay)
            .ToList();
            ShowableStates.Clear();
            foreach (State state in filteredStates)
            {
                ShowableStates.Add(state);

            }
        }
        private void ClearFilter()
        {
            
            ShowableStates.Clear();
            foreach (State state in BackStates)
            {
                ShowableStates.Add(state);

            }
        }
    }
}

