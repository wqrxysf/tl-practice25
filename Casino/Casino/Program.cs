using Casino;

List<int> winningUserNumbers = [ 18, 19, 20 ];

const double victoryMultiplicator = 0.2;

const string casinoName = """
    ####   ##    ####  ####  #  #  ####
    #     #  #  #       ##   ## #  #  #
    #     ####   ####   ##   # ##  #  #
    #     #  #      #   ##   #  #  #  #
    ####  #  #  ####   ####  #  #  ####

    """;

const string casinoMenu = """
        M E N U
    1 - Play Casino;
    2 - Check Balance;
    3 - Exit;
    Please, enter your choice: 
    """;

PrintGameName( casinoName );
int balance = GetBalance();

Operation? operation = Operation.Initial;
while ( operation != Operation.Exit )
{
    try
    {
        Console.Write( casinoMenu );
        operation = ReadOperation();
        if ( operation != null )
        {
            balance = HandleOperation( operation, balance );
        }
        else
        {
            continue;
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
}

static int GetBalance()
{
    bool isCorrectBalance = false;
    while ( !isCorrectBalance )
    {
        try
        {
            Console.Write( "Enter amount of money on your balance: " );
            string userBalance = Console.ReadLine();
            if ( !( int.TryParse( userBalance, out int balance ) ) )
            {
                Console.WriteLine( $"Invalid balance value: {userBalance}" );
            }
            if ( balance <= 0 )
            {
                Console.WriteLine( $"Your balance {balance} is too low..." );
            }
            isCorrectBalance = true;
            return balance;
        }
        catch ( Exception e )
        {
            Console.WriteLine( $"Error detected: {e}" );
        }
    }
    return 0;
}

static Operation? ReadOperation()
{
    string operationStr = Console.ReadLine();
    bool isParsed = Enum.TryParse( operationStr, out Operation operation );
    return isParsed ? operation : null;
}

int HandleOperation( Operation? operation, int balance )
{
    switch ( operation )
    {
        case Operation.Play:
            int bet = GetUserBet( balance );
            balance -= bet;
            balance += StartGame( bet );
            return balance;
        case Operation.PrintBalance:
            Console.WriteLine( $"Your balance: {balance}.\nWould you like to play?\n" );
            return balance;
        case Operation.Exit:
            Console.WriteLine( "Goodbye! We are waiting you for a new game!" );
            break;
        default:
            throw new Exception( $"Unsupported operation passed: {operation}" );
    }
    return balance;
}

int StartGame( int bet )
{
    int randomUserNumber = GetRandomNumber();
    if ( winningUserNumbers.Contains( randomUserNumber ) )
    {
        int winAmount = CalculateAmount( bet, randomUserNumber );
        Console.WriteLine( $"Congratulations! You have won {winAmount}.\nRemember, the casino does not spare the lucky ones.\n" );
        return winAmount;
    }
    else
    {
        Console.WriteLine( $"Sorry, your bet {bet} has been lost!\n" );
        return 0;
    }
}

static int GetUserBet( int balance )
{
    Console.Write( "Enter your bet. It will be deducted from the total balance: " );
    string userBet = Console.ReadLine();
    if ( int.TryParse( userBet, out int bet ) )
    {
        if ( bet > balance || bet <= 0 )
        {
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

static int GetRandomNumber()
{
    Random random = new Random();
    int randomNumber = random.Next( 1, 21 );
    return randomNumber;
}

static int CalculateAmount( int bet, int randomUserNumber )
{
    double amountDbl = ( bet * ( 1 + victoryMultiplicator ) * ( randomUserNumber % 17 ) );
    return Convert.ToInt32( amountDbl );
}