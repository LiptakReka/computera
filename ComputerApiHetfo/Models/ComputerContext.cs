using Microsoft.EntityFrameworkCore;

namespace ComputerApiHetfo.Models;

public partial class ComputerContext : DbContext
{
    public ComputerContext()
    {
    }

    public ComputerContext(DbContextOptions<ComputerContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Comp> Comps { get; set; }
    public virtual DbSet<Osystem> Osystems { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comp>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.ToTable("comp");

            entity.HasIndex(e => e.osid, "OsId");

            entity.Property(e => e.brand)
                .HasMaxLength(37)
                .HasDefaultValueSql("'NULL'");
            entity.Property(e => e.createdat)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime");
            entity.Property(e => e.display).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.memory)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)");
            entity.Property(e => e.osid).HasDefaultValueSql("'NULL'");
            entity.Property(e => e.type)
                .HasMaxLength(30)
                .HasDefaultValueSql("'NULL'");

            entity.HasOne(d => d.Os).WithMany(p => p.Comps)
                .HasForeignKey(d => d.osid)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("comp_ibfk_1");
        });

        modelBuilder.Entity<Osystem>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PRIMARY");

            entity.ToTable("osystem");

            entity.Property(e => e.name)
                .HasMaxLength(50)
                .HasDefaultValueSql("'NULL'");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
