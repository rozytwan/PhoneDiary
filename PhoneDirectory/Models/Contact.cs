//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System.ComponentModel.DataAnnotations;
namespace PhoneDirectory.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Contact
    {
        public int ContactId { get; set; }
        [Required]
        public string ContactName { get; set; }
        [Required]
        public string ContactNo1 { get; set; }
        public string ContactNo2 { get; set; }
     public int CountryId { get; set; }
        public int StateId { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public string UserId { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Country Country { get; set; }
        public virtual State State { get; set; }

        public List<Contact> contactList { get; set; }
    }
}
