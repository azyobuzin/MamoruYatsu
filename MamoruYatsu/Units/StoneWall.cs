namespace MamoruYatsu.Units
{
    class StoneWall : Wall
    {
        public StoneWall(Field field) : base(field, new UnitViews.StoneWall()) { }

        public override int Maximum
        {
            get
            {
                return 60;
            }
        }
    }
}
