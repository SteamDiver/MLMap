using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VisualEntries
{
    /// <summary>
    /// Логика взаимодействия для HouseEntry.xaml
    /// </summary>
    public partial class HouseEntry : UserControl
    {
        private Cursor _cursor;

        public HouseEntry()
        {
            InitializeComponent();
        }

        void onDragDelta(object sender, DragDeltaEventArgs e)
        {
            var thumb = sender as Thumb;
            double yadjust = UserControl.Height + e.VerticalChange;
            double xadjust = UserControl.Width + e.HorizontalChange;
            if ((xadjust >= 0) && (yadjust >= 0))
            {
                UserControl.Width = xadjust;
                UserControl.Height = yadjust;
                Canvas.SetLeft(thumb, Canvas.GetLeft(myThumb) +
                                        e.HorizontalChange);
                Canvas.SetTop(thumb, Canvas.GetTop(myThumb) +
                                       e.VerticalChange);
            }
        }

        void onDragStarted(object sender, DragStartedEventArgs e)
        {
            myThumb.Background = Brushes.Orange;
        }

        void onDragCompleted(object sender, DragCompletedEventArgs e)
        {
            myThumb.Background = Brushes.Blue;
        }
    }
}
