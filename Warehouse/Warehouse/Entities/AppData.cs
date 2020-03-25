using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Entities
{
    class AppData
    {
        public static ttlEntities Context
        {
            get; set;
        } = new ttlEntities();
    }
}
