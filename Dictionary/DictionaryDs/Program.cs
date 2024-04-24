using System;
using System.Collections.Generic;
namespace DictionaryDs;
class Program{
    public static void Main(string[] args)
    {
        CustomizedDictionary<int,string> name=new CustomizedDictionary<int,string>();
        name.Add(12,"vicnetn");
        name.Add(15,"sudha");
        name.Add(14,"sfdas");
        foreach(KeyValue<int,string> data in name){
            System.Console.WriteLine("Key : "+data.Key +" Value is : "+data.Value);
        }
        string Name=name[12];
        System.Console.WriteLine("name is  : "+Name);
        name[15]="vincentsudha";
      
        
    }
}