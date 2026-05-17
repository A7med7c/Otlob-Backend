using DomainLayer.Models.OrderModule;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations.OrderModule
{
    internal class OrderConfigurations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.Property(p => p.SubTotal).HasColumnType("decimal(8, 2)");

            builder.HasMany(p => p.Items)
                .WithOne();

            builder.HasOne(p => p.DeliveyMethod)
                .WithMany()
                .HasForeignKey(p => p.DeliveryMethodId);

            builder.OwnsOne(p => p.ShippingAddress);
        }
    }
}
