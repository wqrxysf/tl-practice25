namespace Fighters.Models.Armors;

public class Iron : IArmor
{
    public string ArmorTitle => "Железная броня";
    public int ArmorPoint => 52;

    public override string ToString()
    {
        return ArmorTitle;
    }
}
