namespace Fighters.Models.Weapons;

public class Fists : IWeapon
{
    public string WeaponName => "Кулаки";

    public int WeaponDamage => 2;

    public override string ToString()
    {
        return WeaponName;
    }
}
