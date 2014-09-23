using System;
using System.Windows;

namespace MamoruYatsu.Units
{
    abstract class Wall : WithHitPoints
    {
        public Wall(Field field, FrameworkElement ui)
            : base(ui)
        {
            this.field = field;
        }

        private Field field;

        protected override void HpZero()
        {
            var index = Array.IndexOf(this.field.Walls, this);
            this.field.SetWall(null, index);

            DispatcherAsync.Delay(TimeSpan.FromSeconds(0.5), () =>
            {
                var x = index / 3 * 3;
                var y = index % 3;

                for (var i = y; i < 2; i++)
                {
                    var w = this.field.Walls[x + i + 1];
                    this.field.SetWall(null, x + i + 1);
                    this.field.SetWall(w, x + i);
                }
                this.field.SetWall(null, x + 2);
            });
        }

        public void Cure()
        {
            this.HitPoints = this.Maximum;
        }
    }
}
