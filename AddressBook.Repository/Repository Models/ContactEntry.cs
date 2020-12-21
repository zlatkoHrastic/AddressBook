using System;
using System.Collections.Generic;

namespace AddressBook.Repository.RepositoryModels
{
    public class ContactEntry
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public List<string> TelephoneNumbers { get; set; }
    }
}
