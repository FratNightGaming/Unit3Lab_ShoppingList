using System.Collections;
using System.Linq;

namespace ShoppingList
{
    internal class Program
    {
        public static Dictionary<string, decimal> items = new Dictionary<string, decimal>() 
        {{"Milk", 4.99m}, {"Cheese", 2.99m }, { "Steak", 29.45m },  { "Sausage", 8.77m }, { "Ice cream", 5.88m }, { "Banana", .89m }, { "Salad", 12.21m }, { "Vodka", 10.33m } };

        public static List<string> userPurchases = new List<string>();
        public static List<decimal> prices = new List<decimal>();

        public static string itemEntered = string.Empty;

        public static decimal totalCost = 0;
        
        static void Main(string[] args)
        {
            Introduction();
        }

        public static void Introduction()
        {
            Console.WriteLine("Welcome to GOOGLE SHOP!");
            Console.WriteLine("Please browse through the items below:");
            Console.WriteLine("-----------------------------");

            DisplayItemsByPrice();

            ItemEntry();
        }

        public static void ItemEntry()
        {
            while (true)
            {
                Console.WriteLine("\nPlease enter an item name or enter \"Items\" to display items by price:\n");
                itemEntered = Console.ReadLine().ToLower().Trim();
                itemEntered = WordUpperCaser(itemEntered);// this works but item.Key - a string - does not work

                if (items.ContainsKey(itemEntered))
                {
                    AddToCart();
                    break;
                }

                else if (itemEntered == "Items")
                {
                    DisplayItemsByPrice();
                    continue; //unnecessary but helps other coders visualize intent
                }

                else
                {
                    Console.WriteLine($"\n\"{itemEntered}\" not found. Try again.\n");
                    DisplayItemsByPrice();
                    Console.WriteLine();
                    continue; //unnecessary but helps other coders visualize intent
                }
            }
        }

        public static void AddToCart()
        {
            Console.WriteLine($"\n{itemEntered} found. {itemEntered} will cost ${items[itemEntered]}. Would you like to add item to cart? \"Y\" or \"N\"");
            string confirmItem = Console.ReadLine().ToUpper();

            if (confirmItem == "Y" || confirmItem == "YES")
            {
                userPurchases.Add(itemEntered);
                prices.Add(items[itemEntered]);

                Console.WriteLine($"\n{itemEntered} (${items[itemEntered]}) confirmed. Your current amount owed is {CurrentTotal()}. ");

                Console.WriteLine("Would you like to purchase another item? \"Y\" or \"N\"\n");

                string purchaseAdditionalItem = Console.ReadLine().ToUpper();

                if (purchaseAdditionalItem == "Y")
                {
                    ItemEntry();
                }

                else if (purchaseAdditionalItem == "N")
                {
                    Console.WriteLine();
                    DisplayOrderSummary();
                    return;
                }

                else
                {
                    Console.WriteLine("Invalid response:");
                    ItemEntry();
                }
            }

            else
            {
                ItemEntry();
            }
        }
        
        public static void DisplayItemsByPrice()//can I have one function that takes a parameter, then sorts by the parameter?
        {
            /*foreach (var item in items)
            {
                item.Key = WordUpperCaser(item.Key); //why wont this work
            }*/
            
            Console.WriteLine();

            foreach (KeyValuePair<string, decimal> kvp in items.OrderBy(x => x.Value))
            {
                //Console.WriteLine(string.Format("Item: {0, 10} | {1, 10} ", kvp.Key, "$"+ kvp.Value));//option 1 formatting
                Console.WriteLine($"Item: {kvp.Key.PadRight(10)}| ${kvp.Value}");//option 2 formatting

                //Console.WriteLine($"Item: {good, -10} {items[good], 10}");//line was used when I created List<string> goods
            }
        }

        public static void DisplayItemsByItem()
        {

            foreach (KeyValuePair<string, decimal> kvp in items.OrderBy(x => x.Key))
            {
                Console.WriteLine($"\nItem: {kvp.Key.PadRight(10)}| ${kvp.Value}");//
            }
        }

        public static void DisplayOrderSummary()
        {
            Console.WriteLine("\nHere is your order summary:\n");

            userPurchases.Sort();//sorts purchases alphabetically
            items.OrderBy(x => x.Key);//sorts prices by cost

            for (int i = 0; i < userPurchases.Count; i++)
            {
                Console.WriteLine($"{userPurchases[i], -10} \t {items[userPurchases[i]], 10}");//formatting is difficult in console apps...
            }

            Console.WriteLine($"\nYour total cost today is ${totalCost}.\nThank you for shopping with us.\nHave a great day!");
        }

        public static decimal CurrentTotal()
        {
            totalCost = 0;//necessary to reset totalCost to 0 when function is called
            
            foreach (decimal purchase in prices)
            {
                totalCost += purchase;
            }

            return totalCost;
        }
        
        public static string WordUpperCaser(string item)
        {
            char [] word = item.ToCharArray();
            word[0] = char.ToUpper(word[0]);
            return string.Join("", word);
        }

    }
}

/*Task: Make a shopping list application which uses collections to store your items. (You will be using two collections, one for the menu and one for the shopping list.)

What will the application do?
Display at least 8 item names and prices.
Ask the user to enter an item name
If that item exists, display that item and price and add that item to the user’s order.
If that item doesn’t exist, display an error and re-prompt the user.  (Display the menu again if you’d like.)
After adding one to their order, ask if they want to add another. If so, repeat.  (User can enter an item more than once; we’re not keeping track of quantity at this point.)
When they’re done adding items, display a list of all items ordered with prices in columns.
Display the sum price of the items ordered.

Build Specifications/Grading Points
Application uses a Dictionary<string, decimal> to keep track of the menu of items.  You can code it with literals. (2 points for instantiation & initialization)
Use a List<string> for the shopping list and store the name of the items the customer ordered.
Application takes item name input and:
Responds if that item doesn’t exist (1 point)
Adds its name to the relevant List if it does (1 point)
Application asks user if they want to quit or continue, and loops appropriately (1 point)
Application displays list of items with their prices (2 points)
Application displays the correct total predict for the list (1 point)
To determine the sum: Loop through the shopping list’s List collection, obtain the name, and look up the name’s price in the Menu Dictionary.

Extended Challenges:
Implement a menu system so the user can enter just a letter or number for the item.
Display the most and least expensive item ordered.
Display the items ordered in order of price.
*/