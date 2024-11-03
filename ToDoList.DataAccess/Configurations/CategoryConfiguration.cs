using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToDoList.Models.Entities;

namespace ToDoList.DataAccess.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories").HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("CategoryId");
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

        builder
            .HasMany(x => x.ToDos)
            .WithOne(c => c.Category)
            .HasForeignKey(c => c.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}