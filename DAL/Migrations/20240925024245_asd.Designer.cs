﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(CustomerContext))]
    [Migration("20240925024245_asd")]
    partial class asd
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DAL.Models.MstLoans", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Amount")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("amount");

                    b.Property<string>("BorrowerId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("borrower_id");

                    b.Property<string>("InteresestRate")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("interest_rate");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("status");

                    b.Property<DateTime>("Update_at")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("Update_at");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<string>("duration")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("duration");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("mst_loans");
                });

            modelBuilder.Entity("DAL.Models.MstUser", b =>
                {
                    b.ToTable("mst_user", (string)null);
                });

            modelBuilder.Entity("DAL.Models.MstUser2", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("id");

                    b.Property<decimal?>("Balance")
                        .HasColumnType("numeric")
                        .HasColumnName("balance");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("password");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("role");

                    b.HasKey("Id")
                        .HasName("mst_user2_pkey");

                    b.ToTable("mst_user2", (string)null);
                });

            modelBuilder.Entity("DAL.Models.trn_funding", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric")
                        .HasColumnName("Amount");

                    b.Property<string>("Lender_id")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("lender_id");

                    b.Property<string>("Loan_Id")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("loan_id");

                    b.Property<string>("LoansId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("funded_at")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("funded_at");

                    b.HasKey("Id");

                    b.HasIndex("LoansId");

                    b.HasIndex("UserId");

                    b.ToTable("trn_funding");
                });

            modelBuilder.Entity("DAL.Models.MstLoans", b =>
                {
                    b.HasOne("DAL.Models.MstUser2", "User")
                        .WithMany("MstLoans")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Models.trn_funding", b =>
                {
                    b.HasOne("DAL.Models.MstLoans", "Loans")
                        .WithMany()
                        .HasForeignKey("LoansId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Models.MstUser2", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Loans");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DAL.Models.MstUser2", b =>
                {
                    b.Navigation("MstLoans");
                });
#pragma warning restore 612, 618
        }
    }
}
