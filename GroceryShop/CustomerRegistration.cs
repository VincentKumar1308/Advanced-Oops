using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryShop
{
    public class CustomerRegistration:PersonalDetails,IBalance
    {
        private static int s_customerID=1000;
        public string CustomerID{get;set;}
        public double WalletBalance{get;set;}

        //constructors
        public CustomerRegistration(double balance,string name,string fatherName,Gender gender,long mobileNo,DateTime dob,string mailID):base( name,fatherName, gender,mobileNo,dob,mailID){
           
            CustomerID="CID"+(++s_customerID);
             WalletBalance=balance;

        }

        //methods
        public void WalletRecharge(double money){
        WalletBalance=money+WalletBalance;
        }

        
    }
}