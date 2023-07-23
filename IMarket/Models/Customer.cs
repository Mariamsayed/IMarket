﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMarket.Models.Db;

namespace IMarket.Models
{
    public class Customer :BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone  { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}