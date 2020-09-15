using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{
    public class Contact
    {
        public string Name { get; }
        public string PhoneNo { get; }
        public string EmailAddress { get; }
        public Contact(string name, string phoneNo, string emailAddress)
        {
            Name = name;
            PhoneNo = phoneNo;
            EmailAddress = emailAddress;
        }
    }
}
