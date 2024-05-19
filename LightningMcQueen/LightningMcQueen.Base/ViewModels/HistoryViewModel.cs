using DynamicData.Binding;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Model;
using Newtonsoft.Json;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive;
using System.Reflection;
using System.Text;


namespace LightningMcQueen.ViewModels
{
    internal class HistoryViewModel : ReactiveObject
    {
        public ObservableCollection<State> ShowableStates { get; set; }=new ObservableCollection<State>();
        public ReactiveCommand<Unit, Unit> OpenFileCommand { get; private set; }
        public HistoryViewModel()
        {
            OpenFileCommand = ReactiveCommand.Create(() => OpenFile());
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

                // Read the contents of the file
                string json =File.ReadAllText(filePath);


                // Deserialize the JSON string into a list of states
                ShowableStates = JsonConvert.DeserializeObject<ObservableCollection<State>>(json);

            }
        }
    }
}
