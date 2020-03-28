using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Entities
{
    public partial class Organization
    {
        public string FullnameDirector
        {
            get
            {
                return LastName + ' ' + FirstName + ' ' + MiddleName;
            }
        }
    }
}
