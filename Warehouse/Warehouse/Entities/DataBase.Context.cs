﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ttlEntities : DbContext
    {
        public ttlEntities()
            : base("name=ttlEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Document> Document { get; set; }
        public virtual DbSet<DocumentGood> DocumentGood { get; set; }
        public virtual DbSet<Good> Good { get; set; }
        public virtual DbSet<GroupGood> GroupGood { get; set; }
        public virtual DbSet<Manufacturer> Manufacturer { get; set; }
        public virtual DbSet<Organization> Organization { get; set; }
        public virtual DbSet<Party> Party { get; set; }
        public virtual DbSet<Passport> Passport { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<TypeDocument> TypeDocument { get; set; }
        public virtual DbSet<TypeOrganization> TypeOrganization { get; set; }
        public virtual DbSet<Unit> Unit { get; set; }
        public virtual DbSet<Warehouse> Warehouse { get; set; }
        public virtual DbSet<WarehouseGood> WarehouseGood { get; set; }
        public virtual DbSet<WarehouseOrganization> WarehouseOrganization { get; set; }
    }
}
