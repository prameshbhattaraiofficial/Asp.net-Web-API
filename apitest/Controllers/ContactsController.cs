using apitest.Data;
using apitest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apitest.Controllers
{

    [ApiController]
    [Route("api/[Controller]")]
    public class ContactsController : Controller
    {

        private readonly ContactsApiDbContext _db;

        public ContactsController(ContactsApiDbContext contactsController)
        {
            _db = contactsController;
        }

        [HttpGet]
        public async Task< IActionResult> GetEmployee()
        {
            return Ok( await _db.contacts.ToListAsync());
        }

        [HttpGet]
        [Route("{id:guid}")]

        public async Task<IActionResult> GetContent([FromRoute] Guid id)
        {

            var contact = await _db.contacts.FindAsync(id);
            if(contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }


        [HttpPost]
        public async Task< IActionResult> AddContact( AddContactRequest addContactRequest)
        {
            var contact = new Contacts()
            {
                id= Guid.NewGuid(),
                Address= addContactRequest.Address,
                Phone= addContactRequest.Phone,

            };
            await _db.contacts.AddAsync(contact);
            await _db.SaveChangesAsync();
            return Ok(contact);
        }

        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {

            var contact = await _db.contacts.FindAsync(id);
            if(contact !=null)
            {
                contact.Phone = updateContactRequest.Phone;
                contact.Address = updateContactRequest.Address; 
                return Ok(contact);
            }
            return NotFound();

        }

        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteContacts([FromRoute] Guid id)
        {
            var contact = await _db.contacts.FindAsync(id);
            if(contact != null )
            {
                _db.contacts.Remove(contact);
                await _db.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound();
        }
        
    }
}
