using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace zancudo.Entidades.Configuraciones
{
    public class TipoDisfrazConfig : IEntityTypeConfiguration<TipoDisfraz>
    {
        public void Configure(EntityTypeBuilder<TipoDisfraz> builder)
        {
            builder.Property(prop => prop.Nombre)
            .HasMaxLength(25)
            .IsRequired();

        }
    }
}
