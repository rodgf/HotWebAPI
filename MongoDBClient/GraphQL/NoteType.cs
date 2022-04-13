using HotChocolate.Types;
using MongoDBClient.Database;

namespace MongoDBClient.GraphQL {
  public class NoteType : ObjectType<Note> {
    protected override void Configure(IObjectTypeDescriptor<Note> descriptor) {
      descriptor.Field(_ => _.Body);
      descriptor.Field(_ => _.UpdatedOn);
      descriptor.Field(_ => _.CreatedOn);
      //descriptor.Field<AlunoResolver>(_ => _.GetAluno(default, default));
      descriptor.Field<ProjectResolver>(_ => _.GetProject(default, default));
    }
  }
}
