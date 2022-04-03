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

    public IndexModel(ILogger<IndexModel> logger) {
      _logger = logger;
    }

    //
    public void OnGet() {
      DBHpr hpr = new DBHpr();

      List<Note> notas = hpr.ObtemNotas();
      ViewData["notas"] = notas;

      List<Aluno> alunos = hpr.ObtemAlunos();
      ViewData["alunos"] = alunos;

      List<Aluno> enturmados = hpr.ObtemAlunos("Enturmada");
      ViewData["enturmados"] = enturmados;

      ViewData["resultado"] = JValue.Parse(ObtemJson(hpr))
        .ToString(Formatting.Indented)
        .Replace("\n", "<br>")
        .Replace("\t", "&nbsp;&nbsp;")
        .Replace(" ", "&nbsp;");
    }

    //
    private static string ObtemJson(DBHpr hpr) {
      string[] query = {
                @"{
	""$project"": {
		""tipo"": {
			""$switch"": {
				""branches"": [{
						""case"": { ""$ne"": [ ""$nome"", ""Fonseca"" ] }, 
						""then"": ""Normal"" 
					}],
				""default"": ""$nome""
			}
		},
		""enturmada"": {
			""$switch"": {
				""branches"": [{
						""case"": { ""$ne"": [ ""$turma"", ""Enturmada"" ] }, 
						""then"": ""Desenturmado"" 
					}],
				""default"": ""$turma""
			}
		},
		""matricula"": ""$matricula"",
		""nome"": ""$nome"",
		""turma"": ""$turma""
	}
}", @"{
	$lookup: {
		from: ""turma"",
		localField: ""turma"",
		foreignField: ""nome"",
		as: ""refturma""
	}
}", @"{
		$lookup: {
			from: ""derivado"",
			localField: ""matricula"",
			foreignField: ""matricula"",
			as: ""derivacao""
		}
}" };

      List<object> objects = hpr.ObtemAlunosExt(query);
      string result = System.Text.Json.JsonSerializer.Serialize(objects);
      return result;
    }
  }
}
