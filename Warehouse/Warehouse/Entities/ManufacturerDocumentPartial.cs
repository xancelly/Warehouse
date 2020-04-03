using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Entities
{
    public partial class DocumentGood
    {
        public string DocumentMan
        {
            get
            {
                return '"' + Good.Manufacturer.TypeOrganization.Id + '"' + ' ' + Good.Manufacturer.Name;
            }
        }
    }
}
