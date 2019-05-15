using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Ado.Models.Entitys
{
   public class Approved
    {
        public int OfferId { get; set; }
        public string UserId { get; set; }
        public int StatusId { get; set; }
        public string OfferName { get; set; }
        public string UserName { get; set; }
        public string StatusName { get; set; }
        public DateTime DateApproved { get; set; }
    }
}
