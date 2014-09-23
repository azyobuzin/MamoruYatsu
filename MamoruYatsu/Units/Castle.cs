using System;
using System.Windows.Controls;

namespace MamoruYatsu.Units
{
    class Castle : WithHitPoints
    {
        public Castle()
            : base(new UnitViews.Castle())
        {
            this.UI.SetValue(Canvas.LeftProperty, 0.0);
            this.UI.SetValue(Canvas.BottomProperty, 0.0);
        }

        public override int Maximum
        {
            get
            {
                return 80;
            }
        }

        protected override void HpZero()
        {
            if (this.GameOver != null)
                this.GameOver(this, EventArgs.Empty);
        }

        public event EventHandler GameOver;

        public void Cure()
        {
            this.HitPoints = this.Maximum;
        }

        public void Reset()
        {
            this.HitPoints = this.Maximum;
        }
    }
}
