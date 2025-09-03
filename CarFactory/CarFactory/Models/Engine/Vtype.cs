namespace CarFactory.Models.Engine
{
    public class Vtype : IEngine
    {
        public string Name => "V-type";
        public int MaxSpeed => 300;
    }
}