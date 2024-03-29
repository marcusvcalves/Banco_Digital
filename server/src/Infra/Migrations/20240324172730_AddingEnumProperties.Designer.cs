﻿// <auto-generated />
using System;
using Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infra.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240324172730_AddingEnumProperties")]
    partial class AddingEnumProperties
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.Entities.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AccountType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Balance")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Accounts");

                    b.HasDiscriminator<string>("AccountType").HasValue("Common");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Domain.Models.Entities.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<bool>("ActiveCard")
                        .HasColumnType("bit");

                    b.Property<string>("CardType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Cards");

                    b.HasDiscriminator<string>("CardType").HasValue("Normal");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Domain.Models.Entities.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ClientType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cpf")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Domain.Models.Entities.Policy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CreditCardId")
                        .HasColumnType("int");

                    b.Property<int?>("DebitCardId")
                        .HasColumnType("int");

                    b.Property<DateTime>("HiringDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TriggeringDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex("CreditCardId");

                    b.HasIndex("DebitCardId");

                    b.ToTable("Policies");
                });

            modelBuilder.Entity("Domain.Models.Entities.CheckingAccount", b =>
                {
                    b.HasBaseType("Domain.Models.Entities.Account");

                    b.Property<decimal>("MonthlyFee")
                        .HasColumnType("decimal(18, 2)");

                    b.HasDiscriminator().HasValue("Checking");
                });

            modelBuilder.Entity("Domain.Models.Entities.SavingsAccount", b =>
                {
                    b.HasBaseType("Domain.Models.Entities.Account");

                    b.Property<decimal>("ReturnRates")
                        .HasColumnType("decimal(18, 2)");

                    b.HasDiscriminator().HasValue("Savings");
                });

            modelBuilder.Entity("Domain.Models.Entities.CreditCard", b =>
                {
                    b.HasBaseType("Domain.Models.Entities.Card");

                    b.Property<decimal>("CreditLimit")
                        .HasColumnType("decimal(18, 2)");

                    b.HasDiscriminator().HasValue("Credit");
                });

            modelBuilder.Entity("Domain.Models.Entities.DebitCard", b =>
                {
                    b.HasBaseType("Domain.Models.Entities.Card");

                    b.Property<decimal>("DailyLimit")
                        .HasColumnType("decimal(18, 2)");

                    b.HasDiscriminator().HasValue("Debit");
                });

            modelBuilder.Entity("Domain.Models.Entities.Account", b =>
                {
                    b.HasOne("Domain.Models.Entities.Client", "Client")
                        .WithMany("Accounts")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Domain.Models.Entities.Card", b =>
                {
                    b.HasOne("Domain.Models.Entities.Account", "Account")
                        .WithMany("Cards")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Domain.Models.Entities.Policy", b =>
                {
                    b.HasOne("Domain.Models.Entities.CreditCard", "CreditCard")
                        .WithMany()
                        .HasForeignKey("CreditCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Entities.DebitCard", null)
                        .WithMany("Policies")
                        .HasForeignKey("DebitCardId");

                    b.Navigation("CreditCard");
                });

            modelBuilder.Entity("Domain.Models.Entities.Account", b =>
                {
                    b.Navigation("Cards");
                });

            modelBuilder.Entity("Domain.Models.Entities.Client", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("Domain.Models.Entities.DebitCard", b =>
                {
                    b.Navigation("Policies");
                });
#pragma warning restore 612, 618
        }
    }
}
