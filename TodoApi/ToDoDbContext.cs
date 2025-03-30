using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TodoApi;

public partial class ToDoDbContext : DbContext
{
    public ToDoDbContext()
    {
    }

    public ToDoDbContext(DbContextOptions<ToDoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Item> Items { get; set; }


// protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
// {
//     optionsBuilder.UseMySQL("mysql://uudtiwvk5cmts4wa:4P4RlE91Qvwin0ZhAWkT@btvvodt2k4n7c3tyv7ia-mysql.services.clever-cloud.com:3306/btvvodt2k4n7c3tyv7ia");
// }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("mysql://uudtiwvk5cmts4wa:4P4RlE91Qvwin0ZhAWkT@btvvodt2k4n7c3tyv7ia-mysql.services.clever-cloud.com:3306/btvvodt2k4n7c3tyv7ia");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("item");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
