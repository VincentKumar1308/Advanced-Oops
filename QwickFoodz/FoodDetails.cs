using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QwickFoodz
{
    public class FoodDetails
    {
        //fields
        private static int s_foodID=2000;
        //properties
        public string FoodID{get;set;}
        public string FoodName{get;set;}
        public double PricePerQuantity{get;set;}
        public  int QuantityAvailable{get;set;}

        //constructors
        public  FoodDetails(string foodName,double pricePerQuantity,int quantityAvailable){
            FoodID="FID"+(++s_foodID);
            FoodName=foodName;
            PricePerQuantity=pricePerQuantity;
            QuantityAvailable=quantityAvailable;
        }
        
    }
}