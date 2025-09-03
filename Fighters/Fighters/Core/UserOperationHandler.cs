using Fighters.Models.Fighters;

namespace Fighters.Core;

public class UserOperationHandler
{
    private IFighter? _hero = null;
    private const string operationMenu = """
        1 - Создать нового персонажа;
        2 - Посмотреть своего персонажа;
        3 - Войти в колизей;
        4 - Выйти из игры;
        """;

    public Operation GetCommand()
    {
        Operation operation = Operation.Internal;
        Console.WriteLine( operationMenu );
        string userOperation = Console.ReadLine();
        Enum.TryParse( userOperation, out operation );
        return operation;
    }
}