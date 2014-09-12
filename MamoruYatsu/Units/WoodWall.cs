﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;

namespace MamoruYatsu.Units
{
    class WoodWall : Wall
    {
        public WoodWall(Field field) : base(field, new UnitViews.WoodWall()) { }

        public override int Maximum
        {
            get
            {
                return 30;
            }
        }
    }
}
