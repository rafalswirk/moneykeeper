﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MoneyKeeper.Budget.DAL;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MoneyKeeper.Budget.DAL.Migrations
{
    [DbContext(typeof(BudgetCategoryDbContext))]
    [Migration("20230518204348_TaxIdStorage")]
    partial class TaxIdStorage
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MoneyKeeper.Budget.Entities.BudgetCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Group")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("BudgetCategories");
                });

            modelBuilder.Entity("MoneyKeeper.Budget.Entities.CategorySpreadsheetMap", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("Column")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Row")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("CategoryMap");
                });

            modelBuilder.Entity("MoneyKeeper.Budget.Entities.TaxId", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TaxIdentificationNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TaxIds");
                });

            modelBuilder.Entity("MoneyKeeper.Budget.Entities.TaxIdMapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryMapId")
                        .HasColumnType("integer");

                    b.Property<int>("TaxIdId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CategoryMapId");

                    b.HasIndex("TaxIdId");

                    b.ToTable("TaxIdMapping");
                });

            modelBuilder.Entity("MoneyKeeper.Budget.Entities.CategorySpreadsheetMap", b =>
                {
                    b.HasOne("MoneyKeeper.Budget.Entities.BudgetCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("MoneyKeeper.Budget.Entities.TaxIdMapping", b =>
                {
                    b.HasOne("MoneyKeeper.Budget.Entities.CategorySpreadsheetMap", "CategoryMap")
                        .WithMany()
                        .HasForeignKey("CategoryMapId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MoneyKeeper.Budget.Entities.TaxId", "TaxId")
                        .WithMany()
                        .HasForeignKey("TaxIdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoryMap");

                    b.Navigation("TaxId");
                });
#pragma warning restore 612, 618
        }
    }
}
