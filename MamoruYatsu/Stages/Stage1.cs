namespace MamoruYatsu.Stages
{
    class Stage1 : IStage
    {
        public int Number
        {
            get
            {
                return 1;
            }
        }

        public bool NewEnemy()
        {
            return App.Random.Next(12) == 0;
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
