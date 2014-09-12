using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MamoruYatsu.Stages
{
    class Stage2 : IStage
    {
        public bool NewEnemy()
        {
            return App.Random.Next(3) == 0;
        }

        public int Reward
        {
            get
            {
                return 200;
            }
        }
    }
}
