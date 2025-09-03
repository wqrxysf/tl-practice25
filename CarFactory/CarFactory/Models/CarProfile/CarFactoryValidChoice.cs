namespace CarFactory.Models.CarFactory
{
    public class CarFactoryValidChoice
    {

        public static int GetValidChoice( string message, int maxChoice )
        {
            while ( true )
            {
                Console.Write( message );
                if ( int.TryParse( Console.ReadLine(), out int choice ) && choice >= 0 && choice < maxChoice )
                {
                    return choice;
                }
                else
                {
                    Console.WriteLine( $"Введите корректное число от 0 до {maxChoice - 1}." );
                }
            }
        }
    }
}