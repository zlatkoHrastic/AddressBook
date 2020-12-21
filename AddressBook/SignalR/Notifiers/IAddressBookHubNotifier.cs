using AddressBook.Repository.RepositoryModels;
using System.Threading.Tasks;

namespace AddressBook.Application.SignalR.Notifiers
{
    public interface IAddressBookHubNotifier
    {
        Task ContactDeleted(int contactId);
        Task ContactInserted(ContactEntry contactEntry);
        Task ContactUpdated(ContactEntry contactEntry);
    }
}