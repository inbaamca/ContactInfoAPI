using ContactInfoAPP.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ContactInfoAPP.Controllers
{
    public class ContactInfoController : Controller
    {
        string baseUri = "http://localhost:38939/";
        public async Task<IActionResult> Index()
        {
            List<ContactInfoViewmodel> contactInfo = await GetContacts();
            if (contactInfo == null) { return View(new List<ContactInfoViewmodel>()); }
            return View(contactInfo);
        }
        public async Task<ActionResult> AddEdit(long id)
        {
            ContactInfoViewmodel contactInfo = new ContactInfoViewmodel();
            HttpClient client = new HttpClient();
            HttpResponseMessage res = null;
            try
            {
                if (id > 0)
                {
                    client.BaseAddress = new Uri(baseUri);
                    res = await client.GetAsync("api/Contact/GetContact?id=" + id.ToString());
                    if (res.IsSuccessStatusCode)
                    {
                        var result = res.Content.ReadAsStringAsync().Result;
                        contactInfo = JsonConvert.DeserializeObject<ContactInfoViewmodel>(result);
                    }
                }
                return View(contactInfo);
            }
            finally { contactInfo = null; client = null; res = null; }
        }

        public async Task<IActionResult> AddUpdateContact(ContactInfoViewmodel contactInfo)
        {
            List<ContactInfoViewmodel> contactList = null;
            HttpClient client = new HttpClient();
            HttpResponseMessage res = null;
            try
            {
                string content = JsonConvert.SerializeObject(contactInfo);
                var strContent = new StringContent(content, Encoding.UTF8, "application/json");
                res = await client.PostAsync(baseUri + "api/Contact/AddUpdateContact", strContent);
                if (res.IsSuccessStatusCode)
                {
                    ViewBag.SuccessMsg = "Add / Modified Successfully";
                }
                contactList = await GetContacts();
                return View("Index", contactList);
            }
            finally { contactList = null; client = null; res = null; }
        }
        public async Task<ActionResult> DeleteContact(long id)
        {
            List<ContactInfoViewmodel> contactList = null;
            HttpClient client = new HttpClient();
            HttpResponseMessage res = null;
            try
            {
                string content = JsonConvert.SerializeObject(id.ToString());
                var Id = new StringContent(content, Encoding.UTF8, "application/json");
                res = await client.PostAsync(baseUri + "api/Contact/DeleteContact", Id);
                if (res.IsSuccessStatusCode)
                {
                    ViewBag.SuccessMsg = "Deleted Successfully";
                }
                contactList = await GetContacts();
                return View("Index", contactList);
            }
            finally { contactList = null; client = null; res = null; }
        }
        public ActionResult MapView(string id)
        {
            ViewBag.Pincode = id;
            return View();
        }
        private async Task<List<ContactInfoViewmodel>> GetContacts()
        {
            List<ContactInfoViewmodel> contactInfo = null;
            HttpClient client = new HttpClient();
            HttpResponseMessage res = null;
            try
            {
                client.BaseAddress = new Uri(baseUri);
                res = await client.GetAsync("api/Contact/GetContactList");
                if (res.IsSuccessStatusCode)
                {
                    var result = res.Content.ReadAsStringAsync().Result;
                    contactInfo = JsonConvert.DeserializeObject<List<ContactInfoViewmodel>>(result);
                }
                return contactInfo.OrderByDescending(exp => exp.Id).ToList();
            }
            finally { contactInfo = null; client = null; res = null; }
        }
    }
}
