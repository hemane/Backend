using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HeMaNe.Web.Database.Models
{
    public class Club
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Postcode { get; set; }
        public string City { get; set; }

        public virtual User Manager { get; set; }
    }
}
