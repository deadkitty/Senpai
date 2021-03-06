﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SenpaiModel;

namespace SenpaiModel.Migrations
{
    [DbContext(typeof(SenpaiDatabase))]
    partial class SenpaiDatabaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932");

            modelBuilder.Entity("SenpaiModel.Kanji", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("EFactor");

                    b.Property<string>("Example");

                    b.Property<string>("Kunyomi");

                    b.Property<int>("LastRound");

                    b.Property<int?>("LessonId");

                    b.Property<string>("Meaning");

                    b.Property<int>("NextRound");

                    b.Property<string>("Onyomi");

                    b.Property<string>("Sign");

                    b.Property<int>("Timestamp");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("LessonId");

                    b.ToTable("Kanjis");
                });

            modelBuilder.Entity("SenpaiModel.Lesson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("NextRound");

                    b.Property<int>("Size");

                    b.Property<int>("SortIndex");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("SenpaiModel.Word", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<float>("EFactorGer");

                    b.Property<float>("EFactorJap");

                    b.Property<string>("Example");

                    b.Property<string>("Kana");

                    b.Property<string>("Kanji");

                    b.Property<int>("LastRoundGer");

                    b.Property<int>("LastRoundJap");

                    b.Property<int?>("LessonId");

                    b.Property<int>("NextRoundGer");

                    b.Property<int>("NextRoundJap");

                    b.Property<string>("ShowDesc");

                    b.Property<string>("ShowWord");

                    b.Property<string>("Translation");

                    b.Property<string>("Type");

                    b.Property<string>("VocabType");

                    b.HasKey("Id");

                    b.HasIndex("LessonId");

                    b.ToTable("Words");
                });

            modelBuilder.Entity("SenpaiModel.Kanji", b =>
                {
                    b.HasOne("SenpaiModel.Lesson", "Lesson")
                        .WithMany("Kanjis")
                        .HasForeignKey("LessonId");
                });

            modelBuilder.Entity("SenpaiModel.Word", b =>
                {
                    b.HasOne("SenpaiModel.Lesson", "Lesson")
                        .WithMany("Words")
                        .HasForeignKey("LessonId");
                });
#pragma warning restore 612, 618
        }
    }
}
