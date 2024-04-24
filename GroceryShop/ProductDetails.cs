using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryShop
{
    public class ProductDetails
    {
        //properties
        private static int s_productID=2000;
        public  string  ProductID{get;set;}
        public string ProductName{get;set;}
        public int QuantityAvailable{get;set;}
        public double PricePerQuantity{get;set;}

        //constructors
        public ProductDetails(string productName,int quantityAvailable,double pricePerQuantity){
            ProductID="PID"+(++s_productID);
            ProductName=productName;
            QuantityAvailable=quantityAvailable;
            PricePerQuantity=pricePerQuantity;

        }

        //methods
        public void ShowProductList(){
            System.Console.WriteLine($"Product ID : {ProductID} |  ProductName : {ProductName} |  QuantityAvailable : {QuantityAvailable} |  PricePerQuantity : {PricePerQuantity}");
        }
    }
}