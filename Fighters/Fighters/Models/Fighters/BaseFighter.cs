using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;
using Fighters.Models.Armors;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters;

public abstract class BaseFighter : IFighter
{
    public string Name { get; private set; }
    public IRace Race => _race;
    public IArmor Armor => _armor;
    public IWeapon Weapon => _weapon;

    protected readonly IRace _race;
    protected IArmor _armor;
    protected IWeapon _weapon;

    protected int CurrentHealthPoint;
    protected int CurrentArmorPoint;

    protected virtual int ClassHealthPoint => 0;
    protected virtual int ClassDamage => 0;
    protected virtual int ClassArmorPoint => 0;

    protected virtual int ClassLuckyDamage => 0;

    protected Random rnd = new Random();

    protected BaseFighter( string name, IArmor armor, IWeapon weapon, IRace race )
    {
        Name = name;
        _race = race;
        _armor = armor;
        _weapon = weapon;
        CurrentHealthPoint = GetMaxHealthPoint();
        CurrentArmorPoint = GetCurrentArmorPoint();
    }

    public override string ToString()
    {
        return $"{_race} {Name}, одетый в {_armor} с {_weapon}";
    }

    public int Attack( IFighter opponent )
    {
        int damage = CalculateDamage();
        bool wasTaken = opponent.TakeDamage( damage );
        if ( wasTaken )
        {
            return damage;
        }
        return 0;

    }

    public bool TakeDamage( int damage )
    {
        if ( !CanDodge() )
        {
            if ( CurrentArmorPoint + CurrentHealthPoint < damage )
            {
                CurrentHealthPoint = 0;
                CurrentArmorPoint = 0;
            }
            else
            {
                CurrentArmorPoint = CurrentArmorPoint < damage ? 0 : CurrentArmorPoint - damage;
                if ( CurrentArmorPoint == 0 )
                {
                    damage -= CurrentArmorPoint;
                }
                CurrentHealthPoint -= damage;
            }
            return true;
        }
        return false;
    }

    public int CalculateDamage()
    {
        return _weapon.WeaponDamage + _race.RaceDamage + ClassDamage + CalculateLuckyDamage();
    }

    public int CalculateLuckyDamage()
    {
        int rndNumber = rnd.Next( 1, 101 );
        return rndNumber > _race.RaceLuck ? ClassLuckyDamage : 0;
    }

    public bool CanDodge()
    {
        int rndNumber = rnd.Next( 1, 101 );
        return rndNumber > _race.RaceDodgeDamage ? false : true;
    }

    public int GetCurrentHealthPoint()
    {
        return CurrentHealthPoint;
    }

    public int GetMaxHealthPoint()
    {
        return ClassHealthPoint + _race.RaceHealthPoint;
    }

    public int GetCurrentArmorPoint()
    {
        return CurrentArmorPoint;
    }

    public int GetMaxArmorPoint()
    {
        return ClassArmorPoint + _armor.ArmorPoint;
    }

    public bool IsAlive()
    {
        return CurrentHealthPoint > 0 ? true : false;
    }

    public void ReturnHeroPoints()
    {
        CurrentHealthPoint = GetMaxHealthPoint();
        CurrentArmorPoint = GetCurrentArmorPoint();
    }
}
