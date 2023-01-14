﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectManagementBusinessLayer.Data;

namespace ProjectManagementBusinessLayer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230114103154_AddSerialNumber")]
    partial class AddSerialNumber
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ProjectManagementBusinessLayer.Entities.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("ProjectManagementBusinessLayer.Entities.Deliverable", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AdminId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ProjectPhaseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.HasIndex("ProjectPhaseId");

                    b.ToTable("Deliverables");
                });

            modelBuilder.Entity("ProjectManagementBusinessLayer.Entities.Invoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AdminId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("InvoiceDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("InvoiceTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SerialNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("ProjectManagementBusinessLayer.Entities.InvoicePaymentTerms", b =>
                {
                    b.Property<Guid>("InvoiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PaymentTermId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("InvoiceId", "PaymentTermId");

                    b.HasIndex("PaymentTermId");

                    b.ToTable("InvoicePaymentTerms");
                });

            modelBuilder.Entity("ProjectManagementBusinessLayer.Entities.PaymentTerm", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AdminId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("DeliverableId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("bit");

                    b.Property<decimal>("PaymentTermAmount")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("PaymentTermTitle")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.HasIndex("DeliverableId");

                    b.ToTable("PaymentTerms");
                });

            modelBuilder.Entity("ProjectManagementBusinessLayer.Entities.Person", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Person");
                });

            modelBuilder.Entity("ProjectManagementBusinessLayer.Entities.Phase", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PhaseName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Phases");
                });

            modelBuilder.Entity("ProjectManagementBusinessLayer.Entities.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AdminId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("ContractAmount")
                        .HasColumnType("decimal(18,4)");

                    b.Property<byte[]>("ContractFile")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ContractFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContractFileType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProjectManagerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProjectName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ProjectStatusId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProjectTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.HasIndex("ClientId");

                    b.HasIndex("ProjectManagerId");

                    b.HasIndex("ProjectStatusId");

                    b.HasIndex("ProjectTypeId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ProjectManagementBusinessLayer.Entities.ProjectPhase", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("PhaseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PhaseId");

                    b.HasIndex("ProjectId", "PhaseId")
                        .IsUnique();

                    b.ToTable("ProjectPhases");
                });

            modelBuilder.Entity("ProjectManagementBusinessLayer.Entities.ProjectStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProjectStatuses");
                });

            modelBuilder.Entity("ProjectManagementBusinessLayer.Entities.ProjectType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProjectTypes");
                });

            modelBuilder.Entity("ProjectManagementBusinessLayer.Entities.Admin", b =>
                {
                    b.HasBaseType("ProjectManagementBusinessLayer.Entities.Person");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("ProjectManagementBusinessLayer.Entities.Engineer", b =>
                {
                    b.HasBaseType("ProjectManagementBusinessLayer.Entities.Person");

                    b.Property<string>("Band")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Engineer");
                });

            modelBuilder.Entity("ProjectManagementBusinessLayer.Entities.ProjectManager", b =>
                {
                    b.HasBaseType("ProjectManagementBusinessLayer.Entities.Person");

                    b.HasDiscriminator().HasValue("ProjectManager");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ProjectManagementBusinessLayer.Entities.Person", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ProjectManagementBusinessLayer.Entities.Person", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagementBusinessLayer.Entities.Person", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ProjectManagementBusinessLayer.Entities.Person", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectManagementBusinessLayer.Entities.Deliverable", b =>
                {
                    b.HasOne("ProjectManagementBusinessLayer.Entities.Admin", null)
                        .WithMany("Deliverables")
                        .HasForeignKey("AdminId");

                    b.HasOne("ProjectManagementBusinessLayer.Entities.ProjectPhase", "ProjectPhase")
                        .WithMany()
                        .HasForeignKey("ProjectPhaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectManagementBusinessLayer.Entities.Invoice", b =>
                {
                    b.HasOne("ProjectManagementBusinessLayer.Entities.Admin", null)
                        .WithMany("Invoices")
                        .HasForeignKey("AdminId");

                    b.HasOne("ProjectManagementBusinessLayer.Entities.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectManagementBusinessLayer.Entities.InvoicePaymentTerms", b =>
                {
                    b.HasOne("ProjectManagementBusinessLayer.Entities.Invoice", "Invoice")
                        .WithMany("InvoicePaymentTerms")
                        .HasForeignKey("InvoiceId")
                        .IsRequired();

                    b.HasOne("ProjectManagementBusinessLayer.Entities.PaymentTerm", "PaymentTerm")
                        .WithMany("InvoicePaymentTerms")
                        .HasForeignKey("PaymentTermId")
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectManagementBusinessLayer.Entities.PaymentTerm", b =>
                {
                    b.HasOne("ProjectManagementBusinessLayer.Entities.Admin", null)
                        .WithMany("PaymentTerms")
                        .HasForeignKey("AdminId");

                    b.HasOne("ProjectManagementBusinessLayer.Entities.Deliverable", "Deliverable")
                        .WithMany()
                        .HasForeignKey("DeliverableId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectManagementBusinessLayer.Entities.Project", b =>
                {
                    b.HasOne("ProjectManagementBusinessLayer.Entities.Admin", null)
                        .WithMany("Projects")
                        .HasForeignKey("AdminId");

                    b.HasOne("ProjectManagementBusinessLayer.Entities.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagementBusinessLayer.Entities.ProjectManager", "ProjectManager")
                        .WithMany("Projects")
                        .HasForeignKey("ProjectManagerId");

                    b.HasOne("ProjectManagementBusinessLayer.Entities.ProjectStatus", "ProjectStatus")
                        .WithMany()
                        .HasForeignKey("ProjectStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagementBusinessLayer.Entities.ProjectType", "ProjectType")
                        .WithMany()
                        .HasForeignKey("ProjectTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectManagementBusinessLayer.Entities.ProjectPhase", b =>
                {
                    b.HasOne("ProjectManagementBusinessLayer.Entities.Phase", "Phase")
                        .WithMany("ProjectPhases")
                        .HasForeignKey("PhaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManagementBusinessLayer.Entities.Project", "Project")
                        .WithMany("ProjectPhases")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
