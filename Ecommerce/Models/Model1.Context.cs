﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ecommerce.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EcommerceDBEntities : DbContext
    {
        public EcommerceDBEntities()
            : base("name=EcommerceDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<order_detail> order_detail { get; set; }
        public virtual DbSet<Order_master> Order_master { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ImageData> ImageDatas { get; set; }
        public virtual DbSet<user> users { get; set; }
        public virtual DbSet<Product_Type> Product_Type { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}
