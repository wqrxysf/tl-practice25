namespace Fighters.Models.Races;

public class Human : IRace
{
    public int RaceDamage => 5;

    public int RaceHealthPoint => 125;

    public int RaceDodgeDamage => 15;

    public int RaceLuck => 2;

    public string RaceTitle = "Человек";

    public override string ToString()
    {
        return RaceTitle;
    }
}
