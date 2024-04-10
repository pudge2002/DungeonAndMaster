using Prism.Mvvm;
using Alturos.Yolo;
using System.Windows.Documents;
using Alturos.Yolo.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightningMcQueen.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public string ImageSource { get; set; }

        public MainWindowViewModel()
        {
            string path = "C:\\Users\\MAXIM\\source\\repos\\DungeonAndMaster\\LightningMcQueen\\";
            ImageSource = path + "Images\\Bird1.png";

            string path2 = "C:\\Users\\MAXIM\\source\\repos\\DungeonAndMaster\\LightningMcQueen\\Alturos.YoloV2TinyVocData\\data\\";
            YoloWrapper yoloWrapper = new YoloWrapper(path2 + "yolov2-tiny-voc.cfg", path2 + "yolov2-tiny-voc.weights", path2 + "voc.names");

            List<YoloItem> items;
            items = yoloWrapper.Detect(path + "Images\\Bird1.png").ToList();

        }


    }
}
