using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;

namespace gestion_scolaire.Entities
{
    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Specialite> Specialites { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<Absence> Absences { get; set; }
        public virtual DbSet<Groupe> Groupes { get; set; }
        public virtual DbSet<GroupDetail> GroupDetails { get; set; }
        public virtual DbSet<Seance> Seances { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=root;database=scolaire");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //add User Table
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired();
                entity.Property(e => e.Email)
                    .IsRequired();

                entity.Property(e => e.Password)
                    .IsRequired();


                entity.Property(e => e.Cin);


                entity.Property(e => e.Age);


                entity.Property(e => e.Created)
                    .IsRequired();

                entity.Property(e => e.IsEnabled)
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValue(true)
                    .IsRequired();

                entity.Property(e => e.Role)
                    .IsRequired();
            });

            // add Teacher Table
            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.ToTable("teacher");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired();

                entity.Property(e => e.Email)
                    .IsRequired();

                entity.Property(e => e.Password)
                    .IsRequired();


                entity.Property(e => e.Cin)
                   .IsRequired();


                entity.Property(e => e.Age)
                    .IsRequired();


                entity.Property(e => e.Created)
                    .IsRequired();

                entity.Property(e => e.IsEnabled)
                    .HasColumnType("tinyint(1)")
                    .IsRequired();

                entity.Property(e => e.Role)
                    .IsRequired();

                entity.HasOne(e => e.Specialite).WithMany(c => c.Teachers).IsRequired(false);
                entity.Property(e => e.SpecialiteId).IsRequired(true);
                entity.HasMany(e => e.GroupDetails).WithOne(c => c.Teacher).IsRequired(false);
            });

            //add Student Table
            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("student");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired();

                entity.Property(e => e.Email)
                    .IsRequired();

                entity.Property(e => e.Password)
                    .IsRequired();


                entity.Property(e => e.Cin)
                   .IsRequired();


                entity.Property(e => e.Age)
                    .IsRequired();


                entity.Property(e => e.Created)
                    .IsRequired();

                entity.Property(e => e.IsEnabled)
                    .HasColumnType("tinyint(1)")
                    .IsRequired();

                entity.Property(e => e.Role)
                    .IsRequired();

                entity.HasOne(e => e.Groupe).WithMany(c => c.Students).IsRequired(false);
                entity.Property(e => e.GroupeId).IsRequired(true);

                entity.HasMany(e => e.Notes).WithOne(c => c.Student).IsRequired(false);
            });

            // add Specialite Table
            modelBuilder.Entity<Specialite>(entity =>
            {
                entity.ToTable("specialite");
                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired();
                entity.HasMany(e => e.Modules).WithOne(c => c.Specialite).IsRequired(false);
                entity.HasMany(e => e.Teachers).WithOne(c => c.Specialite).IsRequired(false);
                entity.HasMany(e => e.Groupes).WithOne(c => c.Specialite).IsRequired(false);

            });

            // add Absence Table
            modelBuilder.Entity<Absence>(entity =>
            {
                entity.ToTable("absence");
                entity.Property(e => e.Id).HasColumnType("int(11)");
                entity.Property(e => e.Date)
                    .IsRequired();
                entity.HasOne(e => e.Module).WithMany(c => c.Absences).IsRequired(false);
                entity.Property(e => e.ModuleId).IsRequired(true);

                entity.HasOne(e => e.Groupe).WithMany(c => c.Absences).IsRequired(false);
                entity.Property(e => e.GroupeId).IsRequired(true);

                entity.HasOne(e => e.Student).WithMany(c => c.Absences).IsRequired(false);
                entity.Property(e => e.StudentId).IsRequired(true);

            });


            // add Note Table
            modelBuilder.Entity<Note>(entity =>
            {
                entity.ToTable("note");
                entity.Property(e => e.Id).HasColumnType("int(11)");
                entity.HasOne(e => e.Module).WithMany(c => c.Notes).IsRequired(false);
                entity.Property(e => e.ModuleId).IsRequired(true);
                entity.HasOne(e => e.Student).WithMany(c => c.Notes).IsRequired(false);
                entity.Property(e => e.StudentId).IsRequired(true);
                entity.Property(e => e.StudentNote)
                    .IsRequired();
            });

            // add Module Table
            modelBuilder.Entity<Module>(entity =>
            {
                entity.ToTable("module");
                entity.Property(e => e.Id).HasColumnType("int(11)").ValueGeneratedOnAdd().IsRequired(true);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Coef).IsRequired();
                entity.Property(e => e.SpecialiteId).IsRequired(true);
                entity.HasOne(e => e.Specialite).WithMany(c => c.Modules).IsRequired(false);
                entity.HasMany(e => e.Notes).WithOne(c => c.Module).IsRequired(false);

            });


            // add Groupe Table
            modelBuilder.Entity<Groupe>(entity =>
            {
                entity.ToTable("groupe");
                entity.Property(e => e.Id).HasColumnType("int(11)");
                entity.Property(e => e.Name);
                entity.Property(e => e.NrStudent);
                entity.HasOne(e => e.Specialite).WithMany(c => c.Groupes).IsRequired(false);
                entity.HasMany(e => e.GroupDetails).WithOne(c => c.Groupe).IsRequired(false);
                entity.HasMany(e => e.Students).WithOne(c => c.Groupe).IsRequired(false);
                entity.Property(e => e.SpecialiteId).IsRequired(true);

            });

            // add Groupe Seance
            modelBuilder.Entity<Seance>(entity =>
            {
                entity.ToTable("seance");
                entity.Property(e => e.Id).HasColumnType("int(11)");
                entity.Property(e => e.Title);
                entity.Property(e => e.StartTime);
                entity.Property(e => e.DaysOfWeek).HasColumnType("TEXT"); ;
                entity.Property(e => e.AllDay);
                entity.Property(e => e.EndTime);
             
                entity.HasOne(e => e.Module).WithMany(c => c.Seances).IsRequired(false);
                entity.HasOne(e => e.Teacher).WithMany(c => c.Seances).IsRequired(false);
                entity.HasOne(e => e.Groupe).WithMany(c => c.Seances).IsRequired(false);

                entity.Property(e => e.GroupId).IsRequired(true);
                entity.Property(e => e.TeacherId).IsRequired(true);
                entity.Property(e => e.ModuleId).IsRequired(true);
            });


            // add GroupDetail Table
            modelBuilder.Entity<GroupDetail>(entity =>
            {
                entity.ToTable("groupDetail");
                entity.Property(e => e.Id).HasColumnType("int(11)");
                entity.HasOne(e => e.Groupe).WithMany(c => c.GroupDetails).IsRequired(false);
                entity.HasOne(e => e.Teacher).WithMany(t => t.GroupDetails).IsRequired(false);
                entity.Property(e => e.GroupeId).IsRequired(true);
                entity.Property(e => e.TeacherId).IsRequired(true);
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
