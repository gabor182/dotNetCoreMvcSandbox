using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using dotNetCoreMvcSandbox.Models;

namespace dotNetCoreMvcSandbox.Migrations
{
    [DbContext(typeof(ProductsContext))]
    [Migration("20170623115820_CartAdded")]
    partial class CartAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("dotNetCoreMvcSandbox.Models.Cart", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("SessionId");

                    b.HasKey("Id");

                    b.ToTable("Cart");
                });

            modelBuilder.Entity("dotNetCoreMvcSandbox.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CartId");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<double>("Price");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("dotNetCoreMvcSandbox.Models.Product", b =>
                {
                    b.HasOne("dotNetCoreMvcSandbox.Models.Cart")
                        .WithMany("Products")
                        .HasForeignKey("CartId");
                });
        }
    }
}
