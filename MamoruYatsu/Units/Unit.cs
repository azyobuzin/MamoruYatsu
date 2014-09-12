using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MamoruYatsu.Units
{
    abstract class Unit
    {
        public Unit(FrameworkElement ui)
        {
            this.UI = ui;
        }

        public FrameworkElement UI { get; protected set; }

        public Point GetPoint()
        {
            return this.UI.TranslatePoint(new Point(), null);
        }
    }
}
