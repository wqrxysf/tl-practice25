using CarFactory.Models.Cars;

namespace CarFactory
{
    public class CarManager
    {
        private List<ICar> _cars = new List<ICar>();
        private static string _menu = """
    1 - Cоздать автомобиль;
    2 - Печать всех автомобилей;
    3 - Удалить автомобиль;
    4 - Выход;
    """;

        public void Run()
        {
            int command;
            do
            {
                PrintMenu();
                Console.Write( "\nВыберите команду: " );
                string userInput = Console.ReadLine();

                if ( !int.TryParse( userInput, out command ) )
                {
                    Console.WriteLine( "Команда должна быть целым числом. Повторите ввод.\n" );
                    continue;
                }

                switch ( command )
                {
                    case 1:
                        Car newCar = Models.CarFactory.CarFactory.CreateNewCar();
                        CreateNewCar( newCar );
                        break;
                    case 2:
                        ShowAllCar();
                        break;
                    case 3:
                        DeleteCar();
                        break;
                    case 4:
                        Console.WriteLine( "Программа завершена.\n" );
                        break;
                    default:
                        HandleInvalidInput();
                        break;
                }
            } while ( command != 4 );
        }

        private static void HandleInvalidInput()
        {
            Console.WriteLine( "Неверная команда. Повторите ввод.\n" );
        }

        private static void PrintMenu()
        {
            Console.WriteLine( _menu );
        }

        private void CreateNewCar( ICar car )
        {
            if ( _cars.Any( existCar => existCar.Name == car.Name ) )
            {
                Console.WriteLine( $"Автомобиль с названием '{car.Name}' уже существует. Попробуйте другое название.\n" );
                return;
            }

            _cars.Add( car );
            Console.WriteLine( "Машина создана.\n" );
        }

        private void ShowAllCar()
        {
            if ( _cars.Count == 0 )
            {
                Console.WriteLine( "Нет созданных автомобилей.\n" );
            }
            else
            {
                foreach ( ICar car in _cars )
                {
                    car.ShowCarConfigurations();
                }
            }
        }

        private void DeleteCar()
        {
            Console.Write( "Введите название автомобиля: " );
            string name = Console.ReadLine();
            ICar carToDelete = _cars.FirstOrDefault( car => car.Name == name );

            if ( carToDelete != null )
            {
                _cars.Remove( carToDelete );
                Console.WriteLine( $"Автомобиль '{name}'  удален.\n" );
            }
            else
            {
                Console.WriteLine( $"Автомобиль с названием '{name}' не найден.\n" );
            }
        }
    }
}