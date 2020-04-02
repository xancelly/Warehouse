using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Entities
{
    public partial class Document
    {
        public string ReceiptWar
        {
            get
            {
                return "обл. " + Warehouse.Address.Region + " г. " + Warehouse.Address.City + " ул. " + Warehouse.Address.Street + " д. " + Warehouse.Address.House;
            }
        }
    }
}
