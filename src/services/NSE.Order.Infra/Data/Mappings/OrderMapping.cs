using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NSE.Order.Domain.Orders;
namespace NSE.Order.Infra.Data.Mappings;
public class OrderMapping : IEntityTypeConfiguration<Domain.Orders.Order>
{
    public void Configure(EntityTypeBuilder<Domain.Orders.Order> builder)
    {
        builder.HasKey(c => c.Id);

        builder.OwnsOne(p => p.Address, e =>
        {            
            e.Property(pe => pe.Street).HasColumnName("Street");
            e.Property(pe => pe.Number).HasColumnName("Number");
            e.Property(pe => pe.Complement).HasColumnName("Complement");
            e.Property(pe => pe.District).HasColumnName("District");
            e.Property(pe => pe.ZipCode).HasColumnName("ZipCode");
            e.Property(pe => pe.City).HasColumnName("Cidade");
            e.Property(pe => pe.State).HasColumnName("State");
        });

        builder.Property(c => c.Code)
            .HasDefaultValueSql("NEXT VALUE FOR MySequence");

        // 1 : N => Pedido : PedidoItems
        builder.HasMany(c => c.OrderItems)
            .WithOne(c => c.Order)
            .HasForeignKey(c => c.OrderId);

        builder.ToTable("Orders");
    }
}
