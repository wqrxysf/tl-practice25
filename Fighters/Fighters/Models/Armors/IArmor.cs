using System.Xml.Linq;

namespace Fighters.Models.Armors;

public interface IArmor
{
    string ArmorTitle { get; }
    public int ArmorPoint { get; }
}