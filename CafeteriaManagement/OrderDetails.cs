using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeteriaManagement
{
    public enum OrderStatus{Default,Initiated,Ordered,Cancelled}
    public class OrderDetails
    {
        public static int s_orderID=1000;
        public string OrderID{get;set;}
        public string UserID{get;set;}
        public DateTime  OrderDate{get;set;}
        public double TotalPrice{get;set;}
        public  OrderStatus OrderStatus;

        //cosntructors
        public OrderDetails(string userID,DateTime orderDate,double totalPrice,OrderStatus status){
          OrderID="OID"+(++s_orderID);
          UserID=userID;
          OrderDate=orderDate;
          TotalPrice=totalPrice;
          OrderStatus=status;

        }

    }
}