using System;
namespace ListDs;
class Program{
    public static void Main(string[] args)
    {
        CustomizedList<int> number=new CustomizedList<int>();
        number.AddingElements(12);
        number.AddingElements(14);
        number.AddingElements(4);
        number.AddingElements(54);
        number.AddingElements(100);
        CustomizedList<int> number1=new CustomizedList<int>(1);
        number1.AddingElements(13);
        number1.AddingElements(100);
        number.AddingLists(number1);
        for(int i=0;i<number.Count;i++){
            System.Console.WriteLine(number[i]);
        }
        // number.IndexOf(100);
        // number.Insert(12,3);
        // number.Contains(1000);
        // number.RemoveAt(1);
        // number.Remove(100);

        // System.Console.WriteLine("For  each loop");
        // foreach(int value in  number){
        //     System.Console.WriteLine(value);
        // }

        // number.Reverse();
        // foreach(int value in  number){
        //     System.Console.WriteLine(value);
        // }
        //number.InsertRange(number1,2);
        number.Sorting();

    }
}