using AddressBook.Application.Services.Interfaces;
using AddressBook.Repository.RepositoryModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AddressBook.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressBookController : ControllerBase
    {
        private readonly ILogger<AddressBookController> _logger;
        private readonly IAddressBookService _addressBookService;

        public AddressBookController(ILogger<AddressBookController> logger, IAddressBookService addressBookService)
        {
            _logger = logger;
            _addressBookService = addressBookService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var contact = await _addressBookService.GetContact(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpGet("{name}/{address}/{pageNumber}")]
        public async Task<IActionResult> GetByName(string name, string address, int pageNumber)
        {
            if (pageNumber < 1)
            {
                return BadRequest("Invalid page number");
            }

            var searchResult = await _addressBookService.GetContacts(name, address, pageNumber);

            return Ok(searchResult);
        }

        [HttpGet("page/{pageNumber}")]
        public async Task<IActionResult> GetPage(int pageNumber)
        {
            if (pageNumber < 1)
            {
                return BadRequest("Invalid page number");
            }
            var page = await _addressBookService.GetAdressBookPage(pageNumber);

            return Ok(page);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContactEntry contactEntry)
        {
            var id = await _addressBookService.InsertContact(contactEntry);
            return CreatedAtAction(nameof(Get), new { id }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ContactEntry contactEntry)
        {
            if (id != contactEntry.Id)
            {
                return BadRequest("Ids don't match");
            }

            var updatedContact = await _addressBookService.UpdateContact(contactEntry);

            if (updatedContact == null)
            {
                return NotFound("Contact doesn't exist");
            }

            return Ok(updatedContact);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _addressBookService.DeleteContact(id);
            return NoContent();
        }

    }
}
