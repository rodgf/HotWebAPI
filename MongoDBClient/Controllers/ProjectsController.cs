using System.Threading.Tasks;
using GraphQL;
using Microsoft.AspNetCore.Mvc;
using MongoDBClient.GraphQL;

namespace MongoDBClient.Controllers {

  //
  [Route("[controller]")]
  [ApiController]
  public class ProjectsController : ControllerBase {

    // GET api/values
    [HttpGet]
    public async Task<ActionResult> Get() {
      string query = @"{
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
          }
        }
      }";
      NoteSchema schema = new NoteSchema();
      ExecutionResult result = await new DocumentExecuter().ExecuteAsync(_ => {
        _.Schema = schema.GraphQLSchema;
        _.Query = query;
      });

      if (result.Errors?.Count > 0) {
        return BadRequest();
      }

      return Ok(result.Data);
    }
  }
}
