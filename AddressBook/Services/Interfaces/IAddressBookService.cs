using AddressBook.Repository.RepositoryModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressBook.Application.Services.Interfaces
{
    public interface IAddressBookService
    {
        /// <summary>
        /// Gets a single contact based on the provided id
        /// </summary>
        Task<ContactEntry> GetContact(int contactId);

        /// <summary>
        /// Returns the contacts that contain the name and address string.
        /// Returns only 1 result page. Page size is 5
        /// </summary>
        Task<List<ContactEntry>> GetContacts(string name, string address, int pageNumber);

        /// <summary>
        /// Inserts a new contact into the database. Id field is ingored on input.
        /// Sends a notification about the action to connected clients
        /// </summary>
        /// <returns> Database id of the inserted row</returns>
        Task<int> InsertContact(ContactEntry contact);

        /// <summary>
        /// Updates the contact based on the Id field of the sent contact
        ///ends a notification about the action to connected clients
        /// </summary>
        Task<ContactEntry> UpdateContact(ContactEntry contact);

        /// <summary>
        /// Deletes the contact with provided id. 
        /// If the contact with that id doesn't exists the action will succeed
        /// Sends a notification about the action to connected clients
        /// </summary>
        Task DeleteContact(int contactId);

        /// <summary>
        /// Gets a pge of the adress book. Contacts are sorted by name and adress. Page size is 5.
        /// </summary>
        Task<List<ContactEntry>> GetAdressBookPage(int pageNumber);
    }
}