using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IMarket.Models.Db;

namespace IMarket.Models
{
    public class Request:BaseEntity
    {
        public int? ProductId { get; set; }

        public int? CustomerId { get; set; }
        public string Msg { get; set; }
    }
}