namespace CarFactory.Models.Engine
{
    public class Inline : IEngine
    {
        public string Name => "Inline";
        public int MaxSpeed => 210;
    }
}