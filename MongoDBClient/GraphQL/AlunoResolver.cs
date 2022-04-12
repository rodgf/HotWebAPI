using HotChocolate;
using HotChocolate.Types;
using MongoDBClient.Database;

namespace MongoDBClient.GraphQL {

  [ExtendObjectType(Name = "aluno")]
  public class AlunoResolver {
    public Aluno GetAluno([Parent] Note nota, [Service] DBHpr hpr) =>
        hpr.ObtemAluno(nota.UserId);
  }
}
