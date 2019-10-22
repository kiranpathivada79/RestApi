using System;
using System.Linq;
using System.Web.Http;
using CRUDAPI.Models;

namespace CRUDAPI.Controllers
{
    [RoutePrefix("Api/Contact")]
    public class ContactAPIController : ApiController
    {
        contactsEntities objeEntity = new contactsEntities();
        //WebApiDbEntities objEntity = new WebApiDbEntities();
        
        [HttpGet]
        [Route("AllContactDetails")]
        public IQueryable<Contact> GetContacts()
        {
            try
            {
                return objeEntity.Contacts;
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetContactDetailsById/{contactid}")]
        public IHttpActionResult GetContactById(string contactId)
        {
            Contact objEmp = new Contact();
            int ID = Convert.ToInt32(contactId);
            try
            {
                 objEmp = objeEntity.Contacts.Find(ID);
                if (objEmp == null)
                {
                    return NotFound();
                }

            }
            catch (Exception)
            {
                throw;
            }
           
            return Ok(objEmp);
        }

        [HttpPost]
        [Route("InsertContactDetails")]
        public IHttpActionResult PostContact(Contact data)
        {
            
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            try
            {
                objeEntity.Contacts.Add(data);
                objeEntity.SaveChanges();
            }
            catch(Exception)
            {
                throw;
            }



            return Ok(data);
        }
        
        [HttpPut]
        [Route("UpdateContactDetails")]
        public IHttpActionResult PutContactMaster(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Contact objEmp = new Contact();
                objEmp = objeEntity.Contacts.Find(contact.ContactId);
                if (objEmp != null)
                {
                    objEmp.FirstName = contact.FirstName;
                    objEmp.LastName = contact.LastName;
                    objEmp.MiddleName = contact.MiddleName;
                    objEmp.Street = contact.Street;
                    objEmp.State = contact.State;
                    objEmp.Zip = contact.Zip;
                    objEmp.CratedDate = contact.CratedDate;
                }
                int i = this.objeEntity.SaveChanges();

            }
            catch(Exception)
            {
                throw;
            }
            return Ok(contact);
        }
        [HttpDelete]
        [Route("DeleteContactDetails")]
        public IHttpActionResult DeleteContactDelete(int id)
        {
            //int empId = Convert.ToInt32(id);
            Contact emaployee = objeEntity.Contacts.Find(id);
            if (emaployee == null)
            {
                return NotFound();
            }

            objeEntity.Contacts.Remove(emaployee);
            objeEntity.SaveChanges();

            return Ok(emaployee);
        }
    }
}
