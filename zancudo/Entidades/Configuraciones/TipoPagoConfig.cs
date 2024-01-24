using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace zancudo.Entidades.Configuraciones
{
    public class TipoPagoConfig : IEntityTypeConfiguration<TipoPago>
    {
        public void Configure(EntityTypeBuilder<TipoPago> builder)
        {
            builder.Property(prop => prop.Nombre)
                .HasMaxLength(25)
                .IsRequired();

        }
    }
}
