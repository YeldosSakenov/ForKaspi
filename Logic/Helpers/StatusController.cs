using Abstractions.Interfaces.Helpers;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Logic.Helpers
{
  public  class StatusController: IStatusController
    {
        IdeaBankBaseEntities db;
        public StatusController()
        {
            db = new IdeaBankBaseEntities();
        }

        public bool CheckStatus(int offerId)
        {           
            return  (db.Offers.ToList().FindAll(r => r.Id == offerId).FirstOrDefault().Approveds.Count() == db.AspNetRoles.ToList().FindAll(r => r.Name == ConfigurationManager.AppSettings["approvedRole"]).FirstOrDefault().AspNetUsers.Count());
        }
    }
}
