namespace CarFactory.Models.Gearboxes
{
    public class Automatic : IGearboxes
    {
        public string Name => "Automatic";
        public int GearsNumber => 5;
    }
}