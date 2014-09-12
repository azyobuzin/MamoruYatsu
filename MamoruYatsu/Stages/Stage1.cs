using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MamoruYatsu.Stages
{
    class Stage1 : IStage
    {
        public bool NewEnemy()
        {
            return App.Random.Next(4) == 0;
        }

        public int Reward
        {
            get
            {
                return 100;
            }
        }
    }
}
