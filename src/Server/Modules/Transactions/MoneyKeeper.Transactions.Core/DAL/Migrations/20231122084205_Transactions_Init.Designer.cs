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
    [Migration("20231122084205_Transactions_Init")]
    partial class Transactions_Init
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
                        .HasColumnType("INTEGER");

                    b.Property<bool>("OcrValidationResult")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("SpreadsheetEntered")
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("UploadDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ReceiptInfos");
                });
#pragma warning restore 612, 618
        }
    }
}
