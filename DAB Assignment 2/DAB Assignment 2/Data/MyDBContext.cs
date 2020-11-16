using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAB2 
{
    public class MyDBContext : DbContext
    {
        public DbSet<TestCenterManagement> TestCenterManagement { get; set; }
        public DbSet<TestCenter> TestCenter { get; set; }
        public DbSet<Citizen> Citizen { get; set; }
        public DbSet<Municipality> Municipality { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Nation> Nation { get; set; }
        public DbSet<TestCenterCitizen> TestCenterCitizen { get; set; }
        public DbSet<LocationCitizen> LocationCitizen { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=C:\\Users\\Premi\\Downloads\\DAB_Assignment_2\\DAB Assignment 2\\DAB Assignment 2\\MyDBContext.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //defining primary keys
            modelBuilder.Entity<TestCenterManagement>()
                .HasKey(TCM => TCM.PhoneNumber);

            modelBuilder.Entity<TestCenter>()
                .HasKey(TC => TC.TestCenterID);

            modelBuilder.Entity<Citizen>()
                .HasKey(C => C.SocialSecurityNumber);

            modelBuilder.Entity<Municipality>()
                .HasKey(M => M.MunicipalityID);

            modelBuilder.Entity<Nation>()
                .HasKey(N => N.nationName);

            modelBuilder.Entity<Location>()
                .HasKey(L => L.Address);

            modelBuilder.Entity<TestCenterCitizen>()
                .HasKey(tcc => new { tcc.SocialSecurityNumber, tcc.TestCenterID });

            modelBuilder.Entity<LocationCitizen>()
                .HasKey(lcc => new { lcc.SocialSecurityNumber, lcc.Address });

            // defining relationships
            modelBuilder.Entity<TestCenterManagement>()
                .HasOne(TCM => TCM.testcenter)
                .WithOne(TC => TC.testcentermanagement)
                .HasForeignKey<TestCenterManagement>(TCID => TCID.TestCenterID);

            modelBuilder.Entity<TestCenterCitizen>()
                .HasOne(C => C.citizen)
                .WithMany(TCC => TCC.testCenterCitizens)
                .HasForeignKey(CID => CID.SocialSecurityNumber);

            modelBuilder.Entity<TestCenterCitizen>()
                .HasOne(TC => TC.testCenter)
                .WithMany(TCC2 => TCC2.testCenterCitizens)
                .HasForeignKey(TCID => TCID.TestCenterID);

            modelBuilder.Entity<LocationCitizen>()
                .HasOne(L => L.location)
                .WithMany(LC => LC.locationCitizens)
                .HasForeignKey(LID => LID.Address);

            modelBuilder.Entity<LocationCitizen>()
                .HasOne(C => C.citizen)
                .WithMany(LC2 => LC2.locationCitizens)
                .HasForeignKey(CID => CID.SocialSecurityNumber);

            modelBuilder.Entity<Municipality>()
                .HasOne(M => M.nation)
                .WithMany(N => N.municipalities)
                .HasForeignKey(MID => MID.nationName);

            modelBuilder.Entity<Municipality>()
                .HasMany(M => M.locations)
                .WithOne(L => L.municipality)
                .HasForeignKey(LID => LID.MunicipalityID);

            modelBuilder.Entity<Municipality>()
                .HasMany(M => M.Citizens)
                .WithOne(C => C.municipality)
                .HasForeignKey(CID => CID.MunicipalityID);

            modelBuilder.Entity<Municipality>()
                .HasMany(M => M.TestCenters)
                .WithOne(TC => TC.municipality)
                .HasForeignKey(TCID => TCID.MunicipalityID);
        }
    }
}