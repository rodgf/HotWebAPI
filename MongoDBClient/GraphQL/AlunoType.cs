using HotChocolate.Types;
using MongoDBClient.Database;

namespace MongoDBClient.GraphQL {
  public class AlunoType : ObjectType<Aluno> {
    protected override void Configure(IObjectTypeDescriptor<Aluno> descriptor) {
      descriptor.Field(_ => _.matricula);
      descriptor.Field(_ => _.nome);
      descriptor.Field(_ => _.turma);
    }
  }
}
