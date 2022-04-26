using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MongoDBClient.Database;

namespace MongoDBClient.Controllers {

  // Dados de consulta simples
  [Route("[controller]")]
  [ApiController]
  public class NotesController : ControllerBase {
    private readonly DBHpr _hpr;

    public NotesController(DBHpr hpr) {
      _hpr = hpr;
    }

    //
    public IEnumerable<Note> Get() {
      List<Note> notas = _hpr.ObtemNotas();
      return notas;
    }
  }
}
