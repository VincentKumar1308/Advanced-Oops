using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeteriaManagement
{
    public class FoodDetails
    {
        private static int s_foodID=100;
        public string FoodID{get;set;}
        public string FoodName{get;set;}
        public double FoodPrice{get;set;}
        public int Quantity{get;set;}

        //constructors
        public FoodDetails(string foodName,double price,int quantity){
            FoodID="FID"+(++s_foodID);
            FoodName=foodName;
            FoodPrice=price;
            Quantity=quantity;
        }
        
    }
}