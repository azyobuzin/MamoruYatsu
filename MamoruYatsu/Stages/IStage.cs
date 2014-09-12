using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MamoruYatsu.Stages
{
    interface IStage
    {
        bool NewEnemy();
        int Reward { get; }
    }
}
