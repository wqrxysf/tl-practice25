using System.Xml.Linq;

namespace Fighters.Models.Armors;

public class Chain : IArmor
{
    public string ArmorTitle => "Кольчуга";
    public int ArmorPoint => 25;

    public override string ToString()
    {
        return ArmorTitle;
    }
}
