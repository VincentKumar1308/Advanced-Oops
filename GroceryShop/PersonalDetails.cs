using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryShop
{
    public  enum Gender{Male,Female,Others}
    public class PersonalDetails
    {
        //properties
        public string Name{get;set;}
        public string FatherName { get; set; }
        public long MobileNo { get; set; }
        public DateTime DOB { get; set; }
        public string MailID { get; set; }
        public Gender Gender;

        //constructors
        public PersonalDetails(string name,string fatherName,Gender gender,long mobileNo,DateTime dob,string mailID){
            Name=name;
            FatherName=fatherName;
            MobileNo=mobileNo;
            DOB=dob;
            MailID=mailID;
            Gender=gender;

        }
    }
}