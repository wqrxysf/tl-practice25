using Fighters.Models.Fighters;

namespace Fighters.Core;
public class Handler
{
    private IFighter? _hero = null;

    public static void HandleOperation( GameManager gameManager, Operations operation )
    {
        switch ( operation )
        {
            case Operations.CreateHero:
                gameManager.CreateHero();
                break;
            case Operations.StartFight:
                if ( gameManager._hero != null )
                {
                    gameManager.RunGame();
                }
                else
                {
                    Console.WriteLine( "У-упс... Вы ещё не создали персонажа!" );
                }
                break;
            case Operations.Exit:
                        break;
            default:
                throw new Exception();
            }
    }
}