using AddressBook.Application.SignalR.Hubs;
using AddressBook.Repository.RepositoryModels;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AddressBook.Application.SignalR.Notifiers
{
    public class AddressBookHubNotifier : IAddressBookHubNotifier
    {
        private readonly IHubContext<AdressBookHub, IAddressBookClient> _adressBookHubContext;

        public AddressBookHubNotifier(IHubContext<AdressBookHub, IAddressBookClient> adressBookHubContext)
        {
            _adressBookHubContext = adressBookHubContext;
        }

        public Task ContactDeleted(int contactId)
        {
            return _adressBookHubContext.Clients.All.ContactDeleted(contactId);
        }

        public Task ContactUpdated(ContactEntry contactEntry)
        {
            return _adressBookHubContext.Clients.All.ContactUpdated(contactEntry);
        }

        public Task ContactInserted(ContactEntry contactEntry)
        {
            return _adressBookHubContext.Clients.All.ContactInserted(contactEntry);
        }
    }
}
