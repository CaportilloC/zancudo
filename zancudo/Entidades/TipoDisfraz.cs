namespace zancudo.Entidades
{
    public class TipoDisfraz
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public HashSet<Disfraz> Disfraces { get; set; }
    }
}
