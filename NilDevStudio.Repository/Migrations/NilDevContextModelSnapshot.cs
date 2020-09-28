﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NilDevStudio.Repository;

namespace NilDevStudio.Repository.Migrations
{
    [DbContext(typeof(NilDevContext))]
    partial class NilDevContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity("NilDevStudio.Domain.EventSpeaker", b =>
                {
                    b.Property<int>("MyEventId");

                    b.Property<int>("SpeakerId");

                    b.HasKey("MyEventId", "SpeakerId");

                    b.HasIndex("SpeakerId");

                    b.ToTable("EventSpeakers");
                });

            modelBuilder.Entity("NilDevStudio.Domain.Lot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DateEnd");

                    b.Property<DateTime?>("DateStart");

                    b.Property<int>("MyEventId");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("MyEventId");

                    b.ToTable("Lots");
                });

            modelBuilder.Entity("NilDevStudio.Domain.MyEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateEvent");

                    b.Property<string>("Email");

                    b.Property<string>("ImageURL");

                    b.Property<string>("Local");

                    b.Property<int>("QuantPeople");

                    b.Property<string>("Telephone");

                    b.Property<string>("Theme");

                    b.HasKey("Id");

                    b.ToTable("MyEvents");
                });

            modelBuilder.Entity("NilDevStudio.Domain.SocialNetwork", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("MyEventId");

                    b.Property<string>("Name");

                    b.Property<int?>("SpeakerId");

                    b.Property<string>("URL");

                    b.HasKey("Id");

                    b.HasIndex("MyEventId");

                    b.HasIndex("SpeakerId");

                    b.ToTable("SocialNetworks");
                });

            modelBuilder.Entity("NilDevStudio.Domain.Speaker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Curriculum");

                    b.Property<string>("Email");

                    b.Property<string>("ImageURL");

                    b.Property<string>("Name");

                    b.Property<string>("Telephone");

                    b.HasKey("Id");

                    b.ToTable("Speakers");
                });

            modelBuilder.Entity("NilDevStudio.Domain.EventSpeaker", b =>
                {
                    b.HasOne("NilDevStudio.Domain.MyEvent", "MyEvent")
                        .WithMany("EventSpeakers")
                        .HasForeignKey("MyEventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NilDevStudio.Domain.Speaker", "Speaker")
                        .WithMany("EventSpeakers")
                        .HasForeignKey("SpeakerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NilDevStudio.Domain.Lot", b =>
                {
                    b.HasOne("NilDevStudio.Domain.MyEvent")
                        .WithMany("Lots")
                        .HasForeignKey("MyEventId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NilDevStudio.Domain.SocialNetwork", b =>
                {
                    b.HasOne("NilDevStudio.Domain.MyEvent")
                        .WithMany("SocialNetworks")
                        .HasForeignKey("MyEventId");

                    b.HasOne("NilDevStudio.Domain.Speaker")
                        .WithMany("SocialNetworks")
                        .HasForeignKey("SpeakerId");
                });
#pragma warning restore 612, 618
        }
    }
}
