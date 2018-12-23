using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    [Serializable]
    public partial class LayerEntry : UserControl
    {
        private bool _visible = true;
        public string LayerName { get; set; }
        public List<UserControl> Children { get; set; } = new List<UserControl>();

        public bool Visible
        {
            get => _visible;
            set
            {
                _visible = value;
                foreach (var control in Children)
                {
                    control.Visibility = value ? Visibility.Visible : Visibility.Hidden;
                }
            }
        }

        public LayerEntry(string name)
        {
            InitializeComponent();
            LayerName = name;
            DataContext = this;
        }

        public LayerEntry()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
