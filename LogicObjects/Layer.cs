using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicObjects
{
    public class Layer<T> where T:LayerObject
    {
        private List<T> LayerObjects { get; set; }
    }
}
