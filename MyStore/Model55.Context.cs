﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyStore
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MyStoreEntities5 : DbContext
    {
        public MyStoreEntities5()
            : base("name=MyStoreEntities5")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ACHETEUR> ACHETEUR { get; set; }
        public virtual DbSet<CATEGORIE> CATEGORIE { get; set; }
        public virtual DbSet<GENRE> GENRE { get; set; }
        public virtual DbSet<PANIER> PANIER { get; set; }
        public virtual DbSet<PRODUIT> PRODUIT { get; set; }
        public virtual DbSet<VENDEUR> VENDEUR { get; set; }
    }
}
