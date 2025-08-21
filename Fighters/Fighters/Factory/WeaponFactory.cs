using Fighters.Models.Weapons;

namespace Fighters.Factory;

public class WeaponFactory
{
    public static IWeapon CreateWeapon( Weapon weapon )
    {
        switch ( weapon )
        {
            case Weapon.Fists:
                return new Fists();
            case Weapon.Stick:
                return new Stick();
            case Weapon.Spear:
                return new Spear();
            case Weapon.Ak47:
                return new Ak47();
            default:
                return new Fists();
        }
    }

    public static IWeapon GenerateRndWeapon()
    {
        Random rnd = new Random();
        int rndWeapon = rnd.Next( 0, 4 );
        return CreateWeapon( ( Weapon )rndWeapon );
    }
}
