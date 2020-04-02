using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Entities
{
    public partial class Document
    {
        public string SenderWar
        {
            get
            {
                return "обл. " + Warehouse1.Address.Region + " г. " + Warehouse1.Address.City + " ул. " + Warehouse1.Address.Street + " д. " + Warehouse1.Address.House;
            }
        }
    }
}
