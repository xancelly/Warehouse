using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Entities
{
    public partial class Staff
    {
        public string FullnameStaff
        {
            get
            {
                return LastName + ' ' + FirstName + ' ' + MiddleName;
            }
        }
    }
}
