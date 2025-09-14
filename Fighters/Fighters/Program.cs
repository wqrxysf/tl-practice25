using Fighters.Core;

namespace Fighters;

public class Program
{
    public static void Main()
    {
        try
        {
            GameManager GameManager = new GameManager();
            GameManager.HandleOperation();
        }
        catch ( Exception exception )
        {
            Console.WriteLine( $"Error! {exception}" );
        }

    }
}