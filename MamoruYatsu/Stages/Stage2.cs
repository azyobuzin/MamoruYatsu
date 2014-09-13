using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MamoruYatsu.Stages
{
    class Stage2 : IStage
    {
        public int Number
        {
            get
            {
                return 2;
            }
        }

        public bool NewEnemy()
        {
            return App.Random.Next(4) == 0;
        }

        public int Reward
        {
            get
            {
                return 600;
            }
        }
    }
}
