﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Socialite.Infrastructure.Data;

namespace Socialite.Infrastructure.Migrations
{
    [DbContext(typeof(SocialiteDbContext))]
    [Migration("20190228214325_AddTitleToPost")]
    partial class AddTitleToPost
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Socialite.Domain.AggregateModels.PostAggregate.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<int?>("StateId");

                    b.Property<string>("Text");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Socialite.Domain.AggregateModels.PostAggregate.PostState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("PostState");
                });

            modelBuilder.Entity("Socialite.Domain.AggregateModels.StatusAggregate.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                    b.Property<string>("Mood");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("Socialite.Domain.AggregateModels.PostAggregate.Post", b =>
                {
                    b.HasOne("Socialite.Domain.AggregateModels.PostAggregate.PostState", "State")
                        .WithMany()
                        .HasForeignKey("StateId");
                });
#pragma warning restore 612, 618
        }
    }
}
