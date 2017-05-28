using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhoneDirectory.Models;
using System.IO;
using System.Net;


namespace PhoneDirectory.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact

        Entities1 dB = new Entities1();
        Contact objContact = new Contact();
        public ActionResult Index()
        {
            objContact.contactList = dB.Contacts.ToList();

            return View("Index", objContact.contactList);
        }

        public ActionResult AddContact()
        {
            ViewBag.UserId = new SelectList(dB.AspNetUsers, "Id", "Email");
            ViewBag.CountryId = new SelectList(dB.Countries, "CountryId", "CountryName");
            ViewBag.StateId = new SelectList(dB.States, "StateId", "StateName");

            return View();
        }

        Contact objCont = new Contact();

        [HttpPost]
        public ActionResult Save(Contact objContact, HttpPostedFileBase file)
        {
            ViewBag.IsSaved = false;

            if (ModelState.IsValid)
            {

                var fileName = "";
                if (file.ContentLength > 0)
                {
                    fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                    string[] file_name = fileName.Split('.');
                    file.SaveAs(path);
                }
                objCont.ContactName = objContact.ContactName;
                objCont.ContactNo1 = objContact.ContactNo1;
                objCont.ContactNo2 = objContact.ContactNo2;
                objCont.Image = "Content/Images/" + fileName;
                objCont.Address = objContact.Address;
                objCont.CountryId = objContact.CountryId;
                objCont.StateId = objContact.StateId;
                objCont.UserId = objContact.UserId;
                dB.Contacts.Add(objCont);
                dB.SaveChanges();
                return RedirectToAction("Index");

            }
            ViewBag.UserId = new SelectList(dB.AspNetUsers, "Id", "Email", objContact.UserId);
            ViewBag.CountryId = new SelectList(dB.Countries, "CountryId", "CountryName", objContact.CountryId);
            ViewBag.StateId = new SelectList(dB.States, "StateId", "StateName", objContact.StateId);
            return View();

        }
        public ActionResult EditContact(int? id)
        {
            if (!string.IsNullOrEmpty(id.ToString()))
            {

                //objContact = dB.Contacts.Find(id);
                //ViewBag.CountryId = new SelectList(dB.Countries, "CountryId", "CountryName", objContact.CountryId);
                //ViewBag.StateId = new SelectList(dB.States, "StateId", "StateName", objContact.StateId);


                return View(objContact);
            }
            else
            {
                return View("~/Views/Shared/ErrorAdmin.cshtml");
            }
        }
        public ActionResult Edit(Contact contact, int id, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                objContact = dB.Contacts.Find(id);
                var fileName = "";
                if (file.ContentLength > 0)
                {
                    fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/Images"), fileName);
                    string[] file_name = fileName.Split('.');
                    file.SaveAs(path);
                }
                objContact.ContactName = contact.ContactName;
                objContact.ContactNo1 = contact.ContactNo1;
                objContact.ContactNo2 = contact.ContactNo2;
                objContact.Image = "Content/Images/" + fileName;
                objContact.Address = contact.Address;
                objContact.CountryId = contact.CountryId;
                objContact.StateId = contact.StateId;
                objContact.UserId = contact.UserId;
                dB.Entry(contact);
                dB.Contacts.Add(objContact);
                dB.SaveChanges();
                return RedirectToAction("Index");

            }

            ViewBag.UserId = new SelectList(dB.AspNetUsers, "Id", "Email", objContact.UserId);
            ViewBag.CountryId = new SelectList(dB.Countries, "CountryId", "CountryName", objContact.CountryId);
            ViewBag.StateId = new SelectList(dB.States, "StateId", "StateName", objContact.StateId);

            return View(objContact);

        }
        Contact cont = new Contact();
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cont = dB.Contacts.Find(id);
            if (cont == null)
            {
                return HttpNotFound();
            }
            return View(cont);
        }

    [HttpPost, ActionName("Delete")]
        public ActionResult Remove(int id)
        {
            objContact= dB.Contacts.Find(id);
            dB.Contacts.Remove(objContact);
            dB.SaveChanges();
            return RedirectToAction("Index");
        }
    
    
    public ActionResult SearchNew(string search="",string field="",bool ascending=true )
        {
            ViewBag.search = search;
            ViewBag.ascending = ascending;

            var result = dB.Contacts
    .Where(t =>
        t.ContactName.Contains(search) ||
        t.ContactNo1.Contains(search) ||
        t.ContactNo2.Contains(search) ||
        t.Address.Contains(search) ||
         t.State.StateName.Contains(search) ||
        t.Country.CountryName.Contains(search));
        
            if (field == "name")
            {
                if (ascending)
                    result = result.OrderBy(p => p.ContactName);
                else
                    result = result.OrderByDescending(p => p.ContactName);
            }
            if (field=="Country")
            {
                if (ascending)
                {
                    result = result.OrderBy(p => p.Country.CountryName);
                }
                else
                    result = result.OrderByDescending(p => p.Country.CountryName);
               
            }
            if (field=="State")
            {
                if (ascending)
                {
                    result = result.OrderBy(p => p.State.StateName);
                }
                else
                    result = result.OrderByDescending(p => p.State.StateName);
            }
            if (field == "Address")
            {
                if (ascending)
                {
                    result = result.OrderBy(p => p.Address);
                }
                else
                    result = result.OrderByDescending(p => p.Address);
            }
            return View("Index", result);
       
        }

    }
}