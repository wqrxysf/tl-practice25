namespace CarFactory.Models.Cars
{
    public interface ICar
    {
        public string Name { get; }

        public int GetMaxSpeed();
        public int GetGearsNumber();
        public void ShowCarConfigurations();
    }
}