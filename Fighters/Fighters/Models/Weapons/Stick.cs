namespace Fighters.Models.Weapons;

public class Stick : IWeapon
{
    public string WeaponName => "Деревянная палка";
    public int WeaponDamage => 4;

    public override string ToString()
    {
        return WeaponName;
    }
}
