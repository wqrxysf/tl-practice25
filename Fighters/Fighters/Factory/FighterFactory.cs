using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Factory;

public class FighterFactory
{
    public FighterFactory( string name, IRace race, IArmor armor, IWeapon weapon )
    {
        _name = name;
        _armor = armor;
        _weapon = weapon;
        _race = race;
    }
    private static string _name;
    private static IArmor _armor;
    private static IWeapon _weapon;
    private static IRace _race;
    private Fighter _type;

    public static IFighter CreateFighter( Fighter fighter )
    {
        switch ( fighter )
        {
            case Fighter.Stranger:
                return new Stranger( _name, _armor, _weapon, _race );
            case Fighter.Baron:
                return new Baron( _name, _armor, _weapon, _race );
            case Fighter.Lord:
                return new Lord( _name, _armor, _weapon, _race );
            case Fighter.Marquis:
                return new Marquis( _name, _armor, _weapon, _race );
            default:
                return new Stranger( _name, _armor, _weapon, _race );
        }
    }

    public static IFighter GenerateRandomFighter()
    {
        Random rnd = new Random();
        int rndFighter = rnd.Next( 0, 4 );
        return CreateFighter( ( Fighter )rndFighter );
    }
}