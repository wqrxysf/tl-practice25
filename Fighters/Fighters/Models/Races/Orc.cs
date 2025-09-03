namespace Fighters.Models.Races;

public class Orc : IRace
{
    public int RaceDamage => 10;
    public int RaceHealthPoint => 150;
    public int RaceDodgeDamage => 0;
    public int RaceLuck => 0;
    public string RaceTitle = "Орк";

    public override string ToString()
    {
        return RaceTitle;
    }
}
