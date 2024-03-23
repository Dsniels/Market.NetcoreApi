using Core.Entities.OrdenCompra;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Data.Configuration
{
    public class OrdenCompraConfiguration : IEntityTypeConfiguration<OrdenCompras>
    {
        public void Configure(EntityTypeBuilder<OrdenCompras> builder)
        {
            builder.OwnsOne(o => o.DireccionEnvio, x =>
            {
                x.WithOwner();
            });

            builder.Property(s => s.Status)
                .HasConversion(
                    o => o.ToString(),
                    o => (OrdenStatus)Enum.Parse(typeof(OrdenStatus), o)
                    );
            builder.HasMany( o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);

        }
    }
}
