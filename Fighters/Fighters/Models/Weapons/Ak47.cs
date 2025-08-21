namespace Fighters.Models.Weapons;

public class Ak47 : IWeapon
{
    public string WeaponName => "Автомат Калашникова";

    public int WeaponDamage => 10;

    public override string ToString()
    {
        return WeaponName;
    }
}
