using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MamoruYatsu.Units
{
    class Bomb : Enemy
    {
        public Bomb(Field field) : base(field, new UnitViews.Bomb()) { }

        public override int Power
        {
            get
            {
                return 10;
            }
        }
    }
}
