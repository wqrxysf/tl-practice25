using Fighters.Models.Armors;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters;

public class Marquis( string name, IArmor armor, IWeapon weapon, IRace race ) : BaseFighter( name, armor, weapon, race )
{
    protected override int ClassHealthPoint => 25;
    protected override int ClassArmorPoint => 25;
    protected override int ClassDamage => 5;
    protected override int ClassLuckyDamage => 3;
}

