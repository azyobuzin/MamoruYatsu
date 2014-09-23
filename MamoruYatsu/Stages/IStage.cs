namespace MamoruYatsu.Stages
{
    interface IStage
    {
        int Number { get; }
        bool NewEnemy();
        int Reward { get; }
    }
}
