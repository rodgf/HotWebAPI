

using GraphQL.Types;

namespace MongoDBClient.GraphQL {
  public class NoteSchema {
    private ISchema _schema { get; set; }

    //
    public ISchema GraphQLSchema {
      get {
        return this._schema;
      }
    }

    public NoteSchema() {
      this._schema = Schema.For(@"
            type Note {
              body: String,
              updatedOn: Date,
              createdOn: Date,
              userId: Int,
              project: Project,
              dummies: [Dummy]
            }

            type Project {
              tipo: String,
              enturmada: String,
              matricula: Int,
              nome: String,
              turma: String,
              refturma: [Turma],
              derivacao: [Derivado]
            }

            type Turma {
              nome: String,
              local: String
            }

            type Derivado {
              matricula: Int,
              descricao: String
            }

            type Dummy {
              id: Int,
              name: String
            }

            type Query {
              notes: [Note],
              project(userId: Int): Project,
              dummies: [Dummy]
            }
        ", _ => {
        _.Types.Include<Query>();
      });
    }
  }
}
