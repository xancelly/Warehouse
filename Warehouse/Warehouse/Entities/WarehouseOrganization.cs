//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Warehouse.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class WarehouseOrganization
    {
        public int Id { get; set; }
        public Nullable<int> IdWarehouse { get; set; }
        public Nullable<int> IdOrganization { get; set; }
    
        public virtual Organization Organization { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}