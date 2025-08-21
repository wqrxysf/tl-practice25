using Fighters.Core;

namespace Fighters;

public class Program
{
    public static void Main()
    {
        try
        {
            GameManager gm = new GameManager();
            Handler handler = new Handler();
            handler.HandleOperation( gm );
        }
        catch ( Exception ex )
        {
            Console.WriteLine( $"Error! {ex}" );
        }

    }
}