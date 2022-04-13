using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MongoDBClient.Database;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MongoDBClient.Pages {

  //
  public class IndexModel : PageModel {
    private readonly ILogger<IndexModel> _logger;
    private readonly DBHpr _hpr;

    public IndexModel(ILogger<IndexModel> logger, DBHpr hpr) {
      _logger = logger;
      _hpr = hpr;
    }

    //
    public void OnGet() {
      List<Note> notas = _hpr.ObtemNotas();
      ViewData["notas"] = notas;

      List<Aluno> alunos = _hpr.ObtemAlunos();
      ViewData["alunos"] = alunos;

      List<Aluno> enturmados = _hpr.ObtemAlunos("Enturmada");
      ViewData["enturmados"] = enturmados;

      ViewData["resultado"] = JValue.Parse(ObtemJson(_hpr))
        .ToString(Formatting.Indented)
        .Replace("\n", "<br>")
        .Replace("\t", "&nbsp;&nbsp;")
        .Replace(" ", "&nbsp;");
    }

    //
    private static string ObtemJson(DBHpr hpr) {
      List<Project> objects = hpr.ObtemProjetos();
      string result = System.Text.Json.JsonSerializer.Serialize(objects);
      return result;
    }
  }
}
