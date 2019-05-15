using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.Interfaces.Helpers
{
   public interface IStatusController
    {
        bool CheckStatus(int offerId);
    }
}
