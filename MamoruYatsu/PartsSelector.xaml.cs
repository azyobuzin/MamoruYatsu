using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MamoruYatsu.Units;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace MamoruYatsu
{
    /// <summary>
    /// PartsSelector.xaml の相互作用ロジック
    /// </summary>
    public partial class PartsSelector : UserControl
    {
        public PartsSelector()
        {
            InitializeComponent();
        }

        private ViewModel vm;
        
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.vm = this.DataContext as ViewModel;
        }

        private bool CheckMoney(int price)
        {
            if (this.vm.Field.Money < price)
            {
                MessageBox.Show("お金が足りません。", "壁を買う");
                return false;
            }
            return true;
        }

        private void BuyWall(string wallName, Wall wall, int price)
        {
            if (!this.CheckMoney(price)) return;

            if (this.vm.Field.Walls.All(w => w != null))
            {
                MessageBox.Show("設置できる場所がありません。", "壁を買う");
                return;
            }

            var dialog = new TaskDialog();
            dialog.Caption = "壁を買う";
            dialog.InstructionText = wallName;
            dialog.Text = "設置場所を選択してください。";
            dialog.Cancelable = true;

            if (this.vm.Field.Walls[2] == null)
            {
                var left = new TaskDialogButton();
                left.Text = "左";
                left.Click += (_, __) =>
                {
                    for (var i = 0; i < 3; i++)
                    {
                        if (this.vm.Field.Walls[i] == null)
                        {
                            this.vm.Field.SetWall(wall, i);
                            break;
                        }
                    }
                    dialog.Close();
                };
                dialog.Controls.Add(left);
            }

            if (this.vm.Field.Walls[5] == null)
            {
                var center = new TaskDialogButton();
                center.Text = "中央";
                center.Click += (_, __) =>
                {
                    for (var i = 3; i < 6; i++)
                    {
                        if (this.vm.Field.Walls[i] == null)
                        {
                            this.vm.Field.SetWall(wall, i);
                            break;
                        }
                    }
                    dialog.Close();
                };
                dialog.Controls.Add(center);
            }

            if (this.vm.Field.Walls[8] == null)
            {
                var right = new TaskDialogButton();
                right.Text = "右";
                right.Click += (_, __) =>
                {
                    for (var i = 6; i < 9; i++)
                    {
                        if (this.vm.Field.Walls[i] == null)
                        {
                            this.vm.Field.SetWall(wall, i);
                            break;
                        }
                    }
                    dialog.Close();
                };
                dialog.Controls.Add(right);
            }

            this.IsEnabled = false;
            dialog.Show();
            this.vm.Field.Money -= price;
            this.IsEnabled = true;
        }

        private void wallWood_Click(object sender, RoutedEventArgs e)
        {
            this.BuyWall("木の壁", new WoodWall(this.vm.Field), 20);
        }

        private void wallStone_Click(object sender, RoutedEventArgs e)
        {
            this.BuyWall("石の壁", new StoneWall(this.vm.Field), 40);
        }

        private void wallDia_Click(object sender, RoutedEventArgs e)
        {
            this.BuyWall("ダイヤの壁", new DiamondWall(this.vm.Field), 120);
        }

        private void cure_Click(object sender, RoutedEventArgs e)
        {
            const int price = 200;
            if (!this.CheckMoney(price)) return;
            this.vm.Field.Money -= price;
            this.vm.Field.Castle.Cure();
        }

    }
}
