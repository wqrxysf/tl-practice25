namespace Fighters.Models.Weapons;

public class Spear : IWeapon
{
    public string WeaponName => "Копьё";

    public int WeaponDamage => 8;

    public override string ToString()
    {
        return WeaponName;
    }
}
