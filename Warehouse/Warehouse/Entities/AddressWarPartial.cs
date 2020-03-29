using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Entities
{
    public partial class Warehouse
    {
        public string AddressWar
        {
            get
            {
                return "обл. " + Address.Region + " г. " + Address.City + " ул. " + Address.Street + " д. " + Address.House;
            }
        }
    }
}
