﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MoneyKeeper.Transactions.Core.DAL;

#nullable disable

namespace MoneyKeeper.Transactions.Core.DAL.Migrations
{
    [DbContext(typeof(TransactionsDbContext))]
    [Migration("20231222110159_TransactionEntryDate")]
    partial class TransactionEntryDate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.13");

            modelBuilder.Entity("MoneyKeeper.Transactions.Core.Entities.ReceiptInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("OcrDataGenerated")
                        .HasColumnType("boolean");

                    b.Property<bool?>("OcrValidationResult")
                        .HasColumnType("boolean");

                    b.Property<bool>("SpreadsheetEntered")
                        .HasColumnType("boolean");

                    b.Property<DateOnly>("UploadDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ReceiptInfos");
                });

            modelBuilder.Entity("MoneyKeeper.Transactions.Core.Entities.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("InfoId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Value")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("InfoId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("MoneyKeeper.Transactions.Core.Entities.Transaction", b =>
                {
                    b.HasOne("MoneyKeeper.Transactions.Core.Entities.ReceiptInfo", "Info")
                        .WithMany()
                        .HasForeignKey("InfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Info");
                });
#pragma warning restore 612, 618
        }
    }
}
