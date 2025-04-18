﻿// <auto-generated />
using System;
using ClassLibraryDAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ClassLibraryDAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250405103746_AddBusinessTransactionPrimaryKey")]
    partial class AddBusinessTransactionPrimaryKey
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.3");

            modelBuilder.Entity("ClassLibraryEntities.BusinessCategory", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ClassLibraryEntities.BusinessSubItem", b =>
                {
                    b.Property<int>("SubItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ParentSubItemID")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<string>("SubItemName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.HasKey("SubItemID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("ParentSubItemID");

                    b.ToTable("SubCategories");
                });

            modelBuilder.Entity("ClassLibraryEntities.BusinessTransaction", b =>
                {
                    b.Property<int>("TransactionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("ChangeAmount")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Discount")
                        .HasColumnType("TEXT");

                    b.Property<string>("InvoiceNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("PaymentAmount")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("SubTotal")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Total")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("TEXT");

                    b.HasKey("TransactionID");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("ClassLibraryEntities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ClassLibraryEntities.TransactionItem", b =>
                {
                    b.Property<int>("TransactionItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TransactionID")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("TEXT");

                    b.HasKey("TransactionItemID");

                    b.HasIndex("TransactionID");

                    b.ToTable("TransactionItems");
                });

            modelBuilder.Entity("ClassLibraryEntities.BusinessSubItem", b =>
                {
                    b.HasOne("ClassLibraryEntities.BusinessCategory", "Category")
                        .WithMany("SubItems")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClassLibraryEntities.BusinessSubItem", "ParentSubItem")
                        .WithMany("NestedSubItems")
                        .HasForeignKey("ParentSubItemID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Category");

                    b.Navigation("ParentSubItem");
                });

            modelBuilder.Entity("ClassLibraryEntities.TransactionItem", b =>
                {
                    b.HasOne("ClassLibraryEntities.BusinessTransaction", null)
                        .WithMany("Items")
                        .HasForeignKey("TransactionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClassLibraryEntities.BusinessCategory", b =>
                {
                    b.Navigation("SubItems");
                });

            modelBuilder.Entity("ClassLibraryEntities.BusinessSubItem", b =>
                {
                    b.Navigation("NestedSubItems");
                });

            modelBuilder.Entity("ClassLibraryEntities.BusinessTransaction", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
