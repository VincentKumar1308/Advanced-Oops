using System;
namespace GroceryShop;
class Program{
    public static void Main(string[] args)
    {
        Operation.LoadDefaultData();
        Operation.MainMenu();
    }
}