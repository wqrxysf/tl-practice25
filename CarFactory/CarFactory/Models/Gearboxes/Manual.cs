namespace CarFactory.Models.Gearboxes
{
    public class Manual : IGearboxes
    {
        public string Name => "Manual";
        public int GearsNumber => 5;
    }
}