using AddressBook.Application.Services.Interfaces;
using AddressBook.Application.SignalR.Notifiers;
using AddressBook.Repository.Interfaces;
using AddressBook.Repository.RepositoryModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AddressBook.Application.Services.Implementations
{
    public class AddressBookService : IAddressBookService
    {
        private readonly IAddressBookRepository _addressBookRepository;
        private readonly IAddressBookHubNotifier _addressBookHubNotifier;
        private readonly ILogger<AddressBookService> _logger;

        private static int _pageSize = 5;

        public AddressBookService(IAddressBookRepository addressBookRepository, IAddressBookHubNotifier addressBookHubNotifier, ILogger<AddressBookService> logger)
        {
            _addressBookRepository = addressBookRepository;
            _logger = logger;
            _addressBookHubNotifier = addressBookHubNotifier;
        }

        ///<inheritdoc/>
        public async Task<ContactEntry> GetContact(int contactId)
        {
            try
            {
                return await _addressBookRepository.GetContact(contactId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        ///<inheritdoc/>
        public async Task<List<ContactEntry>> GetContacts(string name, string address, int pageNumber)
        {
            try
            {
                return await _addressBookRepository.GetContacts(name, address, pageNumber, _pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        ///<inheritdoc/>
        public async Task<int> InsertContact(ContactEntry contact)
        {
            var insertedId = -1;
            try
            {
                insertedId = await _addressBookRepository.InsertContact(contact);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

            try
            {
                await _addressBookHubNotifier.ContactInserted(contact);
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Couldn't send signalR message. {ex.Message}");
            }

            return insertedId;
        }

        ///<inheritdoc/>
        public async Task<ContactEntry> UpdateContact(ContactEntry contact)
        {
            ContactEntry updatedContact = null;
            try
            {
                updatedContact = await _addressBookRepository.UpdateContact(contact);
            }
            catch (ArgumentException)
            {
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

            try
            {
                await _addressBookHubNotifier.ContactUpdated(updatedContact);
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Couldn't send signalR message. {ex.Message}");
            }

            return updatedContact;
        }

        ///<inheritdoc/>
        public async Task DeleteContact(int contactId)
        {
            try
            {
                await _addressBookRepository.DeleteContact(contactId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

            try
            {
                await _addressBookHubNotifier.ContactDeleted(contactId);
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Couldn't send signalR message. {ex.Message}");
            }
        }

        ///<inheritdoc/>
        public async Task<List<ContactEntry>> GetAdressBookPage(int pageNumber)
        {
            try
            {
                return await _addressBookRepository.GetAdressBookPage(pageNumber, _pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

    }
}
