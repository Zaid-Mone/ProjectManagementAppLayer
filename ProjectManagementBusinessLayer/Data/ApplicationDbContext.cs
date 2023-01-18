using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectManagementBusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagementBusinessLayer.Data
{
    public class ApplicationDbContext : IdentityDbContext<Person>
    {
        public DbSet<Engineer> Engineers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectManager> ProjectManagers { get; set; }
        public DbSet<ProjectType> ProjectTypes { get; set; }
        public DbSet<ProjectStatus> ProjectStatuses { get; set; }
        public DbSet<ProjectPhase> ProjectPhases { get; set; }
        public DbSet<Phase> Phases { get; set; }
        public DbSet<PaymentTerm> PaymentTerms { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoicePaymentTerms> InvoicePaymentTerms { get; set; }
        public DbSet<Deliverable> Deliverables { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<ProjectDirector> ProjectDirectors { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ProjectPhase>().HasIndex(v => new { v.ProjectId, v.PhaseId }).IsUnique();
            builder.Entity<Project>()
                .Property(p => p.ContractAmount)
                .HasColumnType("decimal(18,4)");
            builder.Entity<PaymentTerm>()
             .Property(p => p.PaymentTermAmount)
             .HasColumnType("decimal(18,4)");

            builder.Entity<InvoicePaymentTerms>().HasKey(y => new { y.InvoiceId, y.PaymentTermId });
            //builder.Entity<Invoice>()
            // .HasMany(s => s.InvoicePaymentTerms)
            // .WithOne(sc => sc.Invoice)
            // .HasForeignKey(sc => sc.InvoiceId);
            builder.Entity<InvoicePaymentTerms>()
                     .HasOne(sc => sc.PaymentTerm)
                     .WithMany(c => c.InvoicePaymentTerms)
                    .HasForeignKey(sc => sc.PaymentTermId)
                    .OnDelete(deleteBehavior: DeleteBehavior.ClientSetNull);
            builder.Entity<InvoicePaymentTerms>()
                    .HasOne(sc => sc.Invoice)
                    .WithMany(c => c.InvoicePaymentTerms)
                    .HasForeignKey(sc => sc.InvoiceId)
                    .OnDelete(deleteBehavior:DeleteBehavior.ClientSetNull);
        }
    }
}
