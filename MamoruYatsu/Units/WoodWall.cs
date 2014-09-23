namespace MamoruYatsu.Units
{
    class WoodWall : Wall
    {
        public WoodWall(Field field) : base(field, new UnitViews.WoodWall()) { }

        public override int Maximum
        {
            get
            {
                return 30;
            }
        }
    }
}
