using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Models.Entities;

namespace ToDoList.DataAccess.Configurations;

public class ToDoConfiguration : IEntityTypeConfiguration<ToDo>
{
    public void Configure(EntityTypeBuilder<ToDo> builder)
    {
        builder.ToTable("ToDos").HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("ToDoId");
        builder.Property(x => x.Title).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(500);
        builder.Property(x => x.EndDate).IsRequired();
        builder.Property(x => x.Priority).IsRequired();
        builder.Property(x => x.Completed).IsRequired();

        builder
            .HasOne(x => x.Category)
            .WithMany(c => c.ToDos)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(x => x.User)
            .WithMany(u => u.ToDo)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}