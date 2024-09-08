﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using UserService.Models.Context;

#nullable disable

namespace CSharpExamUserAPI.Migrations
{
    [DbContext(typeof(UsersDbContext))]
    [Migration("20240813113102_SecondMigration")]
    partial class SecondMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CSharpExamUserAPI.Models.Role", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<int>("RoleCode")
                        .HasColumnType("integer")
                        .HasColumnName("RoleCode");

                    b.HasKey("Id")
                        .HasName("PK_Roles");

                    b.HasIndex("RoleCode")
                        .IsUnique()
                        .HasDatabaseName("IX_Roles_RoleCode");

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("CSharpExamUserAPI.Models.User", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("Email");

                    b.Property<int?>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("integer")
                        .HasColumnName("PasswordHash");

                    b.Property<int?>("RoleId")
                        .IsRequired()
                        .HasColumnType("integer")
                        .HasColumnName("RoleId");

                    b.HasKey("Id")
                        .HasName("PK_Users");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasDatabaseName("IX_Users_Email");

                    b.HasIndex("RoleId");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("CSharpExamUserAPI.Models.User", b =>
                {
                    b.HasOne("CSharpExamUserAPI.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Users_Roles_RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("CSharpExamUserAPI.Models.Role", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
