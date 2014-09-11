using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MamoruYatsu.Units
{
    class Castle : WithHitPoints
    {
        public Castle()
        {
            this.UI = new UnitViews.Castle();
            this.UI.SetValue(Canvas.LeftProperty, 0.0);
            this.UI.SetValue(Canvas.BottomProperty, 0.0);
        }
        
        public override int Maximum
        {
            get
            {
                return 500;
            }
        }
    }
}
