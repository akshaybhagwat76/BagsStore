﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BagsStore.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class bags_Entities : DbContext
    {
        public bags_Entities()
            : base("name=bags_Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<GaleryImage> GaleryImages { get; set; }
        public virtual DbSet<GaleryItem> GaleryItems { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemContent> ItemContents { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Picture> Pictures { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<Testimonial> Testimonials { get; set; }
    }
}