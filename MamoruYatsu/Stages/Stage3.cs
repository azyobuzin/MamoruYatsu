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
            return App.Random.Next(2) == 0;
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
