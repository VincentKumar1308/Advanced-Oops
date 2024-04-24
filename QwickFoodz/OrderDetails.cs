using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QwickFoodz
{
     public enum OrderStatus{Initiated,Ordered,Cancelled}
    public class OrderDetails
    {
       
        //fields
        private static int  s_OrderID=3000;
        //properties
        public string OrderID{get;set;}
        public string CustomerID{get;set;}
        public double TotalPrice{get;set;}
        public DateTime DateOfOrder{get;set;}
        public OrderStatus OrderStatus;

        //constructors
        public  OrderDetails(string customerID,double totalPrice,DateTime orderDate,OrderStatus status){
            OrderID="OID"+(++s_OrderID);
            CustomerID=customerID;
            TotalPrice=totalPrice;
            DateOfOrder=orderDate;
            OrderStatus=status;
        }
    }
}