using System.Threading.Tasks;
using GraphQL;
using Microsoft.AspNetCore.Mvc;
using MongoDBClient.GraphQL;

namespace MongoDBClient.Controllers {

  // Obtém dados de consulta por Schema GraphQL
  [Route("[controller]")]
  [ApiController]
  public class ProjectsController : ControllerBase {

    // GET api/values
    [HttpGet]
    public async Task<ActionResult> Get() {
      string stQuery = @"{
        notes {
          body,
          updatedOn,
          createdOn,
          project {
            tipo,
            enturmada,
            matricula,
            nome,
            turma,
            refturma {
              nome,
              local
            },
            derivacao {
              matricula,
              descricao
            }
          },
          dummies {
            name
          }
        }
      }";

      ProjectSchema schema = new ProjectSchema();
      ExecutionResult result = await new DocumentExecuter().ExecuteAsync(_ => {
        _.Schema = schema.GraphQLSchema;
        _.Query = stQuery;
      });

      if (result.Errors?.Count > 0) {
        return BadRequest("Falha ao obter projetos: " + result.Errors[0].Message);
      }

      return Ok(result.Data);
    }
  }
}
