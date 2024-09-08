﻿// <auto-generated />
using System;
using MessagingService.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MessageAPI.Migrations
{
    [DbContext(typeof(MessageDbContext))]
    partial class MessageDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MessageAPI.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("DateTime");

                    b.Property<Guid>("Guid")
                        .HasColumnType("uuid")
                        .HasColumnName("Guid");

                    b.Property<bool>("IsReaded")
                        .HasColumnType("boolean")
                        .HasColumnName("IsReaded");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(1023)
                        .HasColumnType("character varying(1023)")
                        .HasColumnName("Text");

                    b.Property<Guid>("UserFromGuid")
                        .HasColumnType("uuid")
                        .HasColumnName("UserFromGuid");

                    b.Property<Guid>("UserToGuid")
                        .HasColumnType("uuid")
                        .HasColumnName("UserToGuid");

                    b.HasKey("Id")
                        .HasName("PK_Messages");

                    b.HasIndex("Guid")
                        .IsUnique()
                        .HasDatabaseName("IX_Messages_Guid");

                    b.HasIndex("IsReaded")
                        .HasDatabaseName("IX_Message_IsReaded");

                    b.HasIndex("UserFromGuid")
                        .HasDatabaseName("IX_Message_UserFromGuid");

                    b.HasIndex("UserToGuid")
                        .HasDatabaseName("IX_Message_UserToGuid");

                    b.ToTable("Messages", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
