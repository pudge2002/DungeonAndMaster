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
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;


using System.Windows;


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

        public BitmapImage ImageSource { get; set; }

        //public Image image;
        public MainWindowViewModel()
        {
            string path = "C:\\Users\\MAXIM\\source\\repos\\DungeonAndMaster\\LightningMcQueen\\";
            ImageSource = new BitmapImage(new Uri(path + "Images\\Bird1.png"));

            string path2 = "C:\\Users\\MAXIM\\source\\repos\\DungeonAndMaster\\LightningMcQueen\\Alturos.YoloV2TinyVocData\\data\\";
            YoloWrapper yoloWrapper = new YoloWrapper(path2 + "yolov2-tiny-voc.cfg", path2 + "yolov2-tiny-voc.weights", path2 + "voc.names");

            List<YoloItem> items;
            items = yoloWrapper.Detect(path + "Images\\Bird1.png").ToList();

            //DrawBoundingBoxes(items);
        }

        private void DrawBoundingBoxes(List<YoloItem> items, YoloItem selectedItem = null)
        {
            //var imageInfo = this.GetCurrentImage();
            ////Load the image(probably from your stream)
            //image = Image.FromFile(imageInfo.Path);

            Image image = null;

            using (var canvas = Graphics.FromImage(image))
            {
                foreach (var item in items)
                {
                    var x = item.X;
                    var y = item.Y;
                    var width = item.Width;
                    var height = item.Height;

                    var brush = this.GetBrush(item.Confidence);
                    var penSize = image.Width / 100.0f;

                    using (var pen = new Pen(brush, penSize))
                    using (var overlayBrush = new SolidBrush(Color.FromArgb(150, 255, 255, 102)))
                    {
                        if (item.Equals(selectedItem))
                        {
                            canvas.FillRectangle(overlayBrush, x, y, width, height);
                        }

                        canvas.DrawRectangle(pen, x, y, width, height);
                    }
                }

                canvas.Flush();
            }

            //var oldImage = this.pictureBox1.Image;
            //this.pictureBox1.Image = image;
            //oldImage?.Dispose();
        }

        private Brush GetBrush(double confidence)
        {
            if (confidence > 0.5)
            {
                return Brushes.GreenYellow;
            }
            else if (confidence > 0.2 && confidence <= 0.5)
            {
                return Brushes.Orange;
            }

            return Brushes.DarkRed;
        }
    }
}
