using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MongoDBClient.Database;

namespace MongoDBClient.Controllers {

  //
  [Route("[controller]")]
  [ApiController]
  public class NotesController : ControllerBase {
    readonly DBHpr _hpr = new DBHpr();

    //
    public IEnumerable<Note> Get() {
      List<Note> notas = _hpr.ObtemNotas();
      return notas;
    }
  }
}
