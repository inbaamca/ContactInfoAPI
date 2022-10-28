using ContactInfoAPI.Data;
using ContactInfoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactInfoAPI.Controllers
{
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        private Context _context;
        public ContactController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("ApiStatus")]
        public string ApiStatus()
        {
            return "Ok";
        }

        [HttpGet]
        [Route("GetContactList")]
        public List<Contact> GetContactList()
        {
            return _context.Contact.ToList();
        }

        [HttpGet]
        [Route("GetContact")]
        public Contact GetContact(long id)
        {
            return _context.Contact.Find(id);
        }

        [HttpPost]
        [Route("AddUpdateContact")]
        public bool AddUpdateContact([FromBody] Object strContent)
        {
            Contact contact = JsonConvert.DeserializeObject<Contact>(strContent.ToString());
            try
            {
                if (contact == null) { return false; }
                if (contact.Id > 0)
                {
                    _context.Contact.Update(contact);
                    _context.SaveChanges();
                }
                else
                {
                    _context.Contact.Add(contact);
                    _context.SaveChanges();
                }
                return true;
            }
            catch { return false; }
        }
        [HttpPost]
        [Route("DeleteContact")]
        public bool DeleteContact([FromBody] Object Id)
        {
            Contact contact = null;
            long contactId = 0;
            try
            {
                contactId = JsonConvert.DeserializeObject<long>(Id.ToString());
                contact = _context.Contact.Find(contactId);
                if (contact == null) { return false; }
                _context.Contact.Remove(contact);
                _context.SaveChanges();
                return true;
            }
            catch(Exception ex) { return false; }
        }
    }
}
