using System.Collections.Generic;
using HotChocolate;
using HotChocolate.Types;
using MongoDBClient.Database;

namespace MongoDBClient.GraphQL {

  //
  [ExtendObjectType(Name = "Query")]
  public class NoteQuery {

    //
    public IEnumerable<Note> GetNotes([Service] DBHpr hpr, int userId = 0) =>
        hpr.ObtemNotas(userId);
  }
}
