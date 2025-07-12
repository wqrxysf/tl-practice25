using System.Diagnostics.Metrics;
using System.Globalization;
using System.Xml.Linq;
using OrderManager;

const int deliveryTime = 3;
const string adminPassword = "admin123";
const string helloMessage = """

                 ====
    Welcome to the store "magazin na divane"!
    Our prices are lower than on "Wildberries"
                 ====
    """;
const string menuMessage = """
        M E N U
    1 - Start shopping;
    2 - List of products;
    3 - Exit;
    4 - (admin only);
    """;
List<string> stockItems = [ "Coconut", "iphone 16 pro max 256gb", "travelline print t-shirt" ];
List<int> priceStockItems = [ 100, 95000, 999999999 ];
const string adminMenu = """
    1 - Add new position;
    2 - Delete existing position;
    3 - Change product information
    """;

Operation operation = Operation.Initial;
PrintHelloMessage();
while ( operation != Operation.Exit )
{
    PrintMenu();
    try
    {
        operation = ReadOperation();
        if ( operation == Operation.Null )
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
    return ( Enum.TryParse( userOperation, out Operation operation ) ) ? operation : Operation.Null;
}
void HandleOperations( Operation operation )
{
    switch ( operation )
    {
        case Operation.Initial:
            return;
        case Operation.Buy:
            StartOrder();
            break;
        case Operation.AdminLogin:
            AdminAuth();
            break;
        case Operation.Exit:
            Console.WriteLine( "Goodbye. We'll be waiting for you again." );
            break;
        case Operation.List:
            GetStockItems();
            break;
        default:
            throw new Exception( $"Unknow operation error: {operation}" );
    }
}

void StartOrder()
{
    string name = "";
    string adress = "";
    int item = 0;
    int amount = 0;
    GetUserData( ref name, ref adress, ref item, ref amount );
    if ( !PrintOrderConfirmation( name, adress, item, amount ) )
    {
        return;
    }
    PrintOrderInformation( name, adress, item, amount );
}
void GetUserData( ref string name, ref string adress, ref int item, ref int amount )
{
    Console.WriteLine( "\nPlease fill in the following details to place your order." );
    Console.Write( "Name: " );
    name = Console.ReadLine();
    Console.Write( "Address: " );
    adress = Console.ReadLine();
    if ( name == "" | adress == "" )
    {
        throw new Exception( "Missing name or adress" );
    }
    GetStockItems();
    item = GetUserItem();
    amount = GetUserAmount();
}

void GetStockItems()
{
    Console.WriteLine( "Products available for order:" );
    for ( int i = 0; i < stockItems.Count; i++ )
    {
        string currentItem = stockItems[ i ];
        int currentPrice = priceStockItems[ i ];
        Console.WriteLine( $"{i + 1}){currentItem} - {currentPrice}$" );
    }
}
int GetUserItem()
{
    Console.Write( "\nEnter the number of the selected product: " );
    var userItem = Console.ReadLine();
    if ( int.TryParse( userItem, out int itemId ) )
    {
        if ( itemId >= 0 && itemId <= stockItems.Count )
        {
            return itemId - 1;
        }
    }
    throw new Exception( $"Data error: item with id {itemId} not found" );
}

int GetUserAmount()
{
    Console.Write( "\nEnter the quantity of the selected product: " );
    var userAmount = Console.ReadLine();
    if ( int.TryParse( userAmount, out int amount ) )
    {
        if ( amount >= 0 )
        {
            return amount;
        }
    }
    throw new Exception( $"Data error: uncorrent quantity of the product - {userAmount}" );
}

bool PrintOrderConfirmation( string name, string adress, int item, int amount )
{
    int total = priceStockItems[ item ] * amount;
    string selectedItem = stockItems[ item ];
    Console.WriteLine( "Please confirm that the entered data is correct." );
    Console.WriteLine( $"Hello {name}! Did you order {selectedItem} in quantity {amount}(total: {total}) to the following address {adress}?" );
    bool isCorrect = false;
    string userAnswer;
    while ( !isCorrect )
    {
        Console.Write( "Please enter Yes or No if the data is correct. Otherwise you will be returned to the menu:" );
        userAnswer = Console.ReadLine();
        if ( userAnswer == "Yes" )
        {
            return true;
        }
        if ( userAnswer == "No" )
        {
            Console.WriteLine( "Your order has been deleted." );
            return false;
        }
    }
    return false;

}

void PrintOrderInformation( string name, string adress, int item, int amount )
{
    int total = priceStockItems[ item ] * amount;
    string selectedItem = stockItems[ item ];
    DateTime dt = DateTime.Today.AddDays( deliveryTime );
    Console.WriteLine( $"{name}, your order {selectedItem} in the amount of {amount} (total: {total}) will be delivered to the following address: {adress}." );
    Console.WriteLine( $"Delivery is scheduled for {dt.ToLongDateString()}.\nPayment in cash.\nThank you for your purchase and trust in the store \"magazin na divane\"" );
}

void AdminAuth()
{
    Console.WriteLine( "Enter password to log in to the product administration system: " );
    string userPassword = Console.ReadLine();
    if ( userPassword != adminPassword )
    {
        Console.WriteLine( "Wrong password!" );
        return;
    }
    OpenAdminPanel();
}

void OpenAdminPanel()
{
    PrintAdminMenu();
    AdminOperation operation = ReadAdminOperation();
    if ( operation == AdminOperation.Null )
    {
        throw new Exception( $"Unknown operation error: {operation}" );
    }
    HandleAdminOperations( operation );
}

static void PrintAdminMenu()
{
    Console.WriteLine( adminMenu );
}

static AdminOperation ReadAdminOperation()
{
    string userOperation = Console.ReadLine();
    return ( Enum.TryParse( userOperation, out AdminOperation operation ) ) ? operation : AdminOperation.Null;
}
void HandleAdminOperations( AdminOperation operation )
{
    switch ( operation )
    {
        case AdminOperation.Initial:
            return;
        case AdminOperation.Add:
            AddItem();
            break;
        case AdminOperation.Delete:
            DeleteItem();
            break;
        case AdminOperation.Update:
            UpdateItem();
            break;
        default:
            throw new Exception( $"Unknown admin operator: {operation}" );
    }
}

void AddItem()
{
    Console.Write( "Enter the name of the new product: " );
    var itemName = Console.ReadLine();
    if ( itemName == "" ) throw new Exception( "Missing item name" );
    Console.Write( $"Enter the price of the product {itemName}: " );
    var itemPrice = Console.ReadLine();
    if ( int.TryParse( itemPrice, out int price ) )
    {
        stockItems.Add( itemName );
        priceStockItems.Add( price );
        Console.WriteLine( $"Product {itemName} successfully added" );
    }
    else
    {
        throw new Exception( $"Invalid price: {itemPrice}" );
    }
}

void DeleteItem()
{
    GetStockItems();
    Console.WriteLine( "Enter the number of the item to be removed" );
    var userItemId = Console.ReadLine();
    if ( int.TryParse( userItemId, out int itemId ) )
    {
        itemId -= 1;
        if ( itemId >= 0 && itemId < stockItems.Count )
        {
            Console.WriteLine( $"Product {stockItems[ itemId ]} succefully deleted" );
            stockItems.RemoveAt( itemId );
            priceStockItems.RemoveAt( itemId );
            return;
        }
        else
        {
            throw new Exception( $"index out of range: {itemId}" );
        }
    }
    throw new Exception( $"Invalid item id: {userItemId}" );
}

void UpdateItem()
{
    GetStockItems();
    Console.WriteLine( "Enter the number of the item to be updated" );
    var userItemId = Console.ReadLine();
    if ( int.TryParse( userItemId, out int itemId ) )
    {
        itemId -= 1;
        if ( itemId >= 0 && itemId < stockItems.Count )
        {
            string oldName = stockItems[ itemId ];
            int oldPrice = priceStockItems[ itemId ];
            string newName = "";
            int newPrice = 0;
            CheckCorrectData( oldName, oldPrice, ref newName, ref newPrice );
            stockItems[ itemId ] = newName;
            priceStockItems[ itemId ] = newPrice;
            Console.WriteLine( $"Product succefully update: {itemId + 1}){newName} - {newPrice}$" );
            return;
        }
        else
        {
            throw new Exception( $"index out of range: {itemId}" );
        }
    }
    throw new Exception( $"Invalid item id: {userItemId}" );
}

void CheckCorrectData( string oldName, int oldPrice, ref string newName, ref int newPrice )
{
    Console.Write( "Enter new product name (empty line = keep old): " );
    newName = Console.ReadLine();
    newName = ( newName == "" ) ? oldName : newName;
    Console.Write( "Enter new product price (empty line = keep old): " );
    var userPrice = Console.ReadLine();
    if ( !int.TryParse( userPrice, out newPrice ) )
    {
        if ( userPrice == "" )
        {
            newPrice = oldPrice;
        }
        else
        {
            throw new Exception( $"Invalid admin-price: {userPrice}" );
        }

    }
}