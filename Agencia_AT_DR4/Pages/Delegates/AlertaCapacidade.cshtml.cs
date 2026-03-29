using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Agencia_AT_DR4.Pages.Delegates
{
    // Classe que representa o pacote e dispara o evento
    public class GerenciadorReservas
    {
        public int CapacidadeMaxima { get; set; }
        public int ReservasAtuais { get; set; }

        // Declaração do evento
        public event EventHandler? CapacityReached;

        public string AdicionarReserva()
        {
            ReservasAtuais++;

            // Verifica se atingiu o limite e dispara o evento
            if (ReservasAtuais >= CapacidadeMaxima)
            {
                CapacityReached?.Invoke(this, EventArgs.Empty);
                return $"⚠️ Limite atingido! Pacote lotado com {ReservasAtuais} reservas.";
            }

            return $"✅ Reserva adicionada. Total: {ReservasAtuais}/{CapacidadeMaxima}";
        }
    }

    public class AlertaCapacidadeModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Informe a capacidade máxima.")]
        [Range(1, 100, ErrorMessage = "A capacidade deve ser entre 1 e 100.")]
        public int? CapacidadeMaxima { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Informe o número de reservas.")]
        [Range(1, 100, ErrorMessage = "O número deve ser entre 1 e 100.")]
        public int? NumeroReservas { get; set; }

        public List<string> Mensagens { get; set; } = new();

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var gerenciador = new GerenciadorReservas
            {
                CapacidadeMaxima = CapacidadeMaxima!.Value
            };

            // Assina o evento — define o que acontece quando disparar
            gerenciador.CapacityReached += (sender, args) =>
            {
                Console.WriteLine("[EVENTO] CapacityReached disparado — pacote lotado!");
                Mensagens.Add("🔔 EVENTO DISPARADO: Capacidade máxima atingida!");
            };

            // Simula as reservas uma por vez
            for (int i = 0; i < NumeroReservas!.Value; i++)
            {
                var resultado = gerenciador.AdicionarReserva();
                Mensagens.Add(resultado);
            }

            CapacidadeMaxima = null;
            NumeroReservas = null;
            ModelState.Clear();

            return Page();
        }
    }
}