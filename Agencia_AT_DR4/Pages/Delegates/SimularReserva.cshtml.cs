using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Agencia_AT_DR4.Pages.Delegates
{
    public class SimularReservaModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Informe a quantidade de participantes.")]
        [Range(1, 100, ErrorMessage = "A quantidade deve ser entre 1 e 100.")]
        public int? Participantes { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Informe o preço do pacote.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal? PrecoPacote { get; set; }
        public decimal? ValorTotal { get; set; }
        public decimal? ParticipantesExibir { get; set; }
        public decimal? PrecoPacoteExibir { get; set; }
        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            Func<int, decimal, decimal> calcularTotal = (quantidade, preco) => quantidade * preco;

            ValorTotal = calcularTotal(Participantes!.Value, PrecoPacote!.Value);

            ParticipantesExibir = Participantes;
            PrecoPacoteExibir = PrecoPacote;

            Participantes = null;
            PrecoPacote = null;
            ModelState.Clear();

            return Page();
        }
    }
}