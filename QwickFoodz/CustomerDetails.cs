using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QwickFoodz
{
    public class CustomerDetails:PersonalDetials,IBalance
    {
        //fields
        private int _balance;
        private static int s_customerID=1000;
        //properties
        public string CustomerID{get;set;}
        public double walletBalance{get;set;}
        

        //constructors
        public CustomerDetails(double balance,string name,string fatherName,Gender gender,string mobileNo,DateTime dob,string mailID,string location):base(name,fatherName,gender,mobileNo,dob,mailID,location){
            CustomerID="CID"+(++s_customerID);
            walletBalance=balance;
            
        
        }

        //methods
        public  void WalletRecharge(double money){
         
            walletBalance=walletBalance+money;
        }
        public void DeductBalance(double money){
            if(walletBalance>money){
                walletBalance=walletBalance-money;
            }
            else{
                System.Console.WriteLine("insufficiennt balance");
            }
        }
    }
}