using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace MamoruYatsu.Units
{
    abstract class WithHitPoints : Unit
    {
        public WithHitPoints(FrameworkElement ui)
            : base(ui)
        {
            this.p = new ProgressBar()
            {
                Maximum = this.Maximum,
                Width = 100,
                Height = 8
            };
            this.HitPoints = this.Maximum;
            var tp = new ToolTip();
            tp.Content = this.p;
            ui.ToolTip = tp;
        }

        private ProgressBar p;

        public abstract int Maximum { get; }

        private int hitPoints;
        public int HitPoints
        {
            get
            {
                return this.hitPoints;
            }
            protected set
            {
                this.hitPoints = value;
                this.p.Value = value;
            }
        }

        public void MakeDamage(int power)
        {
            this.HitPoints -= power;
            if (this.HitPoints <= 0)
                this.HpZero();
        }

        protected abstract void HpZero();
    }
}
