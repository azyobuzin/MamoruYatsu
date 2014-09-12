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
    class Field : NotificationObject
    {
        public Field()
        {
            this.Castle = new Castle();
            this.Initialize();
        }

        private void Initialize()
        {
            this.Walls = new Wall[9];
            this.Cleared = 0;
            this.Money = 200;
            this.Castle.Reset();

            if (this.Initializing != null)
                this.Initializing(this, EventArgs.Empty);
        }

        public event EventHandler Initializing;

        public Wall[] Walls { get; private set; }
        public event EventHandler<EventArgs<int>> WallsChanging;
        public event EventHandler<EventArgs<int>> WallsChanged;

        public void SetWall(Wall w, int index)
        {
            if (this.WallsChanging != null)
                this.WallsChanging(this, GenericEventArgs.Create(index));

            this.Walls[index] = w;

            if (w != null)
            {
                w.UI.SetValue(Canvas.LeftProperty,
                    index >= 6 ? 390.0
                    : index >= 3 ? 320.0
                    : 250.0
                );

                double bottom;
                switch (index % 3)
                {
                    case 0:
                        bottom = 0.0;
                        break;
                    case 1:
                        bottom = 100.0;
                        break;
                    default:
                        bottom = 200.0;
                        break;
                }
                w.UI.SetValue(Canvas.BottomProperty, bottom);
            }

            if (this.WallsChanged != null)
                this.WallsChanged(this, GenericEventArgs.Create(index));
        }

        public Castle Castle { get; set; }

        public event EventHandler<EventArgs<Enemy>> CreatedEnemy;

        public event EventHandler StartedGame;
        public event EventHandler EndedGame;

        private double currentCount;
        public double CurrentCount
        {
            get
            {
                return this.currentCount;
            }
            private set
            {
                if (this.currentCount != value)
                {
                    this.currentCount = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        private List<Enemy> enemies = new List<Enemy>();

        public void StartGame(IStage stage)
        {
            this.CurrentCount = 0;
            this.enemies.Clear();

            if (this.StartedGame != null)
                this.StartedGame(this, EventArgs.Empty);

            const double interval = 0.2;
            const double totalCount = 20 / interval;
            var timer = new DispatcherTimer(DispatcherPriority.Normal, App.Current.Dispatcher);
            timer.Interval = TimeSpan.FromSeconds(interval);

            EventHandler gameOverHandler = null;
            gameOverHandler = (_, __) =>
            {
                timer.Stop();
                foreach (var e in this.enemies) e.Stop();
                this.Castle.GameOver -= gameOverHandler;
                MessageBox.Show("城が白旗上げた。", "ゲームオーバー");
                if (this.EndedGame != null)
                    this.EndedGame(this, EventArgs.Empty);
                this.Initialize();
            };
            this.Castle.GameOver += gameOverHandler;

            timer.Tick += (_, __) =>
            {
                if (this.CurrentCount++ >= totalCount)
                {
                    timer.Stop();
                    this.Castle.GameOver -= gameOverHandler;
                    this.Cleared = Math.Max(this.Cleared, stage.Number);
                    this.Money += stage.Reward;
                    if (this.EndedGame != null)
                        this.EndedGame(this, EventArgs.Empty);
                    return;
                }

                if (this.CurrentCount < totalCount * 0.85 && stage.NewEnemy())
                {
                    var e = new Bomb(this);
                    this.enemies.Add(e);
                    if (this.CreatedEnemy != null)
                        this.CreatedEnemy(this, GenericEventArgs.Create<Enemy>(e));
                }
            };
            timer.Start();
        }

        private int cleared;
        public int Cleared
        {
            get
            {
                return this.cleared;
            }
            private set
            {
                if (this.cleared != value)
                {
                    this.cleared = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        private int money;
        public int Money
        {
            get
            {
                return this.money;
            }
            set
            {
                if (this.money != value)
                {
                    this.money = value;
                    this.RaisePropertyChanged();
                }
            }
        }
    }
}
