using CarFactory.Models.BodyShape;
using CarFactory.Models.CarColor;
using CarFactory.Models.Engine;
using CarFactory.Models.Gearboxes;
using CarFactory.Models.SteeringPosition;

namespace CarFactory.Models.CarFactory
{
    public class CarConfiguration
    {
        private List<IBodyShape> _bodyShapes;
        private List<ICarColor> _carColors;
        private List<IEngine> _engines;
        private List<IGearboxes> _gearboxes;
        private List<ISteeringPosition> _steeringPositions;

        public CarConfiguration()
        {
            _bodyShapes = new List<IBodyShape> { new Crossover(), new Hatchback(), new Sedan() };
            _carColors = new List<ICarColor> { new Black(), new Blue(), new White() };
            _engines = new List<IEngine> { new Inline(), new Rotary(), new Vtype() };
            _gearboxes = new List<IGearboxes> { new Automatic(), new Manual(), new Robotic() };
            _steeringPositions = new List<ISteeringPosition> { new Left(), new Right() };
        }

        public List<IBodyShape> GetBodyShape() => _bodyShapes;
        public List<ICarColor> GetCarColor() => _carColors;
        public List<IEngine> GetEngine() => _engines;
        public List<IGearboxes> GetGearboxes() => _gearboxes;
        public List<ISteeringPosition> GetSteeringPositione() => _steeringPositions;
    }
}