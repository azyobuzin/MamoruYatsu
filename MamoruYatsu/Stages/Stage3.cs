using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MamoruYatsu.Stages
{
    class Stage3 : IStage
    {
        public int Number
        {
            get
            {
                return 3;
            }
        }

        public bool NewEnemy()
        {
            return App.Random.Next(1) == 0;
        }

        public int Reward
        {
            get
            {
                return 1000;
            }
        }
    }
}
