namespace zancudo.Entidades
{
    public class TipoPago
    {
        public int Id { get; set; }
        public string  Nombre { get; set; }
        public HashSet<ClienteDisfraz> ClientesDisfraces { get; set; }
    }
}
