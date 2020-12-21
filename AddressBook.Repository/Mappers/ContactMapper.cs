using AddressBook.Repository.EfModels;
using AddressBook.Repository.RepositoryModels;
using System.Collections.Generic;

namespace AddressBook.Repository.Mappers
{
    public static class ContactMapper
    {
        public static ContactEntry GetEntryObject(this Contact dbContact)
        {
            return new ContactEntry()
            {
                Id = dbContact.Id,
                Name = dbContact.Name,
                Address = dbContact.Address,
                DateOfBirth = dbContact.DateOfBirth,
                TelephoneNumbers = new List<string>(dbContact.TelephoneNumbers)
            };
        }

        public static Contact GetDatabaseObject(this ContactEntry contact)
        {
            return new Contact()
            {
                Name = contact.Name,
                Address = contact.Address,
                DateOfBirth = contact.DateOfBirth,
                TelephoneNumbers = contact.TelephoneNumbers.ToArray()
            };
        }
    }
}
