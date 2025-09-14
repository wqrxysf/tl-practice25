using Fighters.Models.Armors;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters;

public class Stranger( string name, IArmor armor, IWeapon weapon, IRace race ) : BaseFighter( name, armor, weapon, race )
{
    protected override int ClassHealthPoint => 20;
    protected override int ClassArmorPoint => 5;
    protected override int ClassDamage => 7;
    protected override int ClassLuckyDamage => 12;
}

