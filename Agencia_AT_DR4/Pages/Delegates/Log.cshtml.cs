using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.IO;


namespace Agencia_AT_DR4.Pages.Delegates
{
    public class LogModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Informe a operação a registrar.")]
        public string Operacao { get; set; } = "";

        public List<string> Logs { get; set; } = new();

        private static List<string> _logMemoria = new();

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            Action<string> logger = LogToConsole;
            logger += LogToFile;
            logger += LogToMemory;

            logger(Operacao);

            Logs = new List<string>(_logMemoria);

            // Limpa o campo após registrar
            Operacao = "";
            ModelState.Clear();

            return Page();
        }
        private void LogToConsole(string mensagem)
        {
            Console.WriteLine($"[CONSOLE] {DateTime.Now:HH:mm:ss} - {mensagem}");
        }

        private void LogToFile(string mensagem)
        {
            var pasta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "logs");
            Directory.CreateDirectory(pasta);
            var arquivo = Path.Combine(pasta, "log.txt");
            System.IO.File.AppendAllText(arquivo, $"[FILE] {DateTime.Now:HH:mm:ss} - {mensagem}{Environment.NewLine}");
        }

        private void LogToMemory(string mensagem)
        {
            _logMemoria.Add($"[MEMORY] {DateTime.Now:HH:mm:ss} - {mensagem}");
        }
    }
}