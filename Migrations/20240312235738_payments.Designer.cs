﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using me_faz_um_pix.Data;

#nullable disable

namespace me_faz_um_pix.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20240312235738_payments")]
    partial class payments
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("me_faz_um_pix.Models.Payment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("PaymentProviderAccountId")
                        .HasColumnType("bigint");

                    b.Property<long>("PixKeyId")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("PaymentProviderAccountId");

                    b.HasIndex("PixKeyId");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("me_faz_um_pix.Models.PaymentProvider", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Token")
                        .IsUnique();

                    b.ToTable("PaymentProvider");
                });

            modelBuilder.Entity("me_faz_um_pix.Models.PaymentProviderAccount", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Agency")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("PaymentProviderId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PaymentProviderId");

                    b.HasIndex("UserId");

                    b.ToTable("PaymentProviderAccount");
                });

            modelBuilder.Entity("me_faz_um_pix.Models.PixKey", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("PaymentProviderAccountId")
                        .HasColumnType("bigint");

                    b.Property<string>("PixType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PaymentProviderAccountId");

                    b.HasIndex("Value")
                        .IsUnique();

                    b.ToTable("PixKey");
                });

            modelBuilder.Entity("me_faz_um_pix.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Cpf")
                        .IsUnique();

                    b.ToTable("User");
                });

            modelBuilder.Entity("me_faz_um_pix.Models.Payment", b =>
                {
                    b.HasOne("me_faz_um_pix.Models.PaymentProviderAccount", "PaymentProviderAccount")
                        .WithMany("Payments")
                        .HasForeignKey("PaymentProviderAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("me_faz_um_pix.Models.PixKey", "PixKey")
                        .WithMany("Payments")
                        .HasForeignKey("PixKeyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaymentProviderAccount");

                    b.Navigation("PixKey");
                });

            modelBuilder.Entity("me_faz_um_pix.Models.PaymentProviderAccount", b =>
                {
                    b.HasOne("me_faz_um_pix.Models.PaymentProvider", "PaymentProvider")
                        .WithMany("PaymentProviderAccounts")
                        .HasForeignKey("PaymentProviderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("me_faz_um_pix.Models.User", "User")
                        .WithMany("PaymentProviderAccounts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaymentProvider");

                    b.Navigation("User");
                });

            modelBuilder.Entity("me_faz_um_pix.Models.PixKey", b =>
                {
                    b.HasOne("me_faz_um_pix.Models.PaymentProviderAccount", "PaymentProviderAccount")
                        .WithMany("PixKeys")
                        .HasForeignKey("PaymentProviderAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaymentProviderAccount");
                });

            modelBuilder.Entity("me_faz_um_pix.Models.PaymentProvider", b =>
                {
                    b.Navigation("PaymentProviderAccounts");
                });

            modelBuilder.Entity("me_faz_um_pix.Models.PaymentProviderAccount", b =>
                {
                    b.Navigation("Payments");

                    b.Navigation("PixKeys");
                });

            modelBuilder.Entity("me_faz_um_pix.Models.PixKey", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("me_faz_um_pix.Models.User", b =>
                {
                    b.Navigation("PaymentProviderAccounts");
                });
#pragma warning restore 612, 618
        }
    }
}
