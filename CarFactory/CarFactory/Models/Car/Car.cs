using CarFactory.Models.BodyShape;
using CarFactory.Models.CarColor;
using CarFactory.Models.Engine;
using CarFactory.Models.Gearboxes;
using CarFactory.Models.SteeringPosition;

namespace CarFactory.Models.Cars
{
    public class Car : ICar
    {
        public IBodyShape _bodyShape;
        public ICarColor _color;
        public IEngine _engine;
        public IGearboxes _gearbox;
        public ISteeringPosition _steeringPosition;

        public string Name { get; private set; }

        public Car( string name, IBodyShape bodyShape, ICarColor color, IEngine engine, IGearboxes gearbox, ISteeringPosition steeringPosition )
        {
            Name = name;
            _bodyShape = bodyShape;
            _color = color;
            _engine = engine;
            _gearbox = gearbox;
            _steeringPosition = steeringPosition;
        }

        public int GetMaxSpeed() => _engine.MaxSpeed;

        public int GetGearsNumber() => _gearbox.GearsNumber;

        public void ShowCarConfigurations()
        {
            Console.WriteLine( ToString() );
        }

        public override string ToString()
        {
            return $"\nНазвание машины: {Name}\n" +
                   $"Форма кузова: {_bodyShape.Name}\n" +
                   $"Цвет машины: {_color.Name}\n" +
                   $"Тип двигателя: {_engine.Name}\n" +
                   $"Коробка передач: {_gearbox.Name}\n" +
                   $"Позиция руля: {_steeringPosition.Name}\n" +
                   $"Максимальная скорость: {GetMaxSpeed()} км/ч\n" +
                   $"Количество передач: {GetGearsNumber()}";
        }
    }
}