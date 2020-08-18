using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using CareerCloud.Pocos;
namespace CareerCloud.EntityFrameworkDataAccess
{
 public class CareerCloudContext : DbContext
    {
        public DbSet<ApplicantEducationPoco> ApplicantEducation { get; set; }
        public DbSet<ApplicantJobApplicationPoco> ApplicantJobApplication { get; set; }
        public DbSet<ApplicantProfilePoco> ApplicantProfile { get; set; }
        public DbSet<ApplicantResumePoco> ApplicantResume { get; set; }
        public DbSet<ApplicantSkillPoco> ApplicantSkill { get; set; }
        public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistory { get; set; }
        public DbSet<CompanyDescriptionPoco> CompanyDescription { get; set; }
        public DbSet<CompanyJobDescriptionPoco> CompanyJobDescription { get; set; }
        public DbSet<CompanyJobEducationPoco> CompanyJobEducation { get; set; }
        public DbSet<CompanyJobPoco> CompanyJob { get; set; }
        public DbSet<CompanyJobSkillPoco> CompanyJobSkill { get; set; }
        public DbSet<CompanyLocationPoco> CompanyLocation { get; set; }
        public DbSet<CompanyProfilePoco> CompanyProfile { get; set; }
        public DbSet<SecurityLoginPoco> SecurityLogin { get; set; }
        public DbSet<SecurityLoginsLogPoco> SecurityLoginsLog { get; set; }
        public DbSet<SecurityLoginsRolePoco> SecurityLoginsRole { get; set; }
        public DbSet<SecurityRolePoco> SecurityRole { get; set; }
        public DbSet<SystemCountryCodePoco> SystemCountryCode { get; set; }
        public DbSet<SystemLanguageCodePoco> SystemLanguageCode { get; set; }
       


        protected override void  OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        optionsBuilder.UseSqlServer(@"Data Source=localhost\HUMBERBRIDGING;Initial Catalog =JOB_PORTAL_DB;Integrated Security = True; ");

        }


        protected override void OnModelCreating( ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CompanyDescriptionPoco>()
                .HasOne(p => p.SystemLanguageCode)
                .WithMany(b => b.CompanyDescription)
                .HasForeignKey(p => p.LanguageId);
            base.OnModelCreating(modelBuilder);

        }


    }
}
