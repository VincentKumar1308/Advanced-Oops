using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CafeteriaManagement
{
    public enum Gender{Male,Female,Others}
    public class PersonalDetails
    {
        //properties
        public string UserName{get;set;}
        public string FatherName{get;set;}
        public long MobileNumber{get;set;}
        public string MailID{get;set;}
        public Gender Gender;
   
        //constructors
        public PersonalDetails(string userName,string fatherName,long phoneNo,string mailID,Gender gender){
          
            UserName=userName;
            FatherName=fatherName;
            MobileNumber=phoneNo;
            MailID=mailID;
            Gender=gender;
        
        }
    }
}