using System;

namespace AddressBook.Repository.EfModels
{
    public partial class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string[] TelephoneNumbers { get; set; }
    }
}
