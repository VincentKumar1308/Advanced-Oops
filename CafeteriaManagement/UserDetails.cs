using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeteriaManagement
{
    public class UserDetails :PersonalDetails,IBalance
    {
        //properties
        private static int s_userID=1000;
        public string WorkStationNumber{get;set;}
        private double _balance=0;
        public string UserID{get;set;}
        public double WalletBalance{get {return _balance;} }

        //constructors
        public UserDetails(string userName,string fatherName,long phoneNo,string mailID,Gender gender,string workStationNumber,long balance):base( userName, fatherName, phoneNo, mailID, gender){
            UserID="SF"+(++s_userID);
            WorkStationNumber=workStationNumber;
            _balance=balance;

        }

        //methods
        public void WalletRecharge(double money){
            _balance=_balance+money;
        }
        public void DeductAmount(double money){
            if(_balance>money){
                _balance=_balance-money;
            }
            else{
                System.Console.WriteLine("Your Account not having the enough sufficient money ");
            }
        }

    }
}