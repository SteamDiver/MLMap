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

namespace VisualEntries
{
    /// <summary>
    /// Логика взаимодействия для LayerEntry.xaml
    /// </summary>
    public partial class LayerEntry : UserControl
    {
        public string Caption { get; set; }
        public bool Visible { get; set; }
        public LayerEntry()
        {
            InitializeComponent();
        }
    }
}
