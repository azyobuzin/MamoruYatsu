﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MamoruYatsu.Stages
{
    interface IStage
    {
        int Number { get; }
        bool NewEnemy();
        int Reward { get; }
    }
}
