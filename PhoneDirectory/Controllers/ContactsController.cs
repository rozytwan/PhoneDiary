using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhoneDirectory.Models;

namespace PhoneDirectory.Controllers
{
    public class ContactsController : Controller
    {
        private Entities1 db = new Entities1();

        // GET: Contacts
        public ActionResult Index()
        {
            var contacts = db.Contacts.Include(c => c.AspNetUser).Include(c => c.Country).Include(c => c.State);
            return View(contacts.ToList());
        }

        // GET: Contacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // GET: Contacts/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName");
            ViewBag.StateId = new SelectList(db.States, "StateId", "StateName");
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ContactId,ContactName,ContactNo1,ContactNo2,CountryId,StateId,Address,Image,UserId")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", contact.UserId);
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName", contact.CountryId);
            ViewBag.StateId = new SelectList(db.States, "StateId", "StateName", contact.StateId);
            return View(contact);
        }

        // GET: Contacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", contact.UserId);
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName", contact.CountryId);
            ViewBag.StateId = new SelectList(db.States, "StateId", "StateName", contact.StateId);
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ContactId,ContactName,ContactNo1,ContactNo2,CountryId,StateId,Address,Image,UserId")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", contact.UserId);
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName", contact.CountryId);
            ViewBag.StateId = new SelectList(db.States, "StateId", "StateName", contact.StateId);
            return View(contact);
        }

        // GET: Contacts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = db.Contacts.Find(id);
            db.Contacts.Remove(contact);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
