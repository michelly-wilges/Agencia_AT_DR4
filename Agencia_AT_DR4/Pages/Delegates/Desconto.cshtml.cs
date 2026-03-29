using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Agencia_AT_DR4.Pages.Delegates
{
    public delegate decimal CalculateDelegate(decimal preco);

    public class DescontoModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Informe o preço do pacote.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal? Preco { get; set; }

        public decimal? PrecoComDesconto { get; set; }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            CalculateDelegate calcular = AplicarDesconto;
            PrecoComDesconto = calcular(Preco!.Value);

            return Page();
        }

        private decimal AplicarDesconto(decimal preco)
        {
            return preco - (preco * 0.10m);
        }
    }
}