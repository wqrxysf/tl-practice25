namespace Fighters.Models.Armors;

public class Coat : IArmor
{
    public string ArmorTitle => "Пальто";

    public int ArmorPoint => 10;

    public override string ToString()
    {
        return ArmorTitle;
    }
}
