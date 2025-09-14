namespace Fighters.Models.Armors;

public class BulletproofVest : IArmor
{
    public string ArmorTitle => "Бронежилет";
    public int ArmorPoint => 100;

    public override string ToString()
    {
        return ArmorTitle;
    }
}
