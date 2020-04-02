using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Entities
{
    public partial class Document
    {
        public string ReceiptOrg
        {
            get
            {
                return '"' + Organization.TypeOrganization.Id + '"' + ' ' + Organization.Name;
            }
        }
    }
}
