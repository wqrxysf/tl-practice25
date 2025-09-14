namespace Fighters.Models.Armors;

public class NoArmor : IArmor
{
    public string ArmorTitle => "Без брони";
    public int ArmorPoint => 0;

    public override string ToString()
    {
        return ArmorTitle;
    }
}
