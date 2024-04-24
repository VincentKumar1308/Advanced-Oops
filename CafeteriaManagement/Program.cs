using System;
namespace CafeteriaManagement;
class Program{
    public static void Main(string[] args)
    {
        Operation.LoadDefaultData();
        Operation.MainMenu();
    }
}