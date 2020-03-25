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
    
    public partial class Document
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Document()
        {
            this.DocumentGood = new HashSet<DocumentGood>();
        }
    
        public int Id { get; set; }
        public Nullable<int> IdTypeDocument { get; set; }
        public Nullable<int> IdSener { get; set; }
        public Nullable<int> IdRecipient { get; set; }
        public Nullable<int> IdWarSender { get; set; }
        public Nullable<int> IdWarRecipient { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual Organization Organization { get; set; }
        public virtual Organization Organization1 { get; set; }
        public virtual TypeDocument TypeDocument { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual Warehouse Warehouse1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DocumentGood> DocumentGood { get; set; }
    }
}
