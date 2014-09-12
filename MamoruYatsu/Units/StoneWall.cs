using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MamoruYatsu.Units
{
    class StoneWall : Wall
    {
        public StoneWall(Field field) : base(field, new UnitViews.StoneWall()) { }

        public override int Maximum
        {
            get
            {
                return 60;
            }
        }
    }
}
