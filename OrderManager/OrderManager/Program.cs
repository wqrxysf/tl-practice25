using System.Diagnostics.Metrics;
using System.Globalization;
using System.Xml.Linq;
using OrderManager;

const int deliveryTimeInDays = 3;
const string helloMessage = """

                 ====
    Welcome to the store "magazin na divane"!
    Our prices are lower than on "Wildberries"
                 ====

    """;
const string menuMessage = """
        M E N U
    1 - Start shopping;
    2 - Exit;
    """;

Operation operation = Operation.Unknown;
PrintHelloMessage();
do
{
    PrintMenu();
    try
    {
        operation = ReadOperation();
        if ( operation == Operation.Unknown )
        {
            throw new Exception( $"Unknown operation error: {operation}" );
        }
        HandleOperations( operation );
    }
    catch ( Exception e )
    {
        Console.WriteLine( $"ERROR FOUND! {e}" );
    }
} while ( operation != Operation.Exit );

static void PrintHelloMessage()
{
    Console.WriteLine( helloMessage );
}

static void PrintMenu()
{
    Console.WriteLine( menuMessage );
}

static Operation ReadOperation()
{
    string userOperation = Console.ReadLine();
    return ( Enum.TryParse( userOperation, out Operation operation ) ) ? operation : Operation.Unknown;
}

void HandleOperations( Operation operation )
{
    switch ( operation )
    {
        case Operation.Buy:
            PlaceOrder();
            break;
        case Operation.Exit:
            Console.WriteLine( "Goodbye. We'll be waiting for you again." );
            break;
        default:
            throw new Exception( $"Unknow operation error: {operation}" );
    }
}

static void PlaceOrder()
{
    Order order = GetUserData();
    ConfirmOrder( order );
}


static Order GetUserData()
{
    Console.WriteLine( "Enter product name: " );
    string userProductName = Console.ReadLine();

    Console.WriteLine( "Enter product quantity: " );
    int quantity = int.Parse( Console.ReadLine() );

    Console.WriteLine( "Enter your name: " );
    string userName = Console.ReadLine();

    Console.WriteLine( "Enter delivery address: " );
    string userAddress = Console.ReadLine();

    return new Order( userProductName, quantity, userName, userAddress );
}


static bool IsCorrectInt( string userInt )
{
    return int.TryParse( userInt, out int correctInt );
}

static void ConfirmOrder( Order order )
{
    Console.WriteLine(
        $"Hello, {order.Name}!\n" +
        $"You ordered {order.Quantity} {order.Product} to the address: {order.Address}.\n" +
        "Is it correct? Please enter your name when confirming your order. Otherwise, your order will be cancelled: "
    );

    string userData = Console.ReadLine();
    if ( userData == order.Name )
    {
        PrintDeliveryMessage( order.Address );
        return;
    }

    Console.WriteLine( "The system recognized your answer as negative.\nYour order was cancelled, you can make it again." );
}


static void PrintDeliveryMessage( string adress )
{
    DateTime currentDayInUtc = DateTime.Today.AddDays( deliveryTimeInDays );
    Console.WriteLine( $"Thank you for your order! It will be shipped {currentDayInUtc.ToLongDateString()} to the following address: {adress}" );
}
public record Order( string Product, int Quantity, string Name, string Address );