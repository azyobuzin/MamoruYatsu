using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MamoruYatsu.Stages
{
    class Stage3 : IStage
    {
        public bool NewEnemy()
        {
            return App.Random.Next(2) == 0;
        }

        public int Reward
        {
            get
            {
                return 300;
            }
        }
    }
}
