﻿// <auto-generated />
using System;
using EngineClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EngineClasses.Migrations
{
    [DbContext(typeof(LudoContext))]
    [Migration("20200403131858_UpdateWithGameSquareTableWithNull")]
    partial class UpdateWithGameSquareTableWithNull
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EngineClasses.GameLog", b =>
                {
                    b.Property<int>("GameLogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("WinnerPlayer")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GameLogId");

                    b.ToTable("GameLog");
                });

            modelBuilder.Entity("EngineClasses.GamePiece", b =>
                {
                    b.Property<int>("GamePieceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("GameSquareId")
                        .HasColumnType("int");

                    b.Property<bool>("IsAtBase")
                        .HasColumnType("bit");

                    b.Property<bool>("IsAtGoal")
                        .HasColumnType("bit");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.HasKey("GamePieceId");

                    b.HasIndex("GameSquareId");

                    b.HasIndex("PlayerId");

                    b.ToTable("GamePiece");
                });

            modelBuilder.Entity("EngineClasses.GameSquare", b =>
                {
                    b.Property<int>("GameSquareId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EndSquare")
                        .HasColumnType("bit");

                    b.Property<bool>("StartingSquare")
                        .HasColumnType("bit");

                    b.HasKey("GameSquareId");

                    b.ToTable("GameSquare");
                });

            modelBuilder.Entity("EngineClasses.Player", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SessionId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlayerId");

                    b.HasIndex("SessionId");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("EngineClasses.Session", b =>
                {
                    b.Property<int>("SessionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Turns")
                        .HasColumnType("int");

                    b.HasKey("SessionId");

                    b.ToTable("Session");
                });

            modelBuilder.Entity("EngineClasses.GamePiece", b =>
                {
                    b.HasOne("EngineClasses.GameSquare", "GameSquare")
                        .WithMany()
                        .HasForeignKey("GameSquareId");

                    b.HasOne("EngineClasses.Player", "Player")
                        .WithMany("GamePiece")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EngineClasses.Player", b =>
                {
                    b.HasOne("EngineClasses.Session", "Session")
                        .WithMany("Player")
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
