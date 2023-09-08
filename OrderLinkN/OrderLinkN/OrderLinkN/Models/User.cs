
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderLinkN.Models
{
    [Table("Users")]
    public class User: IEntityBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CompanyName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Address { get; set; } 
        public string State { get; set; } 
        public string Country { get; set; } 
        public string AreaCode { get; set; } 
        public string GSTNumber { get; set; }
    }
}
