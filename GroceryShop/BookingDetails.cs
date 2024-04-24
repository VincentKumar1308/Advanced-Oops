using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryShop
{
    public class BookingDetails
    {
        public enum BookingStatus{Initated,Booked,Cancelled}
        //properties
        private static int s_bookingID=3000;
        public string  BookingID{get;set;}
        public string CustomerID{get;set;}
        public double TotalPrice { get; set; }
        public DateTime DateOfBooking{get;set;}
        public BookingStatus bookingStatus;
    


        //constructors
        public BookingDetails(string customerID,double totalPrice,DateTime bookingDate,BookingStatus status){
            BookingID="BID"+(++s_bookingID);
            CustomerID=customerID;
            TotalPrice=totalPrice;
            DateOfBooking=bookingDate;
            bookingStatus=status;
        }
        //methods
        public void ShowBookingDetails(){
            System.Console.WriteLine($"Booking ID : { BookingID} | CustomerID : {CustomerID} |  TotalPrice : { TotalPrice} |     DateOfBooking : {  DateOfBooking} |  bookingStatus : { bookingStatus}");
        }

    }
}