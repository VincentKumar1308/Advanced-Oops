using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryShop
{
    public class OrderDetails
    {
        //properties
        private static int  s_orderID=4000;
        public string OrderID{get;set;}
        public string BookingID{get;set;}
        public string ProductID{get;set;}
        public int PurchaseCount{get;set;}
        public  double PriceOfOrder{get;set;}

        //constructors
        public OrderDetails(string bookingID,string productID,int purchaseCount,double  priceOfOrder){
            OrderID="OID"+(++s_orderID);
            BookingID=bookingID;
            ProductID=productID;
            PurchaseCount=purchaseCount;
            PriceOfOrder=priceOfOrder;
        }

    }
}