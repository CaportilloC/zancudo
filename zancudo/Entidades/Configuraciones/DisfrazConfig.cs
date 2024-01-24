using Microsoft.EntityFrameworkCore;

namespace zancudo.Entidades.Configuraciones
{
    public class DisfrazConfig : IEntityTypeConfiguration<Disfraz>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Disfraz> builder)
        {
            builder.Property(prop => prop.Nombre)
                .HasMaxLength(25)
                .IsRequired();

            builder.HasIndex(g => g.Nombre).IsUnique().HasFilter("EstaBorrado = 'false'");
        }
    }
}
