using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MamoruYatsu.Units
{
    abstract class WithHitPoints : Unit
    {
        public abstract int Maximum { get; }
        public int HitPoints { get; protected set; }

        public void MakeDamage(int power)
        {
            this.HitPoints -= power;
        }
    }
}
