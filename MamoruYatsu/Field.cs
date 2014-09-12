using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Collections.ObjectModel;
using MamoruYatsu.Units;
using System.Windows.Controls;
using System.Windows.Threading;
using MamoruYatsu.Stages;

namespace MamoruYatsu
{
    class Field
    {
        public Field()
        {
            this.Walls = new Wall[9];
            for (var i = 0; i < 9; i++)
                this.SetWall(new WoodWall(), i);

            this.Castle = new Castle();
        }

        public Wall[] Walls { get; private set; }
        public event EventHandler<EventArgs<int>> WallsChanging;
        public event EventHandler<EventArgs<int>> WallsChanged;

        public void SetWall(Wall w, int index)
        {
            if (this.WallsChanging != null)
                this.WallsChanging(this, GenericEventArgs.Create(index));

            this.Walls[index] = w;
            w.UI.SetValue(Canvas.LeftProperty,
                index >= 6 ? 390.0
                : index >= 3 ? 320.0
                : 250.0
            );

            double bottom;
            switch (index)
            {
                case 0:
                case 3:
                case 6:
                    bottom = 0.0;
                    break;
                case 1:
                case 4:
                case 7:
                    bottom = 100.0;
                    break;
                default:
                    bottom = 200.0;
                    break;
            }
            w.UI.SetValue(Canvas.BottomProperty, bottom);

            if (this.WallsChanged != null)
                this.WallsChanged(this, GenericEventArgs.Create(index));
        }

        public Castle Castle { get; set; }

        public event EventHandler<EventArgs<Enemy>> CreatedEnemy;

        public event EventHandler StartedGame;
        public event EventHandler EndedGame;

        public void StartGame(IStage stage)
        {
            if (this.StartedGame != null)
                this.StartedGame(this, EventArgs.Empty);

            const double interval = 0.2;
            const double totalCount = 20 / interval;
            var count = 0;
            var timer = new DispatcherTimer(DispatcherPriority.Normal, App.Current.Dispatcher);
            timer.Interval = TimeSpan.FromSeconds(interval);
            timer.Tick += (_, __) =>
            {
                if (count++ >= totalCount)
                {
                    timer.Stop();
                    if (this.EndedGame != null)
                        this.EndedGame(this, EventArgs.Empty);
                    return;
                }

                if (count < totalCount * 0.8 && stage.NewEnemy())
                {
                    if (this.CreatedEnemy != null)
                        this.CreatedEnemy(this, GenericEventArgs.Create<Enemy>(new Bomb(this)));
                }
            };
            timer.Start();
        }

        public int Cleared { get; private set; }
    }
}
