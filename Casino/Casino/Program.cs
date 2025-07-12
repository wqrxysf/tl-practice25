using Casino;

const string casinoName = """
    ####   ##    ####  ####  #  #  ####
    #     #  #  #       ##   ## #  #  #
    #     ####   ####   ##   # ##  #  #
    #     #  #      #   ##   #  #  #  #
    ####  #  #  ####   ####  #  #  ####
    """;

const string menuPosition = """
        M E N U
    1 - Play Casino;
    2 - Check Balance;
    3 - Exit;
    Please, enter your choise: 
    """;

const double multiplicator = 0.2;


PrintGameName( casinoName );
int balance = GetBalance();

Operation? operation = Operation.Initial;
while ( operation != Operation.Exit )
{
    try
    {
        Console.Write( menuPosition );
        operation = OperationRead();
        if ( operation != null )
        {
            HandleOperation( operation, ref balance );
        }
        else
        {
            throw new Exception( $"Unknow user command" );
        }
    }
    catch ( Exception e )
    {
        Console.WriteLine( $"Error detected: {e}" );
    }

}

static void PrintGameName( string gameName )
{
    Console.WriteLine( gameName );
    Console.WriteLine();
}

static int GetBalance()
{
    bool balanceCorrect = false;
    while ( !balanceCorrect )
    {
        try
        {
            Console.Write( "Enter amount of money on your balance: " );
            string userBalance = Console.ReadLine();
            if ( !( int.TryParse( userBalance, out int balance ) ) )
            {
                throw new Exception( $"Invalid balance value: {userBalance}" );
            }
            if ( balance <= 0 )
            {
                throw new Exception( $"Your balance {balance} is too low..." );
            }
            balanceCorrect = true;
            return balance;
        }
        catch ( Exception e )
        {
            Console.WriteLine( $"Error detected: {e}" );
        }
    }
    return 0;
}

static Operation? OperationRead()
{
    string operationStr = Console.ReadLine();
    bool isParsed = Enum.TryParse( operationStr, out Operation operation );
    return isParsed ? operation : null;
}

static void HandleOperation( Operation? operation, ref int balance )
{
    switch ( operation )
    {
        case Operation.Initial:
            return;
        case Operation.Play:
            int bet = GetUserBet( ref balance );
            balance += StartGame( bet );
            break;
        case Operation.CheckBalance:
            Console.WriteLine( $"Your balance: {balance}.\nWould you like to play?\n" );
            break;
        case Operation.Exit:
            Console.WriteLine( "Goodbye! We are waiting you for a new game!" );
            break;
        default:
            throw new Exception( $"Unsupported operation passed: {operation}" );
    }
}

static int StartGame( int bet )
{
    int randomUserNumber = SetRandomNumber();
    if ( randomUserNumber == 18 | randomUserNumber == 19 | randomUserNumber == 20 )
    {
        int winAmmo = CalculateAmount( bet, randomUserNumber );
        Console.WriteLine( $"Congratulations! You have won {winAmmo}.\nRemember, the casino does not spare the lucky ones.\n" );
        return winAmmo;
    }
    else
    {
        Console.WriteLine( $"Sorry, your bet {bet} has been lost!\n" );
        return 0;
    }
}

static int GetUserBet( ref int balance )
{
    Console.Write( "Enter your bet. It will be deducted from the total balance: " );
    string userBet = Console.ReadLine();
    if ( int.TryParse( userBet, out int bet ) )
    {
        balance -= bet;
        if ( balance < 0 | bet <= 0 )
        {
            balance += bet;
            throw new Exception( $"Invalid bet value: {bet}" );
        }
        else
        {
            return bet;
        }
    }
    else
    {
        throw new Exception( $"Invalid bet value: {bet}" );
    }
}

static int SetRandomNumber()
{
    Random random = new Random();
    int randomNumber = random.Next( 0, 21 );
    return randomNumber;
}

static int CalculateAmount( int bet, int randomUserNumber )
{
    double amountDbl = ( bet * ( 1 + multiplicator ) * ( randomUserNumber % 17 ) );
    return Convert.ToInt32( amountDbl );
}