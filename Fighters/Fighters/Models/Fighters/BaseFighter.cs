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

    protected int _currentHealthPoint;
    protected int _currentArmorPoint;

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
        _currentHealthPoint = GetMaxHealthPoint();
        _currentArmorPoint = GetCurrentArmorPoint();
    }

    public override string ToString()
    {
        return $"{_race} {Name}, одетый в {_armor} с {_weapon}";
    }

    public int Attack( IFighter opponent )
    {
        int damage = CalculateDamage();
        opponent.TakeDamage( damage );
        return damage;
    }

    public void TakeDamage( int damage )
    {
        if ( !IsDodge() )
        {
            if ( _currentArmorPoint + _currentHealthPoint < damage )
            {
                _currentHealthPoint = 0;
                _currentArmorPoint = 0;
            }
            else
            {
                _currentArmorPoint = _currentArmorPoint < damage ? 0 : _currentArmorPoint - damage;
                if ( _currentArmorPoint == 0 )
                {
                    damage -= _currentArmorPoint;
                }
                _currentHealthPoint -= damage;
            }
        }
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

    public bool IsDodge()
    {
        int rndNumber = rnd.Next( 1, 101 );
        return rndNumber > _race.RaceDodgeDamage ? false : true;
    }

    public int GetCurrentHealthPoint()
    {
        return _currentHealthPoint;
    }

    public int GetMaxHealthPoint()
    {
        return ClassHealthPoint + _race.RaceHealthPoint;
    }

    public int GetCurrentArmorPoint()
    {
        return _currentArmorPoint;
    }

    public int GetMaxArmorPoint()
    {
        return ClassArmorPoint + _armor.ArmorPoint;
    }

    public bool IsAlive()
    {
        return _currentHealthPoint > 0;
    }
}
