namespace MamoruYatsu.Units
{
    class DiamondWall : Wall
    {
        public DiamondWall(Field field) : base(field, new UnitViews.DiamondWall()) { }

        public override int Maximum
        {
            get
            {
                return 120;
            }
        }
    }
}
