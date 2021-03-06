﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using VisualEntries;

namespace MLMap
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AddLayerBtnClick(null, null);
        }

        private void AddLayerBtnClick(object sender, RoutedEventArgs e)
        {
            var layer = new LayerEntry($"Layer {LayersLb.Items.Count}");
            LayersLb.Items.Add(layer);
        }

        private void AddElement(UserControl element)
        {
            var layer = LayersLb.SelectedItem as LayerEntry;
            if (LayersLb.Items.Count == 1)
            {
                layer = LayersLb.Items[0] as LayerEntry;
            }

            if (layer != null)
            {
                layer.Children.Add(element);
                ContentCanvas.Children.Add(element);
            }
            else
            {
                MessageBox.Show("Необходимо выбрат слой");
            }
        }

        private void AddRoad(object sender, RoutedEventArgs e)
        {
            var road = new RoadEntry();
            AddElement(road);
        }

        private void AddHouse(object sender, RoutedEventArgs e)
        {
            var house = new HouseEntry();
            AddElement(house);
        }

        private void AddRiver(object sender, RoutedEventArgs e)
        {
            var river = new RiverEntry();
            AddElement(river);
        }

        private void AddPark(object sender, RoutedEventArgs e)
        {
            var park = new ParkEntry();
            AddElement(park);
        }

        private void DeleteLayerBtnClick(object sender, RoutedEventArgs e)
        {
            foreach (var control in ((LayerEntry)LayersLb.SelectedItem).Children)
            {
                var parent = VisualTreeHelper.GetParent(control) as Canvas;
                parent?.Children.Remove(control);
                LayersLb.Items.Remove(LayersLb.SelectedItem);
            }
            
        }

        private void SaveFile(object sender, RoutedEventArgs e)
        {
            var items = LayersLb.Items.Cast<LayerEntry>().ToList();
            var sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == true)
            {
                var file = sfd.FileName;
                var content = "";
                foreach (var entry in items)
                {
                    content += $"~{entry.LayerName}~\r\n";
                    foreach (var control in entry.Children)
                    {
                        content += $"{XamlWriter.Save(control)}\r\n";
                    }
                }
                File.WriteAllText(file, content);
            }
        }

        private void OpenClick(object sender, RoutedEventArgs e)
        {
            LayersLb.Items.Clear();
            ContentCanvas.Children.Clear();

            var ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                var file = File.ReadAllLines(ofd.FileName);
                LayerEntry layerEntry = null;
                foreach (var line in file)
                {
                    if (line.Contains("~"))
                    {
                        layerEntry = new LayerEntry(line.Replace("~", ""));
                        LayersLb.Items.Add(layerEntry);
                    }
                    else
                    {
                        
                        var element = XamlReader.Parse(line) as UserControl;
                        if (element != null)
                        {
                            layerEntry?.Children.Add(element);
                            ContentCanvas.Children.Add(element);
                        }
                    }
                }
            }
        }

        private void ExportPicture(object sender, RoutedEventArgs e)
        {
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)ContentCanvas.ActualWidth,
                (int)ContentCanvas.ActualHeight, 72, 72, PixelFormats.Pbgra32);
            bmp.Render(ContentCanvas);
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));

            var sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == true)
            {
                using (var fs = File.OpenWrite(sfd.FileName))
                {
                    encoder.Save(fs);
                }
            } 
        }
    }
}
