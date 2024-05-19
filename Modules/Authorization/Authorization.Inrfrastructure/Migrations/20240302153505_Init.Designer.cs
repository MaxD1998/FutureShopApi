﻿// <auto-generated />
using System;
using Authorization.Inrfrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Authorization.Inrfrastructure.Migrations
{
    [DbContext(typeof(AuthContext))]
    [Migration("20240302153505_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Authorization.Domain.Entities.RefreshTokenEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(0);

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1);

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date")
                        .HasColumnOrder(103);

                    b.Property<DateTime?>("ModifyTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(2);

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date")
                        .HasColumnOrder(102);

                    b.Property<Guid>("Token")
                        .HasColumnType("uuid")
                        .HasColumnOrder(101);

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnOrder(100);

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("RefreshToken", (string)null);
                });

            modelBuilder.Entity("Authorization.Domain.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnOrder(0);

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(1);

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date")
                        .HasColumnOrder(106);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnOrder(104);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnOrder(100);

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnOrder(105);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnOrder(101);

                    b.Property<DateTime?>("ModifyTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnOrder(2);

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnOrder(103);

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnOrder(107);

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("User", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("d6669a68-5afb-432d-858f-3f5181579a90"),
                            CreateTime = new DateTime(2024, 3, 2, 15, 35, 4, 653, DateTimeKind.Utc).AddTicks(3303),
                            DateOfBirth = new DateOnly(1, 1, 1),
                            Email = "superadmin@futureshop.pl",
                            FirstName = "Super",
                            HashedPassword = "$2a$11$5tVc.NkKOpJvavSzTx3Wm.fdZ.S6gESA7LXZPO1z71feaeykq1yse",
                            LastName = "Admin",
                            Type = 0
                        });
                });

            modelBuilder.Entity("Authorization.Domain.Entities.RefreshTokenEntity", b =>
                {
                    b.HasOne("Authorization.Domain.Entities.UserEntity", "User")
                        .WithOne("RefreshToken")
                        .HasForeignKey("Authorization.Domain.Entities.RefreshTokenEntity", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Authorization.Domain.Entities.UserEntity", b =>
                {
                    b.Navigation("RefreshToken");
                });
#pragma warning restore 612, 618
        }
    }
}