using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Agencia_AT_DR4.Pages.NotasManager
{
    public class ViewNotesModel : PageModel
    {
        private readonly IWebHostEnvironment _env;

        public ViewNotesModel(IWebHostEnvironment env)
        {
            _env = env;
        }

        [BindProperty]
        [Required(ErrorMessage = "O nome do arquivo é obrigatório.")]
        [MinLength(3, ErrorMessage = "O nome deve ter pelo menos 3 caracteres.")]
        public string NomeArquivo { get; set; } = "";

        [BindProperty]
        [Required(ErrorMessage = "O conteúdo é obrigatório.")]
        public string Conteudo { get; set; } = "";

        public List<string> Arquivos { get; set; } = new();
        public string? ConteudoSelecionado { get; set; }
        public string? ArquivoSelecionado { get; set; }

        public void OnGet(string? arquivo)
        {
            CarregarArquivos();

            if (!string.IsNullOrEmpty(arquivo))
            {
                ArquivoSelecionado = arquivo;
                var caminho = Path.Combine(_env.WebRootPath, "files", arquivo);
                if (System.IO.File.Exists(caminho))
                    ConteudoSelecionado = System.IO.File.ReadAllText(caminho);
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                CarregarArquivos();
                return Page();
            }

            var pasta = Path.Combine(_env.WebRootPath, "files");
            Directory.CreateDirectory(pasta);

            var nomeSeguro = NomeArquivo.Trim().Replace(" ", "_") + ".txt";
            var caminho = Path.Combine(pasta, nomeSeguro);

            System.IO.File.WriteAllText(caminho, Conteudo);

            NomeArquivo = "";
            Conteudo = "";
            ModelState.Clear();

            CarregarArquivos();
            TempData["Sucesso"] = $"Arquivo '{nomeSeguro}' salvo com sucesso!";

            return Page();
        }

        private void CarregarArquivos()
        {
            var pasta = Path.Combine(_env.WebRootPath, "files");
            if (Directory.Exists(pasta))
                Arquivos = Directory.GetFiles(pasta, "*.txt")
                    .Select(Path.GetFileName!)
                    .ToList();
        }
    }
}