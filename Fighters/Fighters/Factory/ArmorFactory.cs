using Fighters.Models.Armors;

namespace Fighters.Factory;

public class ArmorFactory
{
    public static IArmor CreateArmor( Armor armor )
    {
        switch ( armor )
        {
            case Armor.NoArmor:
                return new NoArmor();
            case Armor.Coat:
                return new Coat();
            case Armor.Chain:
                return new Chain();
            case Armor.Iron:
                return new Iron();
            case Armor.BulletproofVest:
                return new BulletproofVest();
            default:
                return new NoArmor();
        }
    }

    public static IArmor GenerateRndArmor()
    {
        Random rnd = new Random();
        int rndArmor = rnd.Next( 0, 5 );
        return CreateArmor( ( Armor )rndArmor );
    }
}