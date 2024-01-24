using Microsoft.EntityFrameworkCore;
using System;

namespace zancudo.Entidades.Seeding
{
    public static class SeedingZancudo
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var Credito = new TipoPago { Id = 1, Nombre = "Credito" };
            var Contado = new TipoPago { Id = 2, Nombre = "Contado" };
            var Cheque = new TipoPago { Id = 3, Nombre = "Cheque" };
            var Tarjeta = new TipoPago { Id = 4, Nombre = "Tarjeta" };

            modelBuilder.Entity<TipoPago>().HasData(Credito, Contado, Cheque, Tarjeta);


            var Teatral = new TipoDisfraz { Id = 1, Nombre = "Teatral" };
            var Festivo = new TipoDisfraz { Id = 2, Nombre = "Festivo" };
            var Tipico = new TipoDisfraz { Id = 3, Nombre = "Traje Tipico" };
            var Personaje = new TipoDisfraz { Id = 4, Nombre = "Personaje" };

            modelBuilder.Entity<TipoDisfraz>().HasData(Teatral, Festivo, Tipico, Personaje);
        }
    }
}
