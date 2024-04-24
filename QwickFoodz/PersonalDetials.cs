using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QwickFoodz
{
    public enum Gender{Male,Female,Transgender}
   
    public class PersonalDetials
    {
         //enum
   
        
        //properties
        public string Name{get;set;}
        public string FatherName{get;set;}
        public Gender Gender;
        public string MoblieNo{get;set;}
        public DateTime DOB{get;set;}
        public string MailID{get;set;}
        public string Location{get;set;}

        //constructors
        public PersonalDetials(string name,string fatherName,Gender genders,string mobileNo,DateTime dob,string mailID,string location ){
            Name=name;
            FatherName=fatherName;
            Gender gender=genders;
            MoblieNo=mobileNo;
            DOB=dob;
            MailID=mailID;
            Location=location;

        }
    }
}