using Fighters.Models.Fighters;

namespace Fighters.Core;

public class Handler
{
    private IFighter? _hero = null;
    private const string operationMenu = """
        1 - Создать нового персонажа;
        2 - Посмотреть своего персонажа;
        3 - Войти в колизей;
        4 - Выйти из игры;
        """;

    public void HandleOperation( GameManager gameManager )
    {
        Operations operation = Operations.Internal;
        while ( operation != Operations.Exit )
        {
            operation = GetCommand();
            switch ( operation )
            {
                case Operations.CreateHero:
                    gameManager.CreateHero();
                    break;
                case Operations.ShowHero:
                    gameManager.ShowHero();
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

    private static Operations GetCommand()
    {
        Operations operation = Operations.Internal;
        Console.WriteLine( operationMenu );
        string userOperation = Console.ReadLine();
        bool isParsed = Enum.TryParse( userOperation, out operation );
        return operation;
    }
}