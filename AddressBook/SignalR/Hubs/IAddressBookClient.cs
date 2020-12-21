using AddressBook.Repository.RepositoryModels;
using System.Threading.Tasks;

namespace AddressBook.Application.SignalR.Hubs
{
    public interface IAddressBookClient
    {
        Task ContactDeleted(int contactId);
        Task ContactUpdated(ContactEntry contactEntry);
        Task ContactInserted(ContactEntry contactEntry);
    }
}
