using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BonitaOrganisationData
{
    

    class UserInformation
    {
        public string id {get; set;}
        public string userName {get; set;}
        public string password {get; set;}
        public string icon {get; set;}
        public string firstName {get; set;}
        public string lastName {get; set;}
        public string title {get; set;}
        public string jobTitle {get; set;}
        public string managerId {get; set;}
        public int enabled { get; set; }
        //Professional data
        public string address {get; set;}
        public string country {get; set;}
        public string zipCode {get; set;}
        public string state {get; set;}
        public string email {get; set;}
        public string phoneNumber {get; set;}
        public string mobileNumber {get; set;}

        //Personal data
        public string addressPersonal { get; set; }
        public string countryPersonal { get; set; }
        public string zipCodePersonal { get; set; }
        public string statePersonal { get; set; }
        public string emailPersonal { get; set; }
        public string phoneNumberPersonal { get; set; }
        public string mobileNumberPersonal { get; set; }

        public DateTime? creationDate { get; set; }
        public DateTime? updateDate { get; set; }
        public int assignedBy { get; set; }

        public void displayData()
        {
            Console.Write(id + "	");
            Console.Write(userName + "	");
            Console.Write(password + "	");
            Console.Write(icon + "	");
            Console.Write(firstName + "	");
            Console.Write(lastName + "	");
            Console.Write(title + "	");
            Console.Write(jobTitle + "	");
            Console.Write(managerId + "	");
            Console.Write(address + "	");
            Console.Write(country + "	");
            Console.Write(zipCode + "	");
            Console.Write(state + "	");
            Console.Write(email + "	");
            Console.Write(phoneNumber + "	");
            Console.Write(mobileNumber + "	");
            Console.Write(addressPersonal + "	");
            Console.Write(countryPersonal + "	");
            Console.Write(zipCodePersonal + "	");
            Console.Write(statePersonal + "	");
            Console.Write(emailPersonal + "	");
            Console.Write(phoneNumberPersonal + "	");
            Console.Write(mobileNumberPersonal + "	");
            Console.WriteLine();
        }


    }
}
