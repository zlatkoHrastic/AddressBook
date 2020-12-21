using AddressBook.Repository.EfModels;
using AddressBook.Repository.Interfaces;
using AddressBook.Repository.Mappers;
using AddressBook.Repository.RepositoryModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.Repository.Implementations
{
    public class AddressBookRepository : IAddressBookRepository
    {
        private readonly AddressBookContext _context;

        public AddressBookRepository(AddressBookContext context)
        {
            _context = context;
        }

        ///<inheritdoc/>
        public async Task<ContactEntry> GetContact(int contactId)
        {
            var dbContact = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == contactId);
            if (dbContact == null)
            {
                return null;
            }

            return dbContact.GetEntryObject();
        }

        ///<inheritdoc/>
        public async Task<List<ContactEntry>> GetContacts(string name, string address, int pageNumber, int pageSize)
        {
            var numberOfContactsToSkip = getNumberOFContactsToSkip(pageNumber, pageSize);
            var dbContacts = _context.Contacts
                .Where(c => string.IsNullOrEmpty(name) || c.Name.Contains(name))
                .Where(c => string.IsNullOrEmpty(address) || c.Name.Contains(address))
                .OrderBy(c => c.Name)
                .ThenBy(c => c.Address)
                .Skip(numberOfContactsToSkip)
                .Take(pageSize);

            return await dbContacts.Select(dbContact => dbContact.GetEntryObject()).ToListAsync();
        }

        ///<inheritdoc/>
        public async Task<int> InsertContact(ContactEntry contact)
        {
            var dbContact = contact.GetDatabaseObject();
            _context.Contacts.Add(dbContact);
            await _context.SaveChangesAsync();

            return dbContact.Id;
        }

        ///<inheritdoc/>
        public async Task<ContactEntry> UpdateContact(ContactEntry contact)
        {
            var dbContact = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == contact.Id);

            if (dbContact == null)
            {
                throw new ArgumentException("Contact doesn't exist");
            }

            dbContact.Address = contact.Address;
            dbContact.Name = contact.Name;
            dbContact.Address = contact.Address;
            dbContact.DateOfBirth = contact.DateOfBirth;
            dbContact.TelephoneNumbers = contact.TelephoneNumbers.ToArray();

            await _context.SaveChangesAsync();

            return dbContact.GetEntryObject();
        }

        ///<inheritdoc/>
        public async Task DeleteContact(int contactId)
        {
            var dbContact = await _context.Contacts.FirstOrDefaultAsync(c => c.Id == contactId);
            if (dbContact == null)
            {
                return;
            }

            _context.Contacts.Remove(dbContact);
            await _context.SaveChangesAsync();
        }

        ///<inheritdoc/>
        public async Task<List<ContactEntry>> GetAdressBookPage(int pageNumber, int pageSize)
        {
            var numberOfContactsToSkip = getNumberOFContactsToSkip(pageNumber, pageSize);
            var dbContacts = _context.Contacts.OrderBy(c => c.Name).ThenBy(c => c.Address)
                .Skip(numberOfContactsToSkip).Take(pageSize);

            return await dbContacts.Select(dbContact => dbContact.GetEntryObject()).ToListAsync();
        }

        ///<inheritdoc/>
        private int getNumberOFContactsToSkip(int pageNumber, int pageSize)
        {
            return (pageNumber - 1) * pageSize;
        }
    }
}
