using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MamoruYatsu
{
    /// <summary>
    /// GameCanvas.xaml の相互作用ロジック
    /// </summary>
    public partial class GameCanvas : Canvas
    {
        public GameCanvas()
        {
            InitializeComponent();
        }

        private ViewModel vm;

        private void Canvas_Loaded(object sender, RoutedEventArgs e)
        {
            this.vm = this.DataContext as ViewModel;
            this.Children.Add(this.vm.Field.Castle.UI);

            this.vm.Field.WallsChanging += (_, x) =>
            {
                var w = this.vm.Field.Walls[x.Data];
                if (w != null)
                    this.Children.Remove(w.UI);
            };
            this.vm.Field.WallsChanged += (_, x) =>
            {
                var w = this.vm.Field.Walls[x.Data];
                if (w != null)
                    this.Children.Add(w.UI);
            };

            foreach (var w in this.vm.Field.Walls.Where(x => x != null))
                this.Children.Add(w.UI);

            this.vm.Field.CreatedEnemy += (_, x) =>
            {
                var ui = x.Data.UI;
                this.Children.Add(ui);
                x.Data.Start();
                x.Data.UIChanged += (__, ___) =>
                {
                    this.Children.Remove(ui);
                    ui = x.Data.UI;
                    this.Children.Add(ui);
                };
                x.Data.Disappeared += (__, ___) => this.Children.Remove(ui);
            };
        }
    }
}
