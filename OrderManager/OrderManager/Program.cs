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

Operation operation = Operation.Initial;
PrintHelloMessage();
while ( operation != Operation.Exit )
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
}

static void PrintHelloMessage()
{
    Console.WriteLine( helloMessage );
    Console.WriteLine();
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
        case Operation.Initial:
            return;
        case Operation.Buy:
            PlacingOrder();
            break;
        case Operation.Exit:
            Console.WriteLine( "Goodbye. We'll be waiting for you again." );
            break;
        default:
            throw new Exception( $"Unknow operation error: {operation}" );
    }
}

static void PlacingOrder()
{
    Order order = new Order();
    order = GetUserData();
    ConfirmOrder( order );
}

static Order GetUserData()
{
    Order userOrder = new Order();
    Console.WriteLine( "Enter product name: " );
    var userProductName = Console.ReadLine();
    userOrder.product = userProductName;
    Console.WriteLine( "Enter product quantity: " );
    var userQuantity = Console.ReadLine();
    if ( !IsCorrectInt( userQuantity ) )
    {
        throw new Exception( $"{userQuantity} - is not a number" );
    }
    int quantity = int.Parse( userQuantity );
    userOrder.quantity = quantity;
    Console.WriteLine( "Enter your name: " );
    var userName = Console.ReadLine();
    userOrder.name = userName;
    Console.WriteLine( "Enter delivery adress: " );
    var userAdress = Console.ReadLine();
    userOrder.adress = userAdress;
    return userOrder;
}

static bool IsCorrectInt( string userInt )
{
    return int.TryParse( userInt, out int correctInt );
}

static void ConfirmOrder( Order order )
{
    Console.WriteLine( $"Hello, {order.name}!\nYou ordered {order.quantity} {order.product} to the adress: {order.adress}.\nIs it correct? Please enter your name when confirming your order. Otherwise, your order will be cancelled: " );
    string userData = Console.ReadLine();
    if ( userData == order.name )
    {
        PrintDeliveryMessage( order.adress );
        return;
    }
    Console.WriteLine( "The system recognized your answer as negative.\nYour order was cancelled, you can make it again." );
    return;
}

static void PrintDeliveryMessage( string adress )
{
    DateTime currentDayInUtc = DateTime.Today.AddDays( deliveryTimeInDays );
    Console.WriteLine( $"Thank you for your order! It will be shipped {currentDayInUtc.ToLongDateString()} to the following address: {adress}" );
}
struct Order
{
    public string product;
    public int quantity;
    public string name;
    public string adress;
}