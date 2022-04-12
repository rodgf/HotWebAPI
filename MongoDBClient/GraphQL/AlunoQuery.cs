using System.Collections.Generic;
using HotChocolate;
using HotChocolate.Types;
using MongoDBClient.Database;

namespace MongoDBClient.GraphQL {

  //
  [ExtendObjectType(Name = "Query")]
  public class AlunoQuery {

    //
    public IEnumerable<Aluno> GetAlunos([Service] DBHpr hpr) =>
        hpr.ObtemAlunos();
  }
}
