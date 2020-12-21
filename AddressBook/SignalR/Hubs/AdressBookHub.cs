using AddressBook.Repository.RepositoryModels;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace AddressBook.Application.SignalR.Hubs
{
    public class AdressBookHub : Hub<IAddressBookClient>
    {
    }
}
