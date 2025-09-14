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

    public static IArmor GenerateRandomArmor()
    {
        Random rnd = new Random();
        int rndArmor = rnd.Next( 0, 5 );
        return CreateArmor( ( Armor )rndArmor );
    }

    public static string GetArmorList()
    {
        return """
            1 - Без одежды;
            2 - Плащ;
            3 - Кольчужная мантия;
            4 - Железные доспехи;
            5 - Пуленепробиваемый бронежилет;
            """;
    }
}
