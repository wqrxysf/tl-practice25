using Fighters.Models.Races;

namespace Fighters.Factory;

public class RaceFactory
{
    public static IRace CreateRace( Race race )
    {
        switch ( race )
        {
            case Race.Human:
                return new Human();
            case Race.Orc:
                return new Orc();
            case Race.Demon:
                return new Demon();
            default:
                return new Human();
        }
    }

    public static IRace GenerateRandomRace()
    {
        Random rnd = new Random();
        int rndRace = rnd.Next( 0, 3 );
        return CreateRace( ( Race )rndRace );
    }
}
