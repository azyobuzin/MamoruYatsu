using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MamoruYatsu.Units
{
    abstract class Wall : WithHitPoints
    {
        protected override void HpZero()
        {
            throw new NotImplementedException();
        }
    }
}
