namespace Agencia_AT_DR4.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;
        public int PacoteTuristicoId { get; set; }
        public PacoteTuristico PacoteTuristico { get; set; } = null!;
        public DateTime DataReserva { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}