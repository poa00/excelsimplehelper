using System.Data.Entity;
using Model.DataBase.Model;

namespace Model.DataBase.Context
{

    public partial class DataBaseContext : DbContext
    {
        public DataBaseContext()
            : base("name=DataBaseContext")
        {
        }

        public virtual DbSet<CertificateDGs> CertificateDGs { get; set; }
        public virtual DbSet<Certifications> Certifications { get; set; }
        public virtual DbSet<Lesson> Lesson { get; set; }
        public virtual DbSet<ProgramDGs> ProgramDGs { get; set; }
        public virtual DbSet<Programs> Programs { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<TypeDocument> TypeDocument { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CertificateDGs>()
                .Property(e => e.issueDate)
                .IsFixedLength();

            modelBuilder.Entity<CertificateDGs>()
                .Property(e => e.party)
                .IsFixedLength();

            modelBuilder.Entity<Certifications>()
                .Property(e => e.startEducation)
                .IsFixedLength();

            modelBuilder.Entity<Certifications>()
                .Property(e => e.endEducation)
                .IsFixedLength();

            modelBuilder.Entity<Certifications>()
                .Property(e => e.issueDate)
                .IsFixedLength();

            modelBuilder.Entity<Lesson>()
                .HasMany(e => e.Programs)
                .WithRequired(e => e.Lesson)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProgramDGs>()
                .HasMany(e => e.CertificateDGs)
                .WithRequired(e => e.ProgramDGs)
                .HasForeignKey(e => e.idProgramDG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Programs>()
                .HasMany(e => e.Certifications)
                .WithRequired(e => e.Programs)
                .HasForeignKey(e => e.idProgramm)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Students>()
                .Property(e => e.name)
                .IsFixedLength();

            modelBuilder.Entity<Students>()
                .Property(e => e.surname)
                .IsFixedLength();

            modelBuilder.Entity<Students>()
                .Property(e => e.patronymic)
                .IsFixedLength();

            modelBuilder.Entity<Students>()
                .Property(e => e.dateDirth)
                .IsFixedLength();

            modelBuilder.Entity<Students>()
                .HasMany(e => e.CertificateDGs)
                .WithRequired(e => e.Students)
                .HasForeignKey(e => e.idStudent)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Students>()
                .HasMany(e => e.Certifications)
                .WithRequired(e => e.Students)
                .HasForeignKey(e => e.idStudent)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TypeDocument>()
                .HasMany(e => e.ProgramDGs)
                .WithRequired(e => e.TypeDocument)
                .HasForeignKey(e => e.typeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TypeDocument>()
                .HasMany(e => e.Programs)
                .WithRequired(e => e.TypeDocument)
                .HasForeignKey(e => e.typeId)
                .WillCascadeOnDelete(false);
        }
    }
}
