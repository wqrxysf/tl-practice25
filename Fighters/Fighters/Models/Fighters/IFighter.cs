using System.Security.Cryptography.X509Certificates;
using Fighters.Models.Armors;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters;

public interface IFighter
{
    public string Name { get; }
    public IRace Race { get; }
    public IArmor Armor { get; }
    public IWeapon Weapon { get; }

    public int GetMaxHealthPoint();
    public int GetCurrentHealthPoint();
    public int GetMaxArmorPoint();
    public int GetCurrentArmorPoint();
    public void ReturnHeroPoints();

    public int CalculateDamage();
    public int CalculateLuckyDamage();
    public bool IsDodge();

    public bool TakeDamage( int damage );

    public bool IsAlive();

    public int Attack( IFighter opponent );
}
