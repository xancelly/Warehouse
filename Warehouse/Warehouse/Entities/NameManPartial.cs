using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Entities
{
    public partial class Manufacturer
    {
        public string NameMan
        {
            get
            {
                return '"' + TypeOrganization.Id + '"' + ' ' + Name;
            }
        }
    }
}
