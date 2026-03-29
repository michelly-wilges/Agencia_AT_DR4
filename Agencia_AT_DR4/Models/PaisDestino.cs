namespace Agencia_AT_DR4.Models
{
    public class PaisDestino
    {
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public string Codigo { get; set; } = "";
        public List<CidadeDestino> Cidades { get; set; } = new();
    }
}