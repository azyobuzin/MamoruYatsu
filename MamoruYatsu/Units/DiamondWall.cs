using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MamoruYatsu.Units
{
    class DiamondWall : Wall
    {
        public DiamondWall(Field field) : base(field, new UnitViews.DiamondWall()) { }

        public override int Maximum
        {
            get
            {
                return 120;
            }
        }
    }
}
