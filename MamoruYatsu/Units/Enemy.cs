using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace MamoruYatsu.Units
{
    abstract class Enemy : Unit
    {
        public Enemy(Field field, FrameworkElement ui)
            : base(ui)
        {
            this.field = field;

            this.right = -ui.ActualWidth;
            this.bottom = (double)App.Random.Next(100);
            ui.SetValue(Canvas.RightProperty, this.right);
            ui.SetValue(Canvas.BottomProperty, this.bottom);
        }

        public abstract int Power { get; }

        public event EventHandler Disappeared;
        public event EventHandler UIChanged;

        private Field field;
        private DispatcherTimer timer;
        private double right;
        private double bottom;

        public void Start()
        {
            var upSpeed = App.Random.Next(25000, 60000) / 10000.0;
            this.timer = new DispatcherTimer(DispatcherPriority.Render, App.Current.Dispatcher);
            this.timer.Interval = TimeSpan.FromTicks(10000000 / 60); // 60 loop per sec
            this.timer.Tick += (_, __) =>
            {
                this.right += 10;
                this.UI.SetValue(Canvas.RightProperty, this.right);

                this.bottom += upSpeed;
                this.UI.SetValue(Canvas.BottomProperty, this.bottom);
                upSpeed -= 0.1;

                var myPoint = this.GetPoint();
                var myRect = new Rect(myPoint.X, myPoint.Y, this.UI.ActualWidth, this.UI.ActualHeight);
                var match = this.field.Walls.Cast<WithHitPoints>()
                    .Concat(new WithHitPoints[] { this.field.Castle })
                    .Where(x => x != null)
                    .FirstOrDefault(x =>
                    {
                        var point = x.GetPoint();
                        return myRect.IntersectsWith(new Rect(point.X, point.Y, x.UI.ActualWidth, x.UI.ActualHeight));
                    });
                if (match != null)
                {
                    this.timer.Stop();
                    match.MakeDamage(this.Power);
                    this.UI = new UnitViews.Explosion();
                    this.UI.SetValue(Canvas.RightProperty, this.right);
                    this.UI.SetValue(Canvas.BottomProperty, this.bottom);
                    if (this.UIChanged != null)
                        this.UIChanged(this, EventArgs.Empty);
                    DispatcherAsync.Delay(TimeSpan.FromSeconds(0.5), () =>
                    {
                        if (this.Disappeared != null)
                            this.Disappeared(this, EventArgs.Empty);
                    });
                }
                else if (this.right > (this.UI.Parent as FrameworkElement).ActualWidth)
                {
                    this.timer.Stop();
                    if (this.Disappeared != null)
                        this.Disappeared(this, EventArgs.Empty);
                }
            };
            this.timer.Start();
        }

        public void Stop()
        {
            this.timer.Stop();
            if (this.Disappeared != null)
                this.Disappeared(this, EventArgs.Empty);
        }
    }
}
