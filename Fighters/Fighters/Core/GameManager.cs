using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fighters.Factory;
using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Core;
public class GameManager
{
    private static readonly int botsInGame = 5;
    private static List<IFighter> _fighters = [];
    public IFighter? _hero = null;

    public void AddFighterInList( IFighter fighter )
    {
        _fighters.Add( fighter );
    }

    public void RemoveFighterFromList( IFighter fighter )
    {
        if ( _fighters.Count > 0 )
        {
            _fighters.Remove( fighter );
        }
    }

    public void RunGame()
    {
        InitializationBots();
        var opponent = GetOpponent();
        StartFight(_hero, opponent );
    }

    public string StartFight( IFighter userFighter, IFighter computerFighter )
    {
        while ( !userFighter.IsAlive() || !computerFighter.IsAlive() )
        {
            userFighter.Attack( computerFighter );
            computerFighter.Attack( userFighter );
        }
        return userFighter.IsAlive() ? $"{userFighter.Name} is won!" : $"{computerFighter.Name} is won!";
    }

    public void CreateHero()
    {
        IRace race = GetRace();
        Fighter fighterClass = GetClass();
        IWeapon weapon = GetWeapon();
        IArmor armor = GetArmor();
        string name = GetName();
        FighterFactory factory = new FighterFactory( name, race, armor, weapon );
        _hero = FighterFactory.CreateFighter( fighterClass );
    }

    public static IRace GetRace()
    {
        Console.WriteLine( """
            Выберите расу своего персонажа:
            1 - Человек;
            2 - Орк;
            3 - Демон;
            """ );
        IRace race = new Human();
        string userInput;
        bool isCorrect = false;
        while ( !isCorrect )
        {
            userInput = Console.ReadLine();
            bool isParsed = Enum.TryParse( userInput, out Race raceEnum );
            if ( isParsed )
            {
                race = RaceFactory.CreateRace( raceEnum );
                isCorrect = true;
            }
            else
            {
                Console.WriteLine( $"Упс! Некорректное значение: {userInput}. Попробуй ещё раз." );
            }
        }
        return race;
    }

    public static Fighter GetClass()
    {
        Console.WriteLine( """
            Выберите класс своего персонажа:
            1 - Чужеземец;
            2 - Барон;
            3 - Лорд;
            4 - Маркиз;
            """ );
        Fighter fighterClass = Fighter.Stranger;
        string userInput;
        bool isCorrect = false;
        while ( !isCorrect )
        {
            userInput = Console.ReadLine();
            bool isParsed = Enum.TryParse( userInput, out Fighter fighterEnum );
            if ( isParsed )
            {
                isCorrect = true;
            }
            else
            {
                Console.WriteLine( $"Упс! Некорректное значение: {userInput}. Попробуй ещё раз." );
            }
        }
        return fighterClass;
    }

    public static IWeapon GetWeapon()
    {
        Console.WriteLine( """
            Выберите оружие для своего персонажа:
            1 - Кулаки;
            2 - Палка;
            3 - Копьё;
            4 - Автомат Калашникова;
            """ );
        IWeapon weapon = new Fists();
        string userInput;
        bool isCorrect = false;
        while ( !isCorrect )
        {
            userInput = Console.ReadLine();
            bool isParsed = Enum.TryParse( userInput, out Weapon weaponEnum );
            if ( isParsed )
            {
                weapon = WeaponFactory.CreateWeapon( weaponEnum );
                isCorrect = true;
            }
            else
            {
                Console.WriteLine( $"Упс! Некорректное значение: {userInput}. Попробуй ещё раз." );
            }
        }
        return weapon;
    }

    public static IArmor GetArmor()
    {
        Console.WriteLine( """
            Выберите одеяние для своего персонажа:
            1 - Без одежды;
            2 - Плащ;
            3 - Кольчужная мантия;
            4 - Железные доспехи;
            5 - Пуленепробиваемый бронежилет;
            """ );
        IArmor armor = new NoArmor();
        string userInput;
        bool isCorrect = false;
        while ( !isCorrect )
        {
            userInput = Console.ReadLine();
            bool isParsed = Enum.TryParse( userInput, out Armor armorEnum );
            if ( isParsed )
            {
                armor = ArmorFactory.CreateArmor( armorEnum );
                isCorrect = true;
            }
            else
            {
                Console.WriteLine( $"Упс! Некорректное значение: {userInput}. Попробуй ещё раз." );
            }
        }
        return armor;
    }

    public static string GetName()
    {
        Console.Write( "Введите имя своего персонажа: " );
        string name = Console.ReadLine();
        return name;
    }

    public static void InitializationBots()
    {
        if ( _fighters.Count == 0 )
        {
            while ( _fighters.Count != botsInGame )
            {
                var factory = new FighterFactory( $"bot {_fighters.Count + 1}", RaceFactory.GenerateRndRace(), ArmorFactory.GenerateRndArmor(), WeaponFactory.GenerateRndWeapon() );
                var fighter = FighterFactory.GenerateRndFighter();
                _fighters.Add( fighter );
            }
        }
    }

    public static IFighter GetOpponent()
    {
        Console.WriteLine( "Выбери своего оппонента: " );
        foreach ( var opponent in _fighters )
        {
            int index = _fighters.FindIndex( x => x == opponent );
            Console.WriteLine( (index + 1) + ") " + opponent );
        }
        Console.Write( "Введи номер противника: " );
        int value = 0;
        bool isCorrect = false;
        while ( !isCorrect )
        {
            string userInput = Console.ReadLine();
            if ( int.TryParse( userInput, out value ) )
            {
                isCorrect = true;
            }
            else
            {
                Console.WriteLine( $"У-у-пс... Некорректное значение: {userInput}" );
            }
        }
        return _fighters[ value - 1 ];
    }
}
