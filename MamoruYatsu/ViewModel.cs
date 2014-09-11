using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MamoruYatsu
{
    class ViewModel
    {
        public ViewModel()
        {
            this.Field = new Field();
        }

        public Field Field { get; private set; }
    }
}
