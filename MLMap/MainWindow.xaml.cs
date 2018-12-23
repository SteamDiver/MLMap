using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
        }

        private void AddLayerBtnClick(object sender, RoutedEventArgs e)
        {
            var layer = new LayerEntry($"Layer {LayersLb.Items.Count}");
            LayersLb.Items.Add(layer);
        }

        private void AddElement(UserControl element)
        {
            ContentCanvas.Children.Add(element);
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
    }
}
